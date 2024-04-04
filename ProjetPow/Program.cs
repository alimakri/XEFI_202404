using JointureInterfaceMetier;
using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetPow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialisation de la console
            Console.OutputEncoding = Encoding.UTF8;
            bool continuer = true;

            // Initialisation de CommandLine
            CommandLine.Init();

            while (continuer)
            {
                // Saisie de la commande
                Console.Write("Tapez votre commande : ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                var saisie = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                // Commande est un alias ?
                if (CommandLine.ListeAlias.Keys.Contains(saisie.ToUpper()))
                    saisie = CommandLine.ListeAlias[saisie.ToUpper()];

                // Construction de l'objet CommandLine (Check)
                var commandLine = new CommandLine(saisie);
                if (commandLine.MessageErreur == "")
                {
                    switch (CommandLine.ListeTypeCommand[commandLine.LaCommande])
                    {
                        case TypeCommandEnum.Data:
                            // Exécution de la commande Data
                            Bol.ExecuteData(commandLine);
                            if (commandLine.MessageErreur == "")
                                Affichage(commandLine);
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(commandLine.MessageErreur);
                            }
                            break;

                        case TypeCommandEnum.Console:
                            // Exécution de la commande Console
                            continuer = ExecuteCommandConsole(commandLine.LaCommande);
                            break;

                        case TypeCommandEnum.CommandLine:
                            var dico = Bol.ExecuteInfos(commandLine);
                            Affichage(dico);
                            break;
                    }
                }
                else
                {
                    // Affichage Erreur check de la commande
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(commandLine.MessageErreur);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }


        private static bool ExecuteCommandConsole(CommandEnum laCommande)
        {
            switch (laCommande)
            {
                case CommandEnum.Clear_Host: Console.Clear(); break;
                case CommandEnum.Exit_Host: return false;
            }
            return true;
        }

        private static void Affichage(CommandLine commandLine)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            switch (commandLine.LaCommande)
            {
                case CommandEnum.Get_Product:
                    foreach (var produit in commandLine.LesProduits) Console.WriteLine(produit);
                    break;
                case CommandEnum.Get_Person:
                    foreach (var personne in commandLine.LesPersonnes) Console.WriteLine(personne);
                    break;
                case CommandEnum.Get_Cat:
                    foreach (var cat in commandLine.LesCats) Console.WriteLine(cat);
                    break;
                case CommandEnum.Get_TotalOrder:
                    foreach (var total in commandLine.LesTotaux) Console.WriteLine(double.Parse(total).ToString("C"));
                    break;
            }
        }
        private static void Affichage(Dictionary<string, string> dico)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var item in dico)
            {
                Console.WriteLine("{0} -> {1}",  item.Key.PadRight(30), item.Value);
            }
        }

    }
}