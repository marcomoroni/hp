using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architect
{
	[AddComponentMenu("Architect/Structure")]
	public class Structure : MonoBehaviour
	{
		public StructureProperties properties;

		// Generate as an L-system
		public void Generate()
		{
			Debug.Log("Generating structure");
		}

		/*private (GameObject, Block) CreateBlock()
		{

		}*/
	}
}