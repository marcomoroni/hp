using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[RequireComponent(typeof(SpriteRenderer)), AddComponentMenu("Architect/Block")]
	public class Block : MonoBehaviour
	{
		public int width = 2;
		public int height = 2;
	}
}