using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.FileData{
	public class FileContext {
		public IList<Adult> Adults {
			get; private set;
		}

		private readonly string _file = "adults.json";

		public FileContext( ) {
			Adults = File.Exists(_file) ? ReadData<Adult>(_file) : new List<Adult>( );
		}

		private IList<T> ReadData<T>(string s) {
			using (var jsonReader = File.OpenText(s)) {
				return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd( ));
			}
		}

		public void SaveChanges( ) {
			string jsonFamilies = JsonSerializer.Serialize(Adults, new JsonSerializerOptions { WriteIndented = true });
			using (StreamWriter outputFile = new StreamWriter(_file, false)) {
				outputFile.Write(jsonFamilies);
			}
		}
	}
}