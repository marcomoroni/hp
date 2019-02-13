using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[AddComponentMenu("Architect/City")]
	public class City : MonoBehaviour
	{
		[Header("Properties")]
		[Tooltip("Leave it null to create random properties.")]
		public CityProperties properties;

		[Header("Prefabs")]
		public GameObject neighborhood;

		public void Generate()
		{
			// TEMP: Generate 1 neighborhood
			// ...
		}
	}
}