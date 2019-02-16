using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[CreateAssetMenu(fileName = "NewCityProperties", menuName = "Architect/City Properties", order = 1)]
	public class CityProperties : ScriptableObject
	{
		[Tooltip("In city units.")]
		public int minWidth = 3;
		[Tooltip("In city units.")]
		public int maxWidth = 10;
		[Tooltip("In pixels.")]
		public int minHeight = 600;
		[Tooltip("In pixels.")]
		public int maxHeight = 700;
		public Color color1 = Color.cyan; // TEMP
		public ArchitecturalStyle style;

		public void Randomize()
		{
			color1 = Random.ColorHSV();
		}
	}
}