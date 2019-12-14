﻿    
using System;
using System.Data.SqlClient;
//数据库连接类Connection：如果连接SQLServer数据库，可以使用SqlConnetion类。在使用SqlConnection类是要引用一个System.Data.SqlClient的命名空间
class Sample
{
    static void Main()
    {
        string connectionString = "Server=tcp:LIM-T460S.database.windows.net,1433;Database=tempdb;User ID=sa@LIM-T460S;Password={LIMforever@530};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                        SELECT
                            c.CustomerID
                            ,c.CompanyName
                            ,COUNT(soh.SalesOrderID) AS OrderCount
                        FROM SalesLT.Customer AS c
                        LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID
                        GROUP BY c.CustomerID, c.CompanyName
                        ORDER BY OrderCount DESC;";

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: {0} Name: {1} Order Count: {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    }
                }
            }
        }

        using (var conn = new SqlConnection(connectionString))
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate)
                    OUTPUT INSERTED.ProductID
                    VALUES (@Name, @Number, @Cost, @Price, CURRENT_TIMESTAMP)";

                cmd.Parameters.AddWithValue("@Name", "SQL Server Express");
                cmd.Parameters.AddWithValue("@Number", "SQLEXPRESS1");
                cmd.Parameters.AddWithValue("@Cost", 0);
                cmd.Parameters.AddWithValue("@Price", 0);

                conn.Open();

                int insertedProductId = (int)cmd.ExecuteScalar();

                Console.WriteLine("Product ID {0} inserted.", insertedProductId);
            }
        }
    }
}

