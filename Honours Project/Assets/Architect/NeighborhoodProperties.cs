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

		public List<List<double>> transitionMatrix; // The probabilty of each row must sum to 1.0

		public List<StructureType> cannotEndWith = new List<StructureType>
		{
			StructureType.Bridge
		};

		public NeighborhoodProperties(CityProperties cityProperties)
		{
			width = Random.Range(cityProperties.minWidth, cityProperties.maxWidth);
			this.minHeight = cityProperties.minHeight;
			this.maxHeight = cityProperties.maxHeight;

			// TEMP: Default transition matrix
			transitionMatrix = new List<List<double>>
			{
				new List<double> {0.0f, 0.8f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f},
				new List<double> {0.0f, 0.5f, 0.2f, 0.2f, 0.0f, 0.0f, 0.1f},
				new List<double> {0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
				new List<double> {0.0f, 0.6f, 0.0f, 0.0f, 0.3f, 0.1f, 0.0f},
				new List<double> {0.0f, 0.0f, 0.0f, 0.8f, 0.0f, 0.0f, 0.2f},
				new List<double> {0.0f, 0.0f, 0.0f, 0.3f, 0.0f, 0.0f, 0.7f},
				new List<double> {0.0f, 0.5f, 0.0f, 0.0f, 0.1f, 0.4f, 0.0f}
			};
		}
	}
}