using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	//[CreateAssetMenu(fileName = "NewCityProperties", menuName = "Architect/City Properties", order = 1)]
	public class CityProperties// : ScriptableObject
	{
		//[Tooltip("In pixels.")]
		public int minWidth = 300;
		//[Tooltip("In pixels.")]
		public int maxWidth = 600;

		//[Tooltip("In pixels.")]
		public int minHeight = 200;
		//[Tooltip("In pixels.")]
		public int maxHeight = 500;

		public ArchitecturalStyle style; // NOT IMPLEMENTED

		//[Tooltip("Number of neighborhoods.")]
		//public int neighborhoods = 5; // z-axis

		public int minNeighbourhoods = 2;
		public int maxNeighbourhoods = 5;

		public int vegetation = 0; // 0 normal

		public float neighbourhoodXScatter = 6f;


		public void Randomize()
		{

		}
	}
}