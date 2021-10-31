using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebClient.Data.Implementations
{
	public class PersonHandler : IPersonHandler
	{
		private static RestClient _c;

		public PersonHandler()
		{
			_c = new();
		}

		public async Task NewAdult(Adult newAdult)
		{
			await _c.AddAdultAsync(newAdult);
			
		}

		public async Task<String> RemoveAdult(int id)
		{
			Console.WriteLine("PersonHandler.cs | Removing Person with ID: " + id);
			return await _c.RemoveAdultAsync(id);
		}

		public async Task<Adult> GetAdult(int id)
		{
			IList<Adult> a = await _c.GetAllAdults();
			return a.FirstOrDefault(a=>a.Id==id);
		}

		public async Task UpdateAdult(Adult updatedAdult)
		{
			await _c.AddAdultAsync(updatedAdult);
		}

		public async Task<IList<Adult>> LoadAdults()
		{
			return await _c.GetAllAdults();
		}
	}
}