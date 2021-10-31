using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;
using WebClient.Models;

namespace WebClient.Data.Implementations
{
	public class RestClient
	{
		private static readonly string URL = "https://localhost:5001";

		public async Task<IList<Adult>> GetAllAdults()
		{
			using HttpClient c = new();
			HttpResponseMessage h = await c.GetAsync($"{URL}/Adult");
			string r = await h.Content.ReadAsStringAsync();
			IList<Adult> adults = JsonSerializer.Deserialize<IList<Adult>>(r,
				new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
			return adults;
		}

		public async Task<String> RemoveAdultAsync(int adultId)
		{
			using HttpClient c = new();
			HttpResponseMessage h = await c.DeleteAsync($"{URL}/Adult/{adultId}");
			return h.StatusCode.ToString();
		}

		public async Task<String> AddAdultAsync(Adult newAdult)
		{
			using HttpClient client = new();
			string todoAsJson = JsonSerializer.Serialize(newAdult);
			StringContent todoString = new(todoAsJson, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URL}/Adult/{newAdult.Id}", todoString);
			return $"{response.StatusCode} | {response.Content.ReadAsStringAsync( ).Result}";
		}

		public async Task<User> LoginAsync(User user)
		{
			using HttpClient client = new();
			string userAsString = JsonSerializer.Serialize(user);
			StringContent userStringContent = new(userAsString, Encoding.UTF8, "application/json");
			Console.WriteLine("Sending login information");

			// Status er, at den simpelthen ikke kan sende... det er sku da træls
			HttpResponseMessage response = await client.PostAsync($"{URL}/User", userStringContent);
			
			// Doesn't ever reach here... tf?
			Console.WriteLine("User has been posted");
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return user;
			}
			else
			{
				return null;
			}
		}
	}
}