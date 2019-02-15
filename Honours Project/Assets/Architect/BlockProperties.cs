using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class BlockProperties
	{
		//public Sprite sprite; // TEMP
		[Tooltip("In city units.")]
		public int width;
		[Tooltip("In pixels.")]
		public int height;
		public List<Style> styles = new List<Style>();
	}

	public enum Style
	{
		Style1,
		Style2
	}
}
