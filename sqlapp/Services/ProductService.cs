using sqlapp.Models;
using MySql.Data.MySqlClient;

namespace sqlapp.Services;

public class ProductService
{

    private static string db_source = "appserver-1.database.windows.net";
    private static string db_user = "administratordr";
    private static string db_password = "Pa55w.rdPa55w.rd";
    private static string db_database = "appdb";

    //private readonly IConfiguration _configuration;

    //public ProductService(IConfiguration configuration)
    //{
    //  _configuration = configuration;
    //}

    private MySqlConnection GetConnection()
    {

        // Utilizar este codigo para ConnectionString:
        //SQLConnection = "Server=tcp:appserver-1.database.windows.net,1433;Initial Catalog=appdb;Persist Security Info=False;User ID=administratordr;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        //return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));

        /* Utilizar este codigo para SQLServer
             var _builder = new SqlConnectionStringBuilder();
            _builder.Server = db_source;
            _builder.UserID = db_user; 
            _builder.Password = db_password;    
            _builder.Database = db_database;
            return new SqlConnection(_builder.ConnectionString);          
         */

        // Utilizar este codigo para MySQL como servico Azure
        // string connectionString = "Server=mysqlserver-302.mysql.database.azure.com; Port=3306; Database=appdb; Uid=administratordr@mysqlserver-302; Pwd=Pa55w.rdPa55w.rd; SslMode=Preferred;";

        // utilizar este string como container
        //string connectionString = "Server=20.243.68.245; Port=3306; Database=appdb; Uid=root; Pwd=Pa55w.rdPa55w.rd; SslMode=Preferred;";

        // utilizar este string como Container Group
        //string connectionString = "Server=localhost; Port=3306; Database=appdb; Uid=root; Pwd=Pa55w.rdPa55w.rd; SslMode=Preferred;";

        // Utilizar este string como Kubernets (onde mysql eh o nome da instancia kubernets para o db)
        string connectionString = "Server=mysql; Port=3306; Database=appdb; Uid=root; Pwd=Pa55w.rdPa55w.rd; SslMode=Preferred;";

        return new MySqlConnection(connectionString);
    }

    public List<Product> GetProducts()
    {
        MySqlConnection conn = GetConnection();

        List<Product> _produtct_list = new List<Product>();

        string statement = "SELECT ProductID, ProductName, QUantity from Products";

        conn.Open();

        MySqlCommand cmd = new MySqlCommand(statement, conn);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while(reader.Read())
            {
                Product product = new Product()
                {
                    ProductID = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    Quantity = reader.GetInt32(2)
                };
                _produtct_list.Add(product);
            }
        }
        conn.Close();
        return _produtct_list;
    }

}
