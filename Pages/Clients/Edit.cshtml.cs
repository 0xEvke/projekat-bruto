using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-5EK9152\\MSSQLSERVER01;Initial Catalog=dd;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.ime_prezime = reader.GetString(1);
                                clientInfo.adresa = reader.GetString(2);
                                clientInfo.radno_mesto = reader.GetString(3);
                                clientInfo.bruto_neto = reader.GetString(4);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.ime_prezime = Request.Form["ime_prezime"];
            clientInfo.adresa = Request.Form["adresa"];
            clientInfo.radno_mesto = Request.Form["radno_mesto"];
            clientInfo.bruto_neto = Request.Form["bruto_neto"];

            try
            {
                String connectionString = "Data Source=DESKTOP-5EK9152\\MSSQLSERVER01;Initial Catalog=dd;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET ime_prezime=@ime_prezime, adresa=@adresa, radno_mesto=@radno_mesto, bruto_neto=@bruto_neto" +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ime_prezime", clientInfo.ime_prezime);
                        command.Parameters.AddWithValue("@adresa", clientInfo.adresa);
                        command.Parameters.AddWithValue("@radno_mesto", clientInfo.radno_mesto);
                        command.Parameters.AddWithValue("@bruto_neto", clientInfo.bruto_neto);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
