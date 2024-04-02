using JointureInterfaceMetier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donnees
{
    public static class Dal
    {
        public static void Execute(CommandLine command)
        {
            switch (command.LaCommande)
            {
                case CommandEnum.Get_Product:
                    command.LesProduits = Get_Product();

                    break;
            }
        }

        private static List<Produit> Get_Product()
        {
            var liste = new List<Produit>();
            var cnx = new SqlConnection();
            cnx.ConnectionString = @"Data source=.\SQLEXPRESS;initial Catalog=AdventureWorks2017;Integrated security=true";
            cnx.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select ProductID, Name, Color from Production.Product";
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                liste.Add(new Produit { Id = (int)rd["ProductId"], Nom = (string)rd["Name"], Couleur = rd["Color"] as string});
            }
            rd.Close();
            return liste;
        }
    }
}
