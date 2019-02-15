﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	//[CreateAssetMenu(fileName = "NewBlockProperties", menuName = "Architect/Block Properties", order = 2)]
	[System.Serializable]
	public class BlockProperties
	{
		//public Sprite sprite; // TEMP
		public int width;
		public int height;
		public List<Style> styles = new List<Style>();
	}

	public enum Style
	{
		Style1,
		Style2
	}
}
