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

		/*//public List<T> elements;
		public List<List<double>> transitionMatrix; // The probabilty of each row must sum to 1.0
		private int currentLenght = 0;
		//public List<T> cannotEndWith = new List<T>();*/

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