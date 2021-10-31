using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebAPI.FileData;
using WebAPI.Models;

namespace WebAPI.Data.Implementations
{
	public class PersonHandler : IPersonHandler {
		private static FileContext _fileContext;

		public PersonHandler( ) {
			_fileContext = new( );
		}

		public async Task NewAdultAsync(Adult newAdult)
		{
			int maxID = _fileContext.Adults.Max(a => a.Id);
			newAdult.Id = maxID + 1;
			_fileContext.Adults.Add(newAdult);
			_fileContext.SaveChanges();
		}

		public async Task RemoveAdultAsync(int id)
		{
			Adult a = _fileContext.Adults.FirstOrDefault(a => a.Id == id);
			if (a != null)
			{
				_fileContext.Adults.Remove(a);
				_fileContext.SaveChanges();
			}
		}

		public async Task<Adult> GetAdultAsync(int id)
		{
			return _fileContext.Adults.FirstOrDefault(a => a.Id == id);
		}

		public async Task UpdateAdultAsync(Adult updatedAdult) {
			Adult a = _fileContext.Adults.FirstOrDefault(a => a.Id == updatedAdult.Id);
			if (a == null) {
				await NewAdultAsync(updatedAdult);
			}
			else
			{
				await UpdateNonMatching(a, updatedAdult);
			}

			_fileContext.SaveChanges( );
		}

		public async Task<IList<Adult>> GetAdultsAsync( ) {
			return _fileContext.Adults;
		}

		// Helper Methods
		
		// Updating each individual property for an Adult
		// This all works due to C# being Pass-By-Reference
		private async Task UpdateNonMatching(Adult oldA, Adult newA) {
			oldA.FirstName = newA.FirstName;
			oldA.LastName = newA.LastName;
			oldA.Age = newA.Age;
			oldA.Sex = newA.Sex;
			oldA.Job = newA.Job;
			oldA.HairColor = newA.HairColor;
			oldA.EyeColor = newA.EyeColor;
			oldA.Height = newA.Height;
			oldA.Weight = newA.Weight;
		}
	}
}