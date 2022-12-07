using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace project.Pages.client
{
    public class EditModel : PageModel
    {
        //object of clientinfo
        public clientinfo clientinfo = new clientinfo();
        //if feild empty then error message
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {//connect to the database create connection string
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionstring))//sql connection
                {
                    connection.Open();
                    string sql = "SELECT * FROM client WHERE id=@id"; 
                    using (SqlCommand command = new SqlCommand(sql, connection))//execute sql query with connection
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())//data reader
                        {
                            while (reader.Read())
                            {
                                clientinfo.id = Convert.ToString(reader.GetInt32(0));
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);
                                Console.WriteLine(clientinfo.id);
                                Console.WriteLine(clientinfo.name);


                                Console.WriteLine(clientinfo.email);

                            }
                        }
                    }

                }
}
            catch (Exception ex)
            {
              
             errorMessage = ex.Message;
                return;
            }
           // Response.Redirect("/client/Edit");

        }
        public void OnPost()
        {//data received from form
            clientinfo.id = Request.Form["id"];
            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.address = Request.Form["address"];

            if (clientinfo.name.Length == 0 || clientinfo.email.Length == 0 ||
            clientinfo.phone.Length == 0 || clientinfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionstring))//sql connection
                {
                    connection.Open();
                    
                    string sql = @"UPDATE client SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";//data received from form

                    using (SqlCommand command = new SqlCommand(sql, connection))//execute sql query with connection
                    { 
                        command.Parameters.AddWithValue("@name", clientinfo.name);//replace @name param with data from form
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@phone", clientinfo.phone);
                        command.Parameters.AddWithValue("@address", clientinfo.address);
                        command.Parameters.AddWithValue("@id", clientinfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/client/Index");
        }
    }
}
