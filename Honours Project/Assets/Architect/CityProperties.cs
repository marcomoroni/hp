using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[CreateAssetMenu(fileName = "NewCityProperties", menuName = "Architect/City Properties", order = 1)]
	public class CityProperties : ScriptableObject
	{
		public int minLength = 3;
		public int maxLength = 10;
		public int minHeight = 6;
		public int maxHeight = 12;
		public Color color1 = Color.cyan;

		public void Randomize()
		{
			color1 = Random.ColorHSV();
		}
	}
}