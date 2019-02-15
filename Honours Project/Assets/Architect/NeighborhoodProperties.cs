using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class NeighborhoodProperties
	{
		public int width;
		public float minHeight;
		public float maxHeight;

		public NeighborhoodProperties(CityProperties cityProperties)
		{
			width = Random.Range(cityProperties.minWidth, cityProperties.maxWidth);
			this.minHeight = cityProperties.minHeight;
			this.maxHeight = cityProperties.maxHeight;
		}
	}
}