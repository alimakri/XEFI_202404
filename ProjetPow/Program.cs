using JointureInterfaceMetier;
using Metier;
using System;
using System.Linq;
using System.Text;

// Update-Product -Id 1018 -Price 3.60
// update Product set ListPrice=3.60 where ProductId=1018
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                var saisie = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;


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
                        case CommandEnum.New_Product:
                        case CommandEnum.Update_Product:
                        case CommandEnum.Delete_Product:
                            Bol.Execute(commandLine);
                            if (commandLine.MessageErreur == "")
                                Affichage(commandLine);
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(commandLine.MessageErreur);
                            }
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
                Console.ForegroundColor = ConsoleColor.Gray;
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