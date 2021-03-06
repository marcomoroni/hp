﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Heights are in pixels (1 px = 1/100 Unity unit)
// Widths are pixelsPerUnit wide

namespace Architect
{
	[AddComponentMenu("Architect/City")]
	public class City : MonoBehaviour
	{
		//[Header("Properties")]
		//[Tooltip("Leave it null to create random properties.")]
		public CityProperties properties;

		[Header("Prefabs")]
		public GameObject neighborhoodPrefab;

		[HideInInspector]
		// <pos, structure>
		private List<(Vector3, Neighborhood)> neighborhoods = new List<(Vector3, Neighborhood)>();



		private void Start()
		{
			properties = GameManagerData.cityProperties;
			Generate();
		}

		public void Generate()
		{
			// If properties is null, generate random one
			/*if (properties == null)
			{
				properties = ScriptableObject.CreateInstance<CityProperties>();
				properties.Randomize();
			}*/

			// Generate neighborhoods
			int numNeighbourhoods = UnityEngine.Random.Range(properties.minNeighbourhoods, properties.maxNeighbourhoods + 1);
			for (int i = 0; i < numNeighbourhoods; i++)
			{
				Vector3 pos = new Vector3(
					Random.Range(-properties.neighbourhoodXScatter, properties.neighbourhoodXScatter),
					0,
					3.0f * i);

				CreateNeighborhood(pos);
			}
		}

		private (GameObject, Neighborhood) CreateNeighborhood(Vector3 pos)
		{
			GameObject go = Instantiate(neighborhoodPrefab, transform.position + pos, transform.rotation);
			go.name = neighborhoodPrefab.name;
			Neighborhood neighborhood = go.GetComponent<Neighborhood>();
			neighborhood.properties = new NeighborhoodProperties(properties);
			neighborhood.Generate();

			return (go, neighborhood);
		}
	}
}