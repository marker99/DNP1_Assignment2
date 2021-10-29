using System.Collections.Generic;
using Models;
using WebAPI.Models;
using WebAPI.Models.Models;

namespace WebAPI.Models {

public class Child : Person {

	public List<Interest> Interests {
		get; set;
	}
	public List<Pet> Pets {
		get; set;
	}
}
}