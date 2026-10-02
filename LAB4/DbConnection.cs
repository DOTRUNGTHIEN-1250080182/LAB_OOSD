using System.Data.SqlClient;

namespace EShopping.Data
{
    public static class DbConnection
    {
        private static readonly string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              Initial Catalog=EShoppingDB;
              Integrated Security=True;
              TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}