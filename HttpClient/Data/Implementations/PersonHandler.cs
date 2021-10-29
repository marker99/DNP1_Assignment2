using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Persistence;
using Models;

namespace WebClient.Data.Implementations
{
	public class PersonHandler : IPersonHandler
	{
		private RestClient client;

		public PersonHandler()
		{
			client = new();
		}

		//public async Task<bool> NewFamilyAsync(Family newFamily)
		//{
		//	throw new System.NotImplementedException();
		//}

		//public async Task RemoveFamilyAsync(string streetName, int houseNumber)
		//{
		//	throw new System.NotImplementedException();
		//}

		//public async Task<IList<Family>> GetFamiliesAsync(string street, int? houseNumber)
		//{
		//	IList<Family> fams = await client.GetFamiliesAsync(street, houseNumber);
		//	return fams;
		//}

		//public async Task UpdateFamilyAsync(Family updatedFamily)
		//{
		//	throw new System.NotImplementedException();
		//}

		public async Task UpdateAdultAsync(Adult updatedAdult)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IList<Adult>> GetAdultsAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}