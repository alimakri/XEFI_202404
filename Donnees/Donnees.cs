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
                    command.LesProduits = Get_Product(command);
                    break;
                case CommandEnum.Get_Cat:
                    command.LesCats = Get_Cat(command);
                    break;
            }
        }
        private static List<string> Get_Cat(CommandLine command)
        {
            var liste = new List<string>();
            var cnx = new SqlConnection();
            cnx.ConnectionString = @"Data source=.\SQLEXPRESS;initial Catalog=AdventureWorks2017;Integrated security=true";
            cnx.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Name from Production.ProductCategory";
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                liste.Add((string)rd["Name"]);
            }
            rd.Close();
            return liste;
        }
        private static List<Produit> Get_Product(CommandLine command)
        {
            var liste = new List<Produit>();
            var cnx = new SqlConnection();
            cnx.ConnectionString = @"Data source=.\SQLEXPRESS;initial Catalog=AdventureWorks2017;Integrated security=true";
            cnx.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.Text;
            if (command.LesParametres.Contains("Cat") && command.LesParametres.Contains("Like"))
            {
                var catVal = command.LesValeurs[command.LesParametres.IndexOf("Cat")];
                var likeVal = command.LesValeurs[command.LesParametres.IndexOf("Like")];
                cmd.CommandText = $@"select ProductID, p.Name, Color, c.Name
                                    from Production.Product p
                                    inner join Production.ProductSubcategory sc on p.ProductSubcategoryID = sc.ProductSubcategoryID
                                    inner join Production.Productcategory c on sc.ProductCategoryID = c.ProductCategoryID
                                    where c.Name='{catVal}' and p.Name like '{likeVal}'";
            }
            else if (command.LesParametres.Contains("Like"))
                cmd.CommandText = $@"select * from Production.Product where Name like '{command.LesValeurs[0]}'";
            else if (command.LesParametres.Contains("Cat"))
                cmd.CommandText = $@"select ProductID, p.Name, Color, c.Name
                                    from Production.Product p
                                    inner join Production.ProductSubcategory sc on p.ProductSubcategoryID = sc.ProductSubcategoryID
                                    inner join Production.Productcategory c on sc.ProductCategoryID = c.ProductCategoryID
                                    where c.Name='{command.LesValeurs[0]}'";
            else
                cmd.CommandText = "select ProductID, Name, Color from Production.Product";
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                liste.Add(new Produit { Id = (int)rd["ProductId"], Nom = (string)rd["Name"], Couleur = rd["Color"] as string });
            }
            rd.Close();
            return liste;
        }
    }
}
