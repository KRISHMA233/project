using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace project.Pages.client
{
    public class createModel : PageModel
    {
        //object of clientinfo
        public clientinfo clientinfo=new clientinfo();
        //if feild empty then error message
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            clientinfo.name= Request.Form["name"];
            clientinfo.email= Request.Form["email"];
            clientinfo.phone =Request.Form["phone"];
            clientinfo.address =Request.Form["address"];

            if (clientinfo.name.Length == 0 || clientinfo.email.Length == 0 ||
            clientinfo.phone.Length == 0 || clientinfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save the new client into the database
            try
            {
                //database connection to store
                string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))//sql connection
                {
                    connection.Open();
                    String sql = @"
                INSERT INTO client([name], [email], [phone], [address]) 
                values (@name,@email,@phone,@address)";

                    
                   
                    using (SqlCommand command = new SqlCommand(sql, connection))//execute sql query with connection
                    {
                        command.Parameters.AddWithValue("@name", clientinfo.name);//replace @name param with data from form
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@phone", clientinfo.phone);
                        command.Parameters.AddWithValue("@address", clientinfo.address);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }
            clientinfo.name = ""; clientinfo.email = ""; clientinfo.phone = ""; clientinfo.address = "";
            successMessage = "New Student Added Correctly";

           
            }
        }
    }
