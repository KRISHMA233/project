using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
//model
namespace project.Pages.client
{
    public class IndexModel : PageModel
    {//to store all data we need list
        public List<clientinfo> listClients= new List<clientinfo>();
        public void OnGet()//method is executed when we access tothe page using http get method and... we need to access to the database and to read data from the people table
        {
            try
            {//connect to the database create connection string
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                    
                    using (SqlConnection connection = new SqlConnection(connectionstring))//sql connection
                {
                    connection.Open();
                    string sql = "SELECT * FROM client";//create sql query to read data
                    using (SqlCommand command = new SqlCommand(sql, connection))//execute sql query with connection
                    {
                        using (SqlDataReader reader = command.ExecuteReader())//data reader
                        {
                            while (reader.Read())
                            {
                                clientinfo clientinfo = new clientinfo();
                                clientinfo.id = Convert.ToString(reader.GetInt32(0));
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);
                                

                                listClients.Add(clientinfo);
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
        
    }
    //create class called clientinfo that will store 1 client data from database
    public class clientinfo
    {
        public string? id;
        public string? name;
        public string? email;
        public string? phone;
        public string? address;
       
    }
}

