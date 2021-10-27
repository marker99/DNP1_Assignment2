using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
	public interface IPersonHandler {
		Task<bool> NewFamilyAsync(Family newFamily);
		Task RemoveFamilyAsync(string streetName, int houseNumber);
		Task<IList<Family>> GetFamiliesAsync( );
		Task UpdateFamilyAsync(Family updatedFamily);
		bool Exists(Family family);

		//void NewAdult(Adult newAdult);
		//void RemoveAdult(int id);
		//Adult GetAdult(int id);
		Task UpdateAdultAsync(Adult updatedAdult);
		Task<IList<Adult>> GetAdultsAsync( );
	}
}