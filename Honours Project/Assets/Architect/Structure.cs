using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[AddComponentMenu("Architect/Structure")]
	public class Structure : MonoBehaviour
	{
		//List<GameObject> blocks = new List<GameObject>();

		enum BlockType
		{
			Normal,
			Roof,
			Fundation,
			Bridge
		}

		struct BlockSymbol
		{
			public BlockType blockType;
			public int height;

			public BlockSymbol(BlockType blockType, int height)
			{
				this.blockType = blockType;
				this.height = height;
			}
		}

		public StructureProperties structureProperties;

		List<BlockSymbol> symbols = new List<BlockSymbol>();

		public void Generate()
		{
			
		}
	}

	public class StructureProperties
	{
		public int height = 1;
	}
}