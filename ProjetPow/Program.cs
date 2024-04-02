using JointureInterfaceMetier;
using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetPow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Tapez votre commande : ");
            var saisie = Console.ReadLine();
            var commandLine = new CommandLine(saisie);
            if (commandLine.MessageErreur == "")
            {
                Console.WriteLine("Execution de la commande...");
                Bol.Execute(commandLine);
                foreach(var produit in commandLine.LesProduits)
                {
                    Console.WriteLine(produit);
                }
            }
            else
            {
                Console.WriteLine(commandLine.MessageErreur);
            }
            Console.ReadLine();
        }
    }
}