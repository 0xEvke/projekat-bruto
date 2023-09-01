using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace WebApplication2.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();

        public string successMessage = "";
        public string errorMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            clientInfo.ime_prezime = Request.Form["ime"];
            clientInfo.adresa = Request.Form["adresa"];
            clientInfo.radno_mesto = Request.Form["radno_mesto"];
            clientInfo.bruto_neto = Request.Form["bruto_neto"];

            if (string.IsNullOrWhiteSpace(clientInfo.ime_prezime) || string.IsNullOrWhiteSpace(clientInfo.adresa) ||
                string.IsNullOrWhiteSpace(clientInfo.radno_mesto) || string.IsNullOrWhiteSpace(clientInfo.bruto_neto))
            {
                errorMessage = "Mora u sve da se napise";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-5EK9152\\MSSQLSERVER01;Initial Catalog=dd;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " +
                                "(ime_prezime, adresa, radno_mesto, bruto_neto) VALUES " +
                                "(@ime_prezime, @adresa, @radno_mesto, @bruto_neto);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ime_prezime", clientInfo.ime_prezime);
                        command.Parameters.AddWithValue("@adresa", clientInfo.adresa);
                        command.Parameters.AddWithValue("@radno_mesto", clientInfo.radno_mesto);
                        command.Parameters.AddWithValue("@bruto_neto", clientInfo.bruto_neto);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Novi klijent je dodat";

                clientInfo.ime_prezime = "";
                clientInfo.adresa = "";
                clientInfo.radno_mesto = "";
                clientInfo.bruto_neto = "";
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
