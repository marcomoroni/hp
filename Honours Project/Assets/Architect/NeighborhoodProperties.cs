using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class NeighborhoodProperties
	{
		public int width;
		public int minHeight;
		public int maxHeight;

		public List<List<int>> transitionMatrix;

		public List<StructureType> cannotEndWith = new List<StructureType>
		{
			StructureType.Bridge
		};

		public NeighborhoodProperties(CityProperties cityProperties)
		{
			width = Random.Range(cityProperties.minWidth, cityProperties.maxWidth);
			this.minHeight = cityProperties.minHeight;
			this.maxHeight = cityProperties.maxHeight;

			// Default transition matrix (sum of each row is 100)
			transitionMatrix = new List<List<int>>
			{
				new List<int> {0, 70, 0, 0, 20, 10, 0},
				new List<int> {0, 50, 20, 20, 0, 0, 10},
				new List<int> {0, 100, 0, 0, 0, 0, 0},
				new List<int> {0, 60, 0, 0, 30, 10, 0},
				new List<int> {0, 0, 0, 80, 0, 0, 20},
				new List<int> {0, 0, 0, 30, 0, 0, 70},
				new List<int> {0, 50, 0, 0, 10, 40, 0}
			};

			// Vegetation
			int vegetation = cityProperties.vegetation;
			transitionMatrix[0][4] = Mathf.Max(0, 20 + vegetation);
			transitionMatrix[0][5] = Mathf.Max(0, 10 + vegetation);
			transitionMatrix[3][4] = Mathf.Max(0, 30 + vegetation);
			transitionMatrix[3][5] = Mathf.Max(0, 10 + vegetation);
			transitionMatrix[6][4] = Mathf.Max(0, 10 + vegetation);
			transitionMatrix[6][5] = Mathf.Max(0, 40 + vegetation);

			// Bridges
			transitionMatrix[1][2] = Mathf.Max(0, 20 + cityProperties.bridges);

			// Density
			int density = cityProperties.density;
			transitionMatrix[1][3] = Mathf.Max(0, 20 + density);
			transitionMatrix[1][6] = Mathf.Max(0, 10 + density);
			transitionMatrix[4][3] = Mathf.Max(0, 80 + density);
			transitionMatrix[4][6] = Mathf.Max(0, 20 + density);
			transitionMatrix[5][3] = Mathf.Max(0, 30 + density);
			transitionMatrix[5][6] = Mathf.Max(0, 70 + density);
		}
	}
}