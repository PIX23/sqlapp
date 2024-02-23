using sqlapp.Models;
using System.Data.SqlClient;
using System.Runtime;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appazserver.database.windows.net";
        private static string db_user = "MhatreP";
        private static string db_password = "SQL@410206";
        private static string db_database = "appdb";


        public SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;
            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;


        }
    }
}
