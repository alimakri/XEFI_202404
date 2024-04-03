using JointureInterfaceMetier;
using Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// New-Product -Name Clavier -Price 22.15
// insert Production.product (Name, ListPrice) values('clavier',20.15)
namespace ProjetPow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
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
                        case CommandEnum.Get_TotalOrder:
                        case CommandEnum.Get_Person:
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(commandLine.MessageErreur);
                }
            }
        }

        private static void Affichage(CommandLine commandLine)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            switch (commandLine.LaCommande)
            {
                case CommandEnum.Get_Product:
                    foreach (var produit in commandLine.LesProduits)
                    {
                        Console.WriteLine(produit);
                    }
                    break;
                case CommandEnum.Get_Person:
                    foreach (var personne in commandLine.LesPersonnes)
                    {
                        Console.WriteLine(personne);
                    }
                    break;
                case CommandEnum.Get_Cat:
                    foreach (var cat in commandLine.LesCats)
                    {
                        Console.WriteLine(cat);
                    }
                    break;
                case CommandEnum.Get_TotalOrder:
                    foreach (var total in commandLine.LesTotaux)
                    {
                        Console.WriteLine(double.Parse(total).ToString("C"));
                    }
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}