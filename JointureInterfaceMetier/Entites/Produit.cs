using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JointureInterfaceMetier
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Couleur { get; set; }
        public decimal Prix { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Nom} {Couleur} {Prix}";
        }
    }
}
