using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;

namespace WebClient.Data.Implementations
{
	public class RestClient
	{
		private static readonly string URL = "localhost:5001";

		// public async Task<IList<Family>> GetFamiliesAsync(string streetname, int? housenumber)
		//{
		//	using HttpClient client = new();

		//	HttpResponseMessage response;
		//	if (streetname != null)
		//	{
		//		if (housenumber != null)
		//		{
		//			response = await client.GetAsync($"{URL}/Family?street={streetname}&houseNumber={housenumber}");
		//		}
		//		else
		//		{
		//			response = await client.GetAsync($"{URL}/Family?street={streetname}");
		//		}
		//	}
		//	else
		//	{
		//		response = await client.GetAsync($"{URL}/Family");
		//	}

		//	if (!response.IsSuccessStatusCode)
		//	{
		//		throw new Exception($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
		//	}

		//	string result = await response.Content.ReadAsStringAsync();
		//	IList<Family> families = JsonSerializer.Deserialize<IList<Family>>(result, new JsonSerializerOptions
		//	{
		//		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		//	});

		//	return families;
		//}

		public async Task<IList<Adult>> GetAllAdults()
		{
			using HttpClient c = new();
			HttpResponseMessage h = await c.GetAsync($"{URL}/Family");
			string r = await h.Content.ReadAsStringAsync();
			IList<Adult> adults = JsonSerializer.Deserialize<IList<Adult>>(r,
				new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
			return adults;
		}
		public async Task<IList<Adult>> RemoveAdult()
		{
			using HttpClient c = new();
			HttpResponseMessage h = await c.GetAsync($"{URL}/Family");
			string r = await h.Content.ReadAsStringAsync();
			IList<Adult> adults = JsonSerializer.Deserialize<IList<Adult>>(r,
				new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
			return adults;
		}

		
	}
}