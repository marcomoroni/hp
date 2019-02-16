using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class StructureProperties
	{
		public int height;
		public int width;

		

		public StructureProperties(NeighborhoodProperties neighborhoodProperties, StructureType structureType)
		{
			height = Random.Range(neighborhoodProperties.minHeight, neighborhoodProperties.maxHeight + 1);
			width = Random.Range(1, 2 + 1);
		}
	}
}