using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.FileData;
using WebAPI.Models;

namespace WebAPI.Data.Implementations
{
	public class PersonHandler : IPersonHandler {
		private static FileContext _fileContext;
		private IList<Adult> _adults;

		public PersonHandler( ) {
			_fileContext = new( );
		}

		public async Task<bool> NewFamilyAsync(Family newFamily) {
			Family existingFamily = _fileContext.Families.FirstOrDefault(f =>
				f.StreetName == newFamily.StreetName && f.HouseNumber == newFamily.HouseNumber);
			if (existingFamily == null) {
				return false;
			}
			// START | Fixing IDs for Adults
			await PopulateAdultsAsync( );
			newFamily.Adults.ForEach(a => {
				a.Id = _adults.Max(b => b.Id) + 1;
				PopulateAdultsAsync( );
			});
			// END | Fixing IDs for Adults
			_fileContext.Families.Add(newFamily);
			_fileContext.SaveChanges( );
			return true;
		}

		public async Task RemoveFamilyAsync(string streetName, int houseNumber) {
			Family f = _fileContext.Families.FirstOrDefault(f =>
				f.StreetName == streetName && f.HouseNumber == houseNumber);

			if (f == null) {
				return;
			}

			_fileContext.Families.Remove(f);
			_fileContext.SaveChanges( );
		}

		public async Task<IList<Family>> GetFamiliesAsync( ) {
			return _fileContext.Families;
		}


		public async Task UpdateFamilyAsync(Family updatedFamily) {
			// Get the old Family
			Family oldFamily = _fileContext.Families.FirstOrDefault(f =>
				f.StreetName == updatedFamily.StreetName && f.HouseNumber == updatedFamily.HouseNumber);
			// Get Index in list of Family
			int familyIdx = _fileContext.Families.IndexOf(oldFamily);
			// Remove old Family
			_fileContext.Families.RemoveAt(familyIdx);
			// Insert new Family
			_fileContext.Families.Insert(familyIdx, updatedFamily);
			_fileContext.SaveChanges( );
		}

		public bool Exists(Family family) {
			Family v = _fileContext.Families.FirstOrDefault(f =>
				f.StreetName == family.StreetName && f.HouseNumber == family.HouseNumber);
			return v != null;
		}

		//public void NewAdult(Adult newAdult)
		//{
		//	int id = _fileContext.Adults.Max(adult => adult.Id);
		//	newAdult.Id = ++id;
		//	_fileContext.Adults.Add(newAdult);
		//	_fileContext.SaveChanges();
		//}

		//public void RemoveAdult(int id)
		//{
		//	Adult adultToRemove = _fileContext.Adults.First(adult => adult.Id == id);
		//	int idx = _fileContext.Adults.IndexOf(adultToRemove);
		//	_fileContext.Adults.RemoveAt(idx);
		//	_fileContext.SaveChanges();
		//}

		//public Adult GetAdult(int id)
		//{
		//	Adult a = _fileContext.Adults.FirstOrDefault(adult => adult.Id == id);
		//	return a;
		//}

		public async Task UpdateAdultAsync(Adult updatedAdult) {
			await PopulateAdultsAsync( );
			Adult a = _adults.FirstOrDefault(a => a.Id == updatedAdult.Id);
			if (a == null) {
				return;
			}
			await UpdateNonMatching(a, updatedAdult);
			_fileContext.SaveChanges( );
		}

		public async Task<IList<Adult>> GetAdultsAsync( ) {
			await PopulateAdultsAsync( );
			return _adults;
		}

		// Helper Methods

		// This method is purely for ensuring the Adult list is constantly up to date with the Families
		private async Task PopulateAdultsAsync( ) {
			_adults = new List<Adult>( );
			IList<Family> families = await GetFamiliesAsync( );
			foreach (Family f in families) {
				f.Adults.ForEach(f => _adults.Add(f));
			}
		}

		// Updating each individual property for an Adult
		// This all works due to C# being Pass-By-Reference
		private async Task UpdateNonMatching(Adult oldA, Adult newA) {
			oldA.FirstName = newA.FirstName;
			oldA.LastName = newA.LastName;
			oldA.Age = newA.Age;
			oldA.Sex = newA.Sex;
			oldA.JobTitle = newA.JobTitle;
			oldA.HairColor = newA.HairColor;
			oldA.EyeColor = newA.EyeColor;
			oldA.Height = newA.Height;
			oldA.Weight = newA.Weight;
		}
	}
}