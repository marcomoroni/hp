using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Architect
{
	[AddComponentMenu("Architect/Neighborhood")]
	public class Neighborhood : MonoBehaviour
	{
		public NeighborhoodProperties properties;

		[Header("Prefabs")]
		public GameObject structurePrefab;

		private enum StructureType
		{
			Start,
			Normal,
			Bridge,
			//BridgeWithMoreOnTop,
			Empty
		}

		public List<List<double>> transitionMatrix; // The probabilty of each row must sum to 1.0

		private List<StructureType> cannotEndWith = new List<StructureType>
		{
			StructureType.Bridge
		};

		private List<StructureProperties> structures = new List<StructureProperties>();

		public int CurrentLenght
		{
			get { return structures.Sum(s => s.width); }
		}

		// Generate as a Markov chain
		public void Generate()
		{
			//CreateStructure();

			// Create transition matrix
			transitionMatrix = CreateTranstionMatrix();

			MarkovChain<StructureType> structureChain = new MarkovChain<StructureType>(transitionMatrix);

			// Generate until lenght is surpassed and has valid ending
			do
			{
				StructureType newStructureType = structureChain.GenerateNext(); // Not using this for now. Will affect CreateStructure()

				var newStructure = CreateStructure(newStructureType, CurrentLenght);
				structures.Add(newStructure.Item2.properties);
			}
			while (CurrentLenght <= properties.width || cannotEndWith.Contains(structureChain[structureChain.Count - 1]));
		}

		private List<List<double>> CreateTranstionMatrix()
		{
			List<List<double>> output = new List<List<double>>
			{
				new List<double> {0.0f, 1.0f, 0.0f, 0.0f},
				new List<double> {0.0f, 0.6f, 0.2f, 0.2f},
				new List<double> {0.0f, 1.0f, 0.0f, 0.0f},
				new List<double> {0.0f, 0.9f, 0.0f, 0.1f}
			};

			return output;
		}

		private (GameObject, Structure) CreateStructure(StructureType type, int pos)
		{
			GameObject go = Instantiate(structurePrefab, transform.position.With(x: transform.position.x + pos * (float)City.pixelsPerUnit / 100), transform.rotation);
			go.name = structurePrefab.name;
			Structure structure = go.GetComponent<Structure>();
			structure.properties = new StructureProperties(properties);
			structure.Generate();

			return (go, structure);
		}

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				Gizmos.color = new Color(1, 0.5f, 0);

				float actualWidth = properties.width * (float)City.pixelsPerUnit / 100;
				float actualMinHeight = properties.minHeight * (float)City.pixelsPerUnit / 100;
				float actualMaxHeight = properties.maxHeight * (float)City.pixelsPerUnit / 100;

				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualMinHeight / 2), new Vector3(actualWidth, actualMinHeight, 0));
				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualMaxHeight / 2), new Vector3(actualWidth, actualMaxHeight, 0));
			}
		}
	}
}