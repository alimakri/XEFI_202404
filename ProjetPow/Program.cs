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
            while (true)
            {
                Console.Write("Tapez votre commande : ");
                var saisie = Console.ReadLine();
                var commandLine = new CommandLine(saisie);
                if (commandLine.MessageErreur == "")
                {
                    Console.WriteLine("Execution de la commande...");
                    Bol.Execute(commandLine);
                    Affichage(commandLine);
                }
                else
                {
                    Console.WriteLine(commandLine.MessageErreur);
                }
            }
            Console.ReadLine();
        }

        private static void Affichage(CommandLine commandLine)
        {
            switch (commandLine.LaCommande)
            {
                case CommandEnum.Get_Product:
                    foreach (var produit in commandLine.LesProduits)
                    {
                        Console.WriteLine(produit);
                    }
                    break;
                case CommandEnum.Get_Cat:
                    foreach (var cat in commandLine.LesCats)
                    {
                        Console.WriteLine(cat);
                    }
                    break;
            }
        }
    }
}