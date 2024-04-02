using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JointureInterfaceMetier
{
    public enum VerbEnum { None, Get, Set, Clear, Exit }
    public enum NounEnum { None, Product, Cat, Host }
    public enum CommandEnum { None, Get_Product, Get_Cat, Clear_Host,
        Exit_Host
    }
    public class CommandLine
    {
        public static Dictionary<string, string> ListeAlias = new Dictionary<string, string>();
        public string MessageErreur = "";
        public CommandEnum LaCommande = CommandEnum.None;
        public List<Produit> LesProduits;
        public List<string> LesCats;
        public List<string> LesParametres = new List<string>();
        public List<string> LesValeurs = new List<string>();

        public static void Init()
        {
            ListeAlias.Add("CLS", "Clear-Host");
            ListeAlias.Add("EXIT", "Exit-Host");
        }
        public CommandLine(string saisie)
        {
            // Alias
            // Solution Regex
            var pattern = @"(?<verb>\w+)-(?<Noun>\w+)(?: +-(?<param>\w+) +""?(?<val>[\wéèçôê %]+)""?)*$";
            Match match = Regex.Match(saisie, pattern);

            if (!match.Success)
            {
                MessageErreur = "L'expression régulière ne fonctionne pas.";
                return;
            }

            // Extraire les valeurs des groupes
            string strVerb = match.Groups["verb"].Value;
            string strNoun = match.Groups["Noun"].Value;

            VerbEnum verb = VerbEnum.None; NounEnum noun = NounEnum.None;
            if (!Enum.TryParse<VerbEnum>(strVerb, out verb))
            {
                MessageErreur = $"Le verbe {strVerb} n'existe pas !";
                return;
            }
            if (!Enum.TryParse<NounEnum>(strNoun, out noun))
            {
                MessageErreur = $"Le nom {strNoun} n'existe pas !";
                return;
            }
            if (!Enum.TryParse<CommandEnum>($"{strVerb}_{strNoun}", out LaCommande))
            {
                MessageErreur = $"La commande {strVerb}-{strNoun} n'existe pas !";
                return;
            }

            var parametres = match.Groups["param"].Captures;
            var valeurs = match.Groups["val"].Captures;
            for (int i = 0; i < parametres.Count; i++)
            {
                LesParametres.Add(parametres[i].Value);
            }
            for (int i = 0; i < valeurs.Count; i++)
            {
                LesValeurs.Add(valeurs[i].Value);
            }
        }
    }

}
