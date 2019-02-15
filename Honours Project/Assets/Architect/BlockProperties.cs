using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[System.Serializable]
	public class BlockProperties
	{
		//public Sprite sprite; // TEMP
		public int width;
		public float height;
		public List<Style> styles = new List<Style>();
	}

	public enum Style
	{
		Style1,
		Style2
	}
}
