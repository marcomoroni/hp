using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	public class NeighborhoodProperties
	{
		public int length;
		public int minHeight;
		public int maxHeight;

		public NeighborhoodProperties(CityProperties cityProperties)
		{
			length = Random.Range(cityProperties.minLength, cityProperties.maxLength);
			this.minHeight = cityProperties.minHeight;
			this.maxHeight = cityProperties.maxHeight;
		}
	}
}