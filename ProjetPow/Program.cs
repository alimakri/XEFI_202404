using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetPow
{
    public enum VerbEnum { None, Get, Set }
    public enum NounEnum { None, Product }
    public enum CommandEnum { None, Get_Product}
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Tapez votre commande : ");
            var saisie = Console.ReadLine();

            // Solution Split
            //var tableau = saisie.Split('-');

            // Solution Regex
            var pattern = @"(?<verb>\w+)-(?<Noun>\w+) +-(?<param1>\w+) +(?<val1>[\wéèçôê]+)";
            Match match = Regex.Match(saisie, pattern);

            if (!match.Success)
            {
                Console.WriteLine("Erreur");
                Console.ReadLine();
                return;
            }

            // Extraire les valeurs des groupes
            string strVerb = match.Groups["verb"].Value;
            string strNoun = match.Groups["Noun"].Value;
            string param1 = match.Groups["param1"].Value;
            string val1 = match.Groups["val1"].Value;

            VerbEnum verb = VerbEnum.None; NounEnum noun = NounEnum.None; CommandEnum command = CommandEnum.None;
            if (!Enum.TryParse<VerbEnum>(strVerb, out verb))
            {
                Console.WriteLine($"Le verbe {strVerb} n'existe pas !");
            }
            else
            {
                if (!Enum.TryParse<NounEnum>(strNoun, out noun))
                {
                    Console.WriteLine($"Le nom {strNoun} n'existe pas !");
                }
                else
                {
                    if (!Enum.TryParse<CommandEnum>($"{strVerb}_{strNoun}", out command))
                    {
                        Console.WriteLine($"La commande {strVerb}-{strNoun} n'existe pas !");
                    }
                    else
                    {
                        Console.WriteLine("Execution de la commande...");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}