using System;
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
	public class FamilyController : ControllerBase {
		private IPersonHandler _personHandler;

		public FamilyController(IPersonHandler personHandler) {
			_personHandler = personHandler;
		}

		[HttpGet]
		public async Task<ActionResult<IList<Family>>> Get([FromQuery] string street, [FromQuery] int? houseNumber) {
			try {
				IList<Family> f = await _personHandler.GetFamiliesAsync( );
				if (street != null) {
					f = f.Where(v => v.StreetName == street).ToList( );
				}

				if (houseNumber != null) {
					f = f.Where(v => v.HouseNumber == houseNumber).ToList( );
				}

				return Ok(f);
			} catch (Exception e) {
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpDelete]
		[Route("{street}/{houseNumber:int}")] // THIS IS VERY IMPORTANT
		public async Task<ActionResult> Delete([FromRoute] string street, [FromRoute] int houseNumber) {
			try {
				await _personHandler.RemoveFamilyAsync(street, houseNumber);
				return Ok( );
			} catch (Exception e) {
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpPost] // TODO: Route så man kan ændre vejnavn / husnummer
		public async Task<ActionResult> Post([FromBody] Family family) {
			try {
				bool exists = _personHandler.Exists(family);
				if (exists) {
					await _personHandler.UpdateFamilyAsync(family);
				} else {
					await _personHandler.NewFamilyAsync(family);
				}

				return Ok( );
			} catch (Exception e) {
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpPatch] // TODO: Route så man kan ændre vejnavn / husnummer
		public async Task<ActionResult> Patch([FromBody] Family family) {
			try {
				await _personHandler.UpdateFamilyAsync(family);
				return Ok( );
			} catch (Exception e) {
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult> Put([FromBody] Family family) {
			try {
				bool alreadyExists = await _personHandler.NewFamilyAsync(family);
				if (alreadyExists) {
					return Conflict( );
				}

				return Created($"{family.StreetName}/{family.HouseNumber}", family);
			} catch (Exception e) {
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}
	}
}