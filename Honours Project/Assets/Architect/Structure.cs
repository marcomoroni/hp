using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Architect
{
	[AddComponentMenu("Architect/Structure")]
	public class Structure : MonoBehaviour
	{
		public StructureProperties properties;

		// Generate as an L-system
		public void Generate()
		{
			// Load all blocks (maybe move it in city or somewhere else with a satic method)
			Object[] blockPVs = Resources.LoadAll("Blocks");

			CreateBlock(0, blockPVs);
		}

		private (GameObject, Block) CreateBlock(float posY, Object[] blockPVs)
		{
			List<Object> validCandidates = new List<Object>();

			for (int i = 0; i < blockPVs.Length; i++)
			{
				BlockProperties b = ((GameObject)blockPVs[i]).GetComponent<Block>().properties;

				if (b.width == properties.width)
				{
					validCandidates.Add(blockPVs[i]);
				}
			}

			if (validCandidates.Count > 0)
			{
				Object candidateChosen = validCandidates.GetRandom();
				GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(candidateChosen);
				go.name = ((GameObject)candidateChosen).name;
				go.transform.parent = this.gameObject.transform;
				go.transform.position = go.transform.parent.transform.position + new Vector3(0, posY * (float)City.pixelsPerCityUnit / 100, 0);
				Block block = go.GetComponent<Block>();

				return (go, block);
			}

			return (null, null);
		}

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				Gizmos.color = new Color(1, 1, 0);

				float actualWidth = properties.width * (float)City.pixelsPerCityUnit / 100;
				float actualHeight = (float)properties.height / 100;

				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualHeight / 2), new Vector3(actualWidth, actualHeight, 0));
			}
		}
	}
}