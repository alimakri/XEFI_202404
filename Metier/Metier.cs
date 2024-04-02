using Donnees;
using JointureInterfaceMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public static class Bol
    {
        public static void Execute(CommandLine command)
        {
            Dal.Execute(command);
        }
    }
}
