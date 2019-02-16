using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class BlockProperties
	{
		public BlockCategory category;

		[Tooltip("In city units.")]
		public int width;
		[Tooltip("In pixels.")]
		public int height;

		public List<ArchitecturalStyle> styles = new List<ArchitecturalStyle>();
	}
}
