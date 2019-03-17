using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Architect
{
	public static class ArchitectTools
	{
		private static Object[] blockPVs;
		public static Object[] BlockPVs
		{
			get
			{
				if (blockPVs == null)
				{
					Object[] allBlocks = Resources.LoadAll("Blocks");

					// Remove ignored blocks
					blockPVs = allBlocks.Where(b => !((GameObject)b).GetComponent<Block>().properties.ignore).ToArray();
				}
				return blockPVs;
			}
		}

		public static Object FindValidBlockPrefabVariant(StructureProperties properties, BlockCategory blockCategory)
		{
			List<Object> validCandidates = new List<Object>();

			for (int i = 0; i < BlockPVs.Length; i++)
			{
				BlockProperties b = ((GameObject)BlockPVs[i]).GetComponent<Block>().properties;

				// TODO: use properties to find correct ones
				if (b.width == properties.width && b.category == blockCategory)
				{
					validCandidates.Add(BlockPVs[i]);
				}
			}

			if (validCandidates.Count > 0)
			{
				Object candidateChosen = validCandidates.GetRandom();
				return candidateChosen;
			}

			Debug.LogWarning("Cannot find a block with required properties: Cat: " + blockCategory + " W:" + properties.width + ".");
			//Debug.LogWarning("Cannot find a block with required properties: W:" + properties.width + ".");
			return null;
		}

		/*private static int[] possibleWidths;
		public static int[] PossibleWidths
		{
			get
			{
				if (possibleWidths == null)
				{
					// TODO: only Generic blocks
					possibleWidths = BlockPVs
						.Select(b => ((GameObject)b).GetComponent<Block>().properties.width)
						.Distinct()
						.ToArray();
				}
				return possibleWidths;
			}
		}*/

		public static int GetAValidVWidth(StructureType structureType)
		{
			// If random in range

			switch (structureType)
			{
				case StructureType.Empty:
					return UnityEngine.Random.Range(10, 30 + 1);
			}


			// If choose one

			List<int> possibleWidths = new List<int>();

			// Hard coded
			switch (structureType)
			{
				case StructureType.Generic:
					possibleWidths.AddRange(new int[] { 64, 114, 128, 156 }); break;

				case StructureType.Bridge:
					possibleWidths.AddRange(new int[] { 48, 83, 128, 160 }); break;

				default:
					possibleWidths.Add(0); break;
			}

			return possibleWidths[UnityEngine.Random.Range(0, possibleWidths.Count)];
		}

		public static BlockProperties GetPropertiesOfBlockPV(Object pv)
		{
			return ((GameObject)pv).GetComponent<Block>().properties;
		}

		#region Editor

		[MenuItem("GameObject/Architect/City", priority = 1)]
		static void CreateCity(MenuCommand menuCommand)
		{
			// https://docs.unity3d.com/ScriptReference/MenuItem.html
			GameObject source = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Architect/City.prefab", typeof(Object));
			GameObjectUtility.SetParentAndAlign(source, menuCommand.context as GameObject);
			GameObject newCity = (GameObject)PrefabUtility.InstantiatePrefab(source);
			Undo.RegisterCreatedObjectUndo(newCity, "Create " + newCity.name);
			Selection.activeObject = newCity;
		}

		[MenuItem("Assets/Create/Architect/Block", priority = 2)]
		static void CreateBlockPrefab(MenuCommand menuCommand)
		{
			// Get prefab asset
			Object source = AssetDatabase.LoadAssetAtPath("Assets/Architect/Block.prefab", typeof(Object));
			// Instantiate prefab (needed to create a prefab variant asset)
			GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(source);
			// Create prefab variant asset
			//GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, "Assets/Resources/Blocks/NewBlock.prefab");
			GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, UnityUtil.GetSelectedPathOrFallback() + "/NewBlock.prefab");
			// Destory prefab instance
			Object.DestroyImmediate(objSource);
		}

		#endregion
	}

	// If you add things here, remember to update the transition matrix
	public enum StructureType
	{
		Start,
		Generic,
		Bridge,
		//BridgeWithMoreOnTop,
		Empty,
	}

	public enum BlockCategory
	{
		Generic,
		Roof,
		Bridge,
		Tree,
		Forest
	}

	public enum ArchitecturalStyle
	{
		Style1,
		Style2
	}

	#region Symbols for structure L-System

	public abstract class SLS_Symbol { }

	public abstract class SLS_TerminalSymbol : SLS_Symbol
	{
		// Conceptually different for terminal definition: for this implementation,
		// terminals must have a block to calculate height

		public Object BlockPrefabVariant { get; set; }
	}
	public abstract class SLS_EmptyTerminalSymbol : SLS_Symbol
	{
		public int height { get; set; }
	}

	////////////////////

	class SLS_F_Roof : SLS_TerminalSymbol
	{
		public SLS_F_Roof(StructureProperties properties)
		{
			BlockPrefabVariant = ArchitectTools.FindValidBlockPrefabVariant(properties, BlockCategory.Roof);
		}
	}

	class SLS_F_Generic : SLS_TerminalSymbol
	{
		public SLS_F_Generic(StructureProperties properties)
		{
			BlockPrefabVariant = ArchitectTools.FindValidBlockPrefabVariant(properties, BlockCategory.Generic);
		}
	}

	class SLS_F_Bridge : SLS_TerminalSymbol
	{
		public SLS_F_Bridge(StructureProperties properties)
		{
			BlockPrefabVariant = ArchitectTools.FindValidBlockPrefabVariant(properties, BlockCategory.Bridge);
		}
	}

	class SLS_InsertGeneric : SLS_Symbol { }

	class SLS_T_Empty : SLS_EmptyTerminalSymbol
	{
		public SLS_T_Empty(int height)
		{
			this.height = height;
		}
	}

	#endregion
}