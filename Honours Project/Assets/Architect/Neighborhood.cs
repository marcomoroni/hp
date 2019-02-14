using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[AddComponentMenu("Architect/Neighborhood")]
	public class Neighborhood : MonoBehaviour
	{
		public NeighborhoodProperties properties;

		[Header("Prefabs")]
		public GameObject structurePrefab;

		private int currentLenght = 0;

		// Generate as a Markov chain
		public void Generate()
		{
			Debug.Log("Generating neighborhood");
			CreateStructure();
		}

		public (GameObject, Structure) CreateStructure()
		{
			GameObject go = Instantiate(structurePrefab, transform.position, transform.rotation);
			go.name = structurePrefab.name;
			Structure structure = go.GetComponent<Structure>();
			structure.properties = new StructureProperties(properties);
			structure.Generate();

			return (go, structure);
		}
	}
}