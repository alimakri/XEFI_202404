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
            bool continuer = true;
            CommandLine.Init();
            while (continuer)
            {
                Console.Write("Tapez votre commande : ");
                var saisie = Console.ReadLine();

                // Alias
                if (CommandLine.ListeAlias.Keys.Contains(saisie.ToUpper()))
                {
                    saisie = CommandLine.ListeAlias[saisie.ToUpper()];
                }

                // Construction de l'objet CommandLine (Check)
                var commandLine = new CommandLine(saisie);
                if (commandLine.MessageErreur == "")
                {
                    switch (commandLine.LaCommande)
                    {
                        // Commande Data
                        case CommandEnum.Get_Cat:
                        case CommandEnum.Get_Product:
                            Bol.Execute(commandLine);
                            Affichage(commandLine);
                            break;

                        // Commande Console
                        case CommandEnum.Clear_Host:
                            Console.Clear();
                            break;
                        case CommandEnum.Exit_Host:
                            continuer = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(commandLine.MessageErreur);
                }
            }
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