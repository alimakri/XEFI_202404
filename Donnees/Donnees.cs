using JointureInterfaceMetier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                case CommandEnum.Get_TotalOrder:
                    command.LesTotaux = Get_TotalOrder(command);
                    break;
                case CommandEnum.Get_Person:
                    command.LesPersonnes = Get_Person(command);
                    break;
                case CommandEnum.New_Product:
                    command.LesEntiers = New_Product(command);
                    break;
                case CommandEnum.Update_Product:
                    command.LesEntiers = Update_Product(command);
                    break;
                case CommandEnum.Delete_Product:
                    command.LesEntiers = Delete_Product(command);
                    break;
            }
        }
        private static List<int> Delete_Product(CommandLine command)
        {
            var cmd = Connexion();
            if (command.LesParametres.Contains("Id"))
            {
                var id = command.LesValeurs[command.LesParametres.IndexOf("Id")];
                cmd.CommandText = $@"delete Production.Product where ProductId={id}";
            }
            int n = 0;
            try
            {
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                command.MessageErreur = ex.Message;
            }
            return new List<int> { n };
        }

        private static List<int> Update_Product(CommandLine command)
        {
            var cmd = Connexion();
            if (command.LesParametres.Contains("Id") &&
                command.LesParametres.Contains("Price"))
            {
                var id = command.LesValeurs[command.LesParametres.IndexOf("Id")];
                var price = command.LesValeurs[command.LesParametres.IndexOf("Price")];
                cmd.CommandText = $@"update Production.Product set ListPrice={price} where ProductId={id}";
            }
            int n = 0;
            try
            {
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                command.MessageErreur = ex.Message;
            }
            return new List<int> { n };
        }

        private static List<int> New_Product(CommandLine command)
        {
            var cmd = Connexion();
            if (command.LesParametres.Contains("Name") &&
                command.LesParametres.Contains("Price") &&
                command.LesParametres.Contains("ProductNumber"))
            {
                var name = command.LesValeurs[command.LesParametres.IndexOf("Name")];
                var price = command.LesValeurs[command.LesParametres.IndexOf("Price")];
                var pn = command.LesValeurs[command.LesParametres.IndexOf("ProductNumber")];
                cmd.CommandText = $@"insert Production.product (Name, ListPrice, ProductNumber, SafetyStockLevel, ReorderPoint, StandardCost, DaysToManufacture, SellStartDate) 
                                        values('{name}',{price}, '{pn}', 1, 1, 0, 0, getdate())";
            }
            int n = 0;
            try
            {
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                command.MessageErreur = ex.Message;
            }
            return new List<int> { n };
        }

        #region Get
        private static SqlCommand Connexion()
        {
            var cnx = new SqlConnection();
            cnx.ConnectionString = @"Data source=.\SQLEXPRESS;initial Catalog=AdventureWorks2017;Integrated security=true";
            cnx.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        private static List<string> Get_TotalOrder(CommandLine command)
        {
            var cmd = Connexion();
            if (command.LesParametres.Contains("Year"))
            {
                cmd.CommandText = @"select Sum(d.OrderQty * d.UnitPrice) total
                                from Sales.SalesOrderDetail d
                                inner join Sales.SalesOrderHeader h on d.SalesOrderID = h.SalesOrderID
                                Group by Year(h.OrderDate)
                                having Year(h.OrderDate)=2014";
            }
            var rd = cmd.ExecuteReader();
            var liste = new List<string>();
            while (rd.Read())
            {
                liste.Add(rd["total"].ToString());
            }
            rd.Close();
            return liste;
        }
        private static List<string> Get_Cat(CommandLine command)
        {
            var cmd = Connexion();
            cmd.CommandText = "select Name from Production.ProductCategory";
            var rd = cmd.ExecuteReader();
            var liste = new List<string>();
            while (rd.Read())
            {
                liste.Add((string)rd["Name"]);
            }
            rd.Close();
            return liste;
        }
        private static List<Produit> Get_Product(CommandLine command)
        {
            var cmd = Connexion();
            if (command.LesParametres.Contains("Cat") && command.LesParametres.Contains("Like"))
            {
                var catVal = command.LesValeurs[command.LesParametres.IndexOf("Cat")];
                var likeVal = command.LesValeurs[command.LesParametres.IndexOf("Like")];
                cmd.CommandText = $@"select ProductID, p.Name, Color, c.Name, p.ListePrice
                                    from Production.Product p
                                    inner join Production.ProductSubcategory sc on p.ProductSubcategoryID = sc.ProductSubcategoryID
                                    inner join Production.Productcategory c on sc.ProductCategoryID = c.ProductCategoryID
                                    where c.Name='{catVal}' and p.Name like '{likeVal}'";
            }
            else if (command.LesParametres.Contains("Id"))
                cmd.CommandText = $@"select * from Production.Product where ProductId={command.LesValeurs[0]}";
            else if (command.LesParametres.Contains("Like"))
                cmd.CommandText = $@"select * from Production.Product where Name like '{command.LesValeurs[0]}'";
            else if (command.LesParametres.Contains("Cat"))
                cmd.CommandText = $@"select ProductID, p.Name, Color, c.Name, p.ListePrice
                                    from Production.Product p
                                    inner join Production.ProductSubcategory sc on p.ProductSubcategoryID = sc.ProductSubcategoryID
                                    inner join Production.Productcategory c on sc.ProductCategoryID = c.ProductCategoryID
                                    where c.Name='{command.LesValeurs[0]}'";
            else
                cmd.CommandText = "select ProductID, Name, Color, ListPrice from Production.Product";
            var rd = cmd.ExecuteReader();
            var liste = new List<Produit>();
            while (rd.Read())
            {
                liste.Add(new Produit
                {
                    Id = (int)rd["ProductId"],
                    Nom = (string)rd["Name"],
                    Couleur = rd["Color"] as string,
                    Prix = (decimal) rd["ListPrice"]
                });
            }
            rd.Close();
            return liste;
        }
        private static List<Personne> Get_Person(CommandLine command)
        {
            var cmd = Connexion();
            if (command.LesParametres.Contains("City"))
            {
                var city = command.LesValeurs[command.LesParametres.IndexOf("City")];
                cmd.CommandText = $@"SELECT Person.Person.BusinessEntityID, Person.Person.FirstName, Person.Person.LastName, Person.Address.City
                                       FROM            Person.Address INNER JOIN
                                                            Person.BusinessEntityAddress ON Person.Address.AddressID = Person.BusinessEntityAddress.AddressID INNER JOIN
                                                            Person.BusinessEntity ON Person.BusinessEntityAddress.BusinessEntityID = Person.BusinessEntity.BusinessEntityID INNER JOIN
                                                            Person.Person ON Person.BusinessEntity.BusinessEntityID = Person.Person.BusinessEntityID
                                       WHERE        (Person.Address.City = N'{city}')";
            }
            else
                cmd.CommandText = $@"SELECT Person.Person.BusinessEntityID, Person.Person.FirstName, Person.Person.LastName, Person.Address.City
                                       FROM            Person.Address INNER JOIN
                                                            Person.BusinessEntityAddress ON Person.Address.AddressID = Person.BusinessEntityAddress.AddressID INNER JOIN
                                                            Person.BusinessEntity ON Person.BusinessEntityAddress.BusinessEntityID = Person.BusinessEntity.BusinessEntityID INNER JOIN
                                                            Person.Person ON Person.BusinessEntity.BusinessEntityID = Person.Person.BusinessEntityID";
            var rd = cmd.ExecuteReader();
            var liste = new List<Personne>();
            while (rd.Read())
            {
                liste.Add(new Personne { Id = (int)rd["BusinessEntityID"], Nom = (string)rd["LastName"], Prenom = (string)rd["FirstName"], Ville = rd["City"] as string });
            }
            rd.Close();
            return liste;
        }
        #endregion

    }
}
