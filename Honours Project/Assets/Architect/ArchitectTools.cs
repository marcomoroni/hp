using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	public static class ArchitectTools
	{
		public static readonly int pixelsPerCityUnit = 64; // Divide by 100 (Unity unit size) when using for translation

		private static Object[] blockPVs;
		public static Object[] BlockPVs
		{
			get
			{
				if (blockPVs == null)
				{
					blockPVs = Resources.LoadAll("Blocks");
				}
				return blockPVs;
			}
		}

		public static Object FindValidBlockPrefabVariant(int width) // TODO: add parameters
		{
			List<Object> validCandidates = new List<Object>();

			for (int i = 0; i < BlockPVs.Length; i++)
			{
				BlockProperties b = ((GameObject)BlockPVs[i]).GetComponent<Block>().properties;

				if (b.width == width)
				{
					validCandidates.Add(BlockPVs[i]);
				}
			}

			if (validCandidates.Count > 0)
			{
				Object candidateChosen = validCandidates.GetRandom();
				return candidateChosen;
			}

			return null;
		}
	}
}