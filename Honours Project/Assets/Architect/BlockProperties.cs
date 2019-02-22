using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class BlockProperties
	{
		public bool ignore = false;

		public BlockCategory category;

		[Tooltip("In pixels.")]
		public int width;
		[Tooltip("In pixels.")]
		public int height;

		public List<ArchitecturalStyle> styles = new List<ArchitecturalStyle>();
	}
}
