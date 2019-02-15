using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[CreateAssetMenu(fileName = "NewCityProperties", menuName = "Architect/City Properties", order = 1)]
	public class CityProperties : ScriptableObject
	{
		public int minWidth = 3;
		public int maxWidth = 10;
		public float minHeight = 6f;
		public float maxHeight = 12f;
		public Color color1 = Color.cyan;

		public void Randomize()
		{
			color1 = Random.ColorHSV();
		}
	}
}