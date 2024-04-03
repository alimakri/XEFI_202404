using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JointureInterfaceMetier
{
    public class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Ville { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Nom} {Prenom} {Ville}";
        }
    }
}
