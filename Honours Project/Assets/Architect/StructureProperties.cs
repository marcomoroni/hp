using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class StructureProperties
	{
		public float height;
		public int width;

		public StructureProperties(NeighborhoodProperties neighborhoodProperties)
		{
			height = Random.Range(neighborhoodProperties.minHeight, neighborhoodProperties.maxHeight);
			width = Random.Range(1, 2 + 1);
		}
	}
}