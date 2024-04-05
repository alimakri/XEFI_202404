using Donnees;
using JointureInterfaceMetier;
using System;
using System.Collections.Generic;

namespace Metier
{
    public static class Bol
    {
        public static void ExecuteData(CommandLine command)
        {
            Dal.Execute(command);
        }

        public static Dictionary<string, string> ExecuteInfos(CommandLine commandLine)
        {
            switch (commandLine.LaCommande)
            {
                case CommandEnum.Get_Alias: return CommandLine.ListeAlias;
                case CommandEnum.Get_Command:
                    var dico = new Dictionary<string, string>();
                    foreach (var cmd in CommandLine.ListeTypeCommand)
                        dico.Add(cmd.Key.ToString(), cmd.Value.ToString());
                    return dico;
            }
            return null;
        }
    }
}
