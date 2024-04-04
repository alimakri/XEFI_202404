using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace JointureInterfaceMetier
{
    #region Enums
    public enum VerbEnum { None, Get, Set, Clear, Exit, New, Update, Delete }
    public enum NounEnum { None, Product, Cat, Host, TotalOrder, Person, Alias, Command }
    public enum CommandEnum
    {
        None,
        // Get Data
        Get_Product, Get_Cat, Get_TotalOrder, Get_Person, 
        // New Data
        New_Product,
        // Update Data
        Update_Product, 
        // Delete Data
        Delete_Product,
        // Host
        Clear_Host, Exit_Host,
        // Infos CommandLine
        Get_Alias, Get_Command,
    }
    public enum TypeCommandEnum { Data, Console, CommandLine }
    #endregion

    public class CommandLine
    {
        #region Dictionnaires
        public static Dictionary<string, string> ListeAlias = new Dictionary<string, string>();
        public static Dictionary<string, string> ListeTypeParam = new Dictionary<string, string>();
        public static Dictionary<CommandEnum, TypeCommandEnum> ListeTypeCommand = new Dictionary<CommandEnum, TypeCommandEnum>();
        #endregion

        #region Listes résultats
        public List<Produit> LesProduits;
        public List<Personne> LesPersonnes;
        public List<int> LesEntiers;
        public List<string> LesCats;
        public List<string> LesTotaux;
        #endregion

        #region Propriétés CommandLine
        public string MessageErreur = "";
        public CommandEnum LaCommande = CommandEnum.None;
        public List<string> LesParametres = new List<string>();
        public List<string> LesValeurs = new List<string>();
        #endregion

        #region Constructor
        public static void Init()
        {
            // Alias
            ListeAlias.Add("CLS", "Clear-Host");
            ListeAlias.Add("EXIT", "Exit-Host");

            // Type Param
            ListeTypeParam.Add("Year", "int");
            ListeTypeParam.Add("Price", "decimal");

            // Type Command
            ListeTypeCommand.Add(CommandEnum.Get_Cat, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.Get_Person, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.Get_Product, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.Clear_Host, TypeCommandEnum.Console);
            ListeTypeCommand.Add(CommandEnum.Delete_Product, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.Exit_Host, TypeCommandEnum.Console);
            ListeTypeCommand.Add(CommandEnum.Get_TotalOrder, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.New_Product, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.Update_Product, TypeCommandEnum.Data);
            ListeTypeCommand.Add(CommandEnum.Get_Alias, TypeCommandEnum.CommandLine);
            ListeTypeCommand.Add(CommandEnum.Get_Command, TypeCommandEnum.CommandLine);
        }
        public CommandLine(string saisie)
        {
            // Regex
            var pattern = @"(?<verb>\w+)-(?<Noun>\w+)(?: +-(?<param>\w+) +""?(?<val>[\wéèçôê %.]+)""?)*$";
            Match match = Regex.Match(saisie, pattern);

            // Check Regex
            if (!match.Success)
            {
                MessageErreur = "L'expression régulière ne fonctionne pas.";
                return;
            }

            // Extraire le verbe et le nom
            string strVerb = match.Groups["verb"].Value;
            string strNoun = match.Groups["Noun"].Value;

            // Check Verbe
            VerbEnum verb = VerbEnum.None; NounEnum noun = NounEnum.None;
            if (!Enum.TryParse<VerbEnum>(strVerb, out verb))
            {
                MessageErreur = $"Le verbe {strVerb} n'existe pas !";
                return;
            }
            // Check Nom
            if (!Enum.TryParse<NounEnum>(strNoun, out noun))
            {
                MessageErreur = $"Le nom {strNoun} n'existe pas !";
                return;
            }
            // Check Commande
            if (!Enum.TryParse<CommandEnum>($"{strVerb}_{strNoun}", out LaCommande))
            {
                MessageErreur = $"La commande {strVerb}-{strNoun} n'existe pas !";
                return;
            }
            // Extraction des couples paramètres\valeurs
            var parametres = match.Groups["param"].Captures;
            var valeurs = match.Groups["val"].Captures;

            // Extraction des paramètres
            for (int i = 0; i < parametres.Count; i++)
            {
                // Check type du paramètre
                if (ListeTypeParam.Keys.Contains(parametres[i].Value))
                {
                    switch (ListeTypeParam[parametres[i].Value])
                    {
                        case "decimal":
                            var culture = CultureInfo.InvariantCulture;
                            if (!decimal.TryParse(valeurs[i].Value, System.Globalization.NumberStyles.Currency, culture, out _))
                                MessageErreur = $"Le paramètre {parametres[i].Value} doit être un décimal.";
                            break;
                        case "int":
                            if (!int.TryParse(valeurs[i].Value, out _))
                                MessageErreur = $"Le paramètre {parametres[i].Value} doit être un entier.";
                            break;
                        case "date":
                            if (!DateTime.TryParse(valeurs[i].Value, out _))
                                MessageErreur = $"Le paramètre {parametres[i].Value} doit être une date.";
                            break;
                    }
                }
                LesParametres.Add(parametres[i].Value);
            }
            // Extraction des valeurs
            for (int i = 0; i < valeurs.Count; i++)
            {
                LesValeurs.Add(valeurs[i].Value);
            }
        }
        #endregion
    }

}
