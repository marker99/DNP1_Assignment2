using System.Collections.Generic;
using Models;

namespace HttpClient.Data
{
    public interface IPersonHandler
    {
        void NewAdult(Adult newAdult);
        void RemoveAdult(int id);
        Adult GetAdult(int id);
        void UpdateAdult(Adult updatedAdult);
        IList<Adult> LoadAdults();
    }
}