using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	public class StructureProperties
	{
		public int height;
		public int width;

		public StructureProperties(NeighborhoodProperties neighborhoodProperties)
		{
			height = Random.Range(neighborhoodProperties.minHeight, neighborhoodProperties.maxHeight);
			width = Random.Range(1, 5);
		}
	}
}