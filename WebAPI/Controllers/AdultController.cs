using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AdultController : ControllerBase {
		private IPersonHandler _personHandler;

		public AdultController(IPersonHandler personHandler) {
			_personHandler = personHandler;
		}

		[HttpGet]
		public async Task<ActionResult<IList<Adult>>> GetAdults([FromQuery] int? adultId) {
			IList<Adult> adults = await _personHandler.GetAdultsAsync( );
			// If an ID was added, sort out all people without that ID
			if (adultId != null) {
				adults = adults.Where(a => a.Id == adultId).ToList( );
			}

			return Ok(adults);
		}

		[HttpPatch]
		[Route("{adultId:int}")]
		public async Task<ActionResult> UpdateAdult([FromRoute] int adultId, [FromBody] Adult updatedAdultInfo) {
			updatedAdultInfo.Id = adultId;
			await _personHandler.UpdateAdultAsync(updatedAdultInfo);
			return Ok( );
		}
	}
}