using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	public class Neighborhood
	{
		public City City { get; }

		public List<Structure> structures = new List<Structure>();

		public Neighborhood(City city)
		{
			City = city;
		}

		public void Generate()
		{

		}
	}

}