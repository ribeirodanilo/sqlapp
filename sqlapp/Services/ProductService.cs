using System.Data.SqlClient;
using sqlapp.Models;

namespace sqlapp.Services;

public class ProductService
{

    private static string db_source = "appserver-1.database.windows.net";
    private static string db_user = "administratordr";
    private static string db_password = "Pa55w.rdPa55w.rd";
    private static string db_database = "appdb";

    private readonly IConfiguration _configuration;

    public ProductService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection GetConnection()
    {

        // Utilizar este codigo para ConnectionString:
        return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));

        var _builder = new SqlConnectionStringBuilder();
        _builder.DataSource = db_source;
        _builder.UserID = db_user; 
        _builder.Password = db_password;    
        _builder.InitialCatalog = db_database;
        return new SqlConnection(_builder.ConnectionString);
    }

    public List<Product> GetProducts()
    {
        SqlConnection conn = GetConnection();

        List<Product> _produtct_list = new List<Product>();

        string statement = "SELECT ProductID, ProductName, QUantity from Products";

        conn.Open();

        SqlCommand cmd = new SqlCommand(statement, conn);

        using (SqlDataReader reader = cmd.ExecuteReader())
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
