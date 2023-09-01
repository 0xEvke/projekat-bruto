using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; 

namespace WebApplication2.Pages.Clients
{
	public class ClientsModel : PageModel
	{
		public List<ClientInfo> listClients = new List<ClientInfo>();
		private const string ApiBaseUrl = "http://data.fixer.io/api/\r\n"; 
		private const string ApiKey = "a794dd74ee19a562c77544ddfe5606a9";

		public async Task<IActionResult> OnGetAsync()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-5EK9152\\MSSQLSERVER01;Initial Catalog=dd;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM clients";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								ClientInfo clientInfo = new ClientInfo();
								clientInfo.id = reader.GetInt32(0).ToString();
								clientInfo.ime_prezime = reader.GetString(1);
								clientInfo.adresa = reader.GetString(2);
								clientInfo.radno_mesto = reader.GetString(3);
								clientInfo.bruto_neto = reader.GetString(4);

								// poziv za konverziju bruto iznosa u odredjene valute
								clientInfo.prva_vrednost = await ConvertToCurrencyAsync(Convert.ToDecimal(clientInfo.bruto_neto), "EUR"); // konverzija u EUR

								listClients.Add(clientInfo);
							}
						}
					}
				}

				using (var package = new ExcelPackage())
				{
					var worksheet = package.Workbook.Worksheets.Add("ClientInfo");
					worksheet.Cells.LoadFromCollection(listClients, true);

					using (var stream = new MemoryStream())
					{
						package.SaveAs(stream);
						stream.Position = 0;

						return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "info.xlsx");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());

				return Page();
			}
		}

		public async Task<decimal> ConvertToCurrencyAsync(decimal amountInRSD, string targetCurrency)
		{
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);

				var queryString = $"?base=RSD&symbols={targetCurrency}";

				var response = await httpClient.GetAsync(ApiBaseUrl + queryString);

				if (response.IsSuccessStatusCode)
				{
					var responseContent = await response.Content.ReadAsStringAsync();
					var exchangeRates = JsonConvert.DeserializeObject<ExchangeRatesResponse>(responseContent);

					if (exchangeRates.Rates.TryGetValue(targetCurrency, out decimal exchangeRate))
					{
						// bruto iznos u nekoj valuti
						decimal calculatedAmount = amountInRSD * exchangeRate;
						return calculatedAmount;
					}
					else
					{
						throw new Exception($"Kurs za valutu {targetCurrency} nije pronadjen.");
					}
				}
				else
				{
					throw new Exception("nece.");
				}
			}
		}
	}

	public class ClientInfo
	{
		public string id { get; set; }
		public string ime_prezime { get; set; }
		public string adresa { get; set; }
		public string radno_mesto { get; set; }
		public string bruto_neto { get; set; }
		public decimal prva_vrednost { get; set; }
	}

	public class ExchangeRatesResponse
	{
		public Dictionary<string, decimal> Rates { get; set; }
	}
}
