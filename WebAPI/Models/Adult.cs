using WebAPI.Models.Models;

namespace WebAPI.Models
{
	public class Adult : Person {
		public Job JobTitle {
			get; set;
		}
	}
}