using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JointureInterfaceMetier
{
    public enum VerbEnum { None, Get, Set, Clear, Exit, New, Update, Delete }
    public enum NounEnum { None, Product, Cat, Host, TotalOrder, Person }
    public enum CommandEnum { None, Get_Product, Get_Cat, Clear_Host, Exit_Host, Get_TotalOrder, Get_Person, New_Product, Update_Product, Delete_Product }
    public class CommandLine
    {
        public static Dictionary<string, string> ListeAlias = new Dictionary<string, string>();
        public static Dictionary<string, string> ListeTypeParam = new Dictionary<string, string>();
        public string MessageErreur = "";
        public CommandEnum LaCommande = CommandEnum.None;
        public List<Produit> LesProduits;
        public List<Personne> LesPersonnes;
        public List<int> LesEntiers;
        public List<string> LesCats;
        public List<string> LesTotaux;
        public List<string> LesParametres = new List<string>();
        public List<string> LesValeurs = new List<string>();

        public static void Init()
        {
            // Alias
            ListeAlias.Add("CLS", "Clear-Host");
            ListeAlias.Add("EXIT", "Exit-Host");

            // Type Param
            ListeTypeParam.Add("Year", "int");
            ListeTypeParam.Add("Price", "decimal");
        }
        public CommandLine(string saisie)
        {
            // Alias
            // Solution Regex
            var pattern = @"(?<verb>\w+)-(?<Noun>\w+)(?: +-(?<param>\w+) +""?(?<val>[\wéèçôê %.]+)""?)*$";
            Match match = Regex.Match(saisie, pattern);

            // Check Regex
            if (!match.Success)
            {
                MessageErreur = "L'expression régulière ne fonctionne pas.";
                return;
            }

            // Extraire les valeurs des groupes
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
            for (int i = 0; i < parametres.Count; i++)
            {
                // Check type 
                if (ListeTypeParam.Keys.Contains(parametres[i].Value))
                {
                    switch(ListeTypeParam[parametres[i].Value])
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
            for (int i = 0; i < valeurs.Count; i++)
            {
                LesValeurs.Add(valeurs[i].Value);
            }
        }
    }

}
