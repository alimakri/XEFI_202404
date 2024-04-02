using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JointureInterfaceMetier
{
    public enum VerbEnum { None, Get, Set }
    public enum NounEnum { None, Product, Client }
    public enum CommandEnum { None, Get_Product }
    public class CommandLine
    {
        public string MessageErreur = "";
        public CommandEnum LaCommande = CommandEnum.None;
        public CommandLine(string saisie)
        {
            // Solution Regex
            var pattern = @"(?<verb>\w+)-(?<Noun>\w+)(?: +-(?<param>\w+) +(?<val>[\wéèçôê]+))*";
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

            var parameters = match.Groups["param"].Captures;
            var vals = match.Groups["val"].Captures;
        }
        public void Execute()
        {
        }
    }

}
