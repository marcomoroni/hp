using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[CreateAssetMenu(fileName = "NewCityProperties", menuName = "Architect/City Properties", order = 1)]
	public class CityProperties : ScriptableObject
	{
		[Tooltip("In pixels.")]
		public int minWidth = 200;
		[Tooltip("In pixels.")]
		public int maxWidth = 640;

		[Tooltip("In pixels.")]
		public int minHeight = 600;
		[Tooltip("In pixels.")]
		public int maxHeight = 700;

		public ArchitecturalStyle style; // NOT IMPLEMENTED

		[Tooltip("Number of neighborhoods.")]
		public int neighborhoods = 5; // z-axis


		public void Randomize()
		{

		}
	}
}