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
		public GameObject neighborhoodPrefab;

		public static readonly int pixelsPerUnit = 64;

		private void Update()
		{
			if (Input.GetKeyDown("space"))
			{
				Generate();
			}
		}

		public void Generate()
		{
			// If properties is null, generate random one
			if (properties == null)
			{
				properties = ScriptableObject.CreateInstance<CityProperties>();
				properties.Randomize();
			}

			// TEMP: Generate 1 neighborhood
			Debug.Log("Generating city");
			CreateNeighborhood();
		}

		private (GameObject, Neighborhood) CreateNeighborhood()
		{
			GameObject go = Instantiate(neighborhoodPrefab, transform.position, transform.rotation);
			go.name = neighborhoodPrefab.name;
			Neighborhood neighborhood = go.GetComponent<Neighborhood>();
			neighborhood.properties = new NeighborhoodProperties(properties);
			neighborhood.Generate();

			return (go, neighborhood);
		}
	}
}