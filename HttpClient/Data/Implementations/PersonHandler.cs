using System.Collections.Generic;
using System.Linq;
using HttpClient.Persistence;
using Models;

namespace HttpClient.Data.Implementations
{
    public class PersonHandler : IPersonHandler
    {
        private static FileContext _fileContext;

        public PersonHandler()
        {
            _fileContext = new();
        }

        public void NewAdult(Adult newAdult)
        {
            int id = _fileContext.Adults.Max(adult => adult.Id);
            newAdult.Id = ++id;
            _fileContext.Adults.Add(newAdult);
            _fileContext.SaveChanges();
        }

        public void RemoveAdult(int id)
        {
            Adult adultToRemove = _fileContext.Adults.First(adult => adult.Id == id);
            int idx = _fileContext.Adults.IndexOf(adultToRemove);
            _fileContext.Adults.RemoveAt(idx);
            _fileContext.SaveChanges();
        }

        public Adult GetAdult(int id)
        {
            Adult a = _fileContext.Adults.FirstOrDefault(adult => adult.Id == id);
            return a;
        }

        public void UpdateAdult(Adult updatedAdult)
        {
            Adult a = GetAdult(updatedAdult.Id);
            int idx = _fileContext.Adults.IndexOf(a);
            _fileContext.Adults.RemoveAt(idx);
            _fileContext.Adults.Insert(idx, updatedAdult);
            _fileContext.SaveChanges();
        }

        public IList<Adult> LoadAdults()
        {
            return _fileContext.Adults;
        }
    }
}