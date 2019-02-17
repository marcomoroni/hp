using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Architect
{
	[AddComponentMenu("Architect/Structure")]
	public class Structure : MonoBehaviour
	{
		public StructureProperties properties;

		// Generate as an L-system
		public void Generate()
		{
			int GetTotalHeight(LSystem<SLS_Symbol> symbols)
			{
				int total = 0;

				foreach (var symbol in symbols)
				{
					switch (symbol)
					{
						case SLS_TerminalSymbol ts:
							total += ArchitectTools.GetPropertiesOfBlockPV(ts.BlockPrefabVariant).height;
							break;
					}
				}

				return total;
			}

			// Create L-System
			LSystem<SLS_Symbol> blocksLSystem = new LSystem<SLS_Symbol>(properties.axioms, properties.rules);

			// Expand until structure height is reached
			while (GetTotalHeight(blocksLSystem) < properties.height)
			{
				blocksLSystem.Expand();
			}

			CreateAllBlocks(blocksLSystem);
		}

		private void CreateAllBlocks(LSystem<SLS_Symbol> derivation)
		{
			int currentHeight = 0;

			foreach (var symbol in derivation)
			{
				switch (symbol)
				{
					case SLS_TerminalSymbol ts:
						var newBlock = CreateBlock(ts.BlockPrefabVariant, currentHeight);
						currentHeight += newBlock.Item2.properties.height;
						break;
				}
			}
		}

		private (GameObject, Block) CreateBlock(UnityEngine.Object blockPrefabVariant, int posY)
		{
			GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(blockPrefabVariant);
			go.name = ((GameObject)blockPrefabVariant).name;
			go.transform.parent = this.gameObject.transform;
			go.transform.position = go.transform.parent.transform.position + new Vector3(0, (float)posY / 100, 0);
			Block block = go.GetComponent<Block>();

			return (go, block);
		}

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				Gizmos.color = new Color(1, 1, 0);

				float actualWidth = (float)properties.width / 100;
				float actualHeight = (float)properties.height / 100;

				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualHeight / 2), new Vector3(actualWidth, actualHeight, 0));
			}
		}
	}
}