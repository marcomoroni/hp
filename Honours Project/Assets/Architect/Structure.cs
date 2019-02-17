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

		// L-System symbol
		private class Symbol { }
		// Conceptually different for terminal definition: for this implementation,
		// terminals must have a block to calculate height
		interface ITerminalSymbol
		{
			Object BlockPrefabVariant { get; }
		}

		// Symbols
		class S_F_Roof : Symbol, ITerminalSymbol
		{
			public Object BlockPrefabVariant { get; }

			public S_F_Roof (StructureProperties properties)
			{
				BlockPrefabVariant = ArchitectTools.FindValidBlockPrefabVariant(properties, BlockCategory.Roof);
			}
		}





		// Generate as an L-system
		public void Generate()
		{
			CreateBlock(ArchitectTools.FindValidBlockPrefabVariant(properties, BlockCategory.Generic), 0);
		}

		private (GameObject, Block) CreateBlock(Object blockPrefabVariant, float posY)
		{
			GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(blockPrefabVariant);
			go.name = ((GameObject)blockPrefabVariant).name;
			go.transform.parent = this.gameObject.transform;
			go.transform.position = go.transform.parent.transform.position + new Vector3(0, posY * (float)ArchitectTools.pixelsPerCityUnit / 100, 0);
			Block block = go.GetComponent<Block>();

			return (go, block);
		}

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				Gizmos.color = new Color(1, 1, 0);

				float actualWidth = properties.width * (float)ArchitectTools.pixelsPerCityUnit / 100;
				float actualHeight = (float)properties.height / 100;

				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualHeight / 2), new Vector3(actualWidth, actualHeight, 0));
			}
		}
	}
}