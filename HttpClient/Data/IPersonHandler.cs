using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace WebClient.Data
{
	public interface IPersonHandler {
		//Task<bool> NewFamilyAsync(Family newFamily);
		//Task RemoveFamilyAsync(string streetName, int houseNumber);
		//Task<IList<Family>> GetFamiliesAsync(string street, int? houseNumber );
		//Task UpdateFamilyAsync(Family updatedFamily);
		Task UpdateAdultAsync(Adult updatedAdult);
		Task<IList<Adult>> GetAdultsAsync( );
	}

}