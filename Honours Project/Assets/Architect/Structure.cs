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

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				Gizmos.color = new Color(1, 1, 0);

				float actualWidth = properties.width * (float)City.pixelsPerUnit / 100;
				float actualHeight = properties.height * (float)City.pixelsPerUnit / 100;

				Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualHeight / 2), new Vector3(actualWidth, actualHeight, 0));
			}
		}
	}
}