using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Architect
{
	//[RequireComponent(typeof(SpriteRenderer)), AddComponentMenu("Architect/Block")]
	[AddComponentMenu("Architect/Block")]
	public class Block : MonoBehaviour
	{
		public BlockProperties properties;



		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				if (properties.width > 0 && properties.height > 0)
				{
					Gizmos.color = new Color(0, 1, 0);

					float makeBigger = -0f; // actually, make smaller

					float actualWidth = (float)properties.width / 100 + makeBigger;
					float actualHeight = (float)properties.height / 100 + makeBigger;

					Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualHeight / 2) - new Vector3(makeBigger / 2, makeBigger / 2), new Vector3(actualWidth, actualHeight, 0));
				}
			}
		}
	}
}