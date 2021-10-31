using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Assigment1._5.Data
{
	public interface IPersonHandler
	{
		Task NewAdult(Adult newAdult);
		Task<String> RemoveAdult(int id);
		Task<Adult> GetAdult(int id);
		Task UpdateAdult(Adult updatedAdult);
		Task<IList<Adult>> LoadAdults();
	}
}