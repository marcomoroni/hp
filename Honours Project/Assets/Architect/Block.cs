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

		// Maybe shouldn't be in this class :/
		[MenuItem("Assets/Create/Architect/Block Prefab Variant", priority = 2)]
		static void CreateBlockPrefab(MenuCommand menuCommand)
		{
			// Get prefab asset
			Object source = AssetDatabase.LoadAssetAtPath("Assets/Architect/Block.prefab", typeof(Object));
			// Instantiate prefab (needed to create a prefab variant asset)
			GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(source);
			// Create prefab variant asset
			//GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, "Assets/Resources/Blocks/NewBlock.prefab");
			GameObject obj = PrefabUtility.SaveAsPrefabAsset(objSource, UnityUtil.GetSelectedPathOrFallback() + "/NewBlock.prefab");
			// Destory prefab instance
			Object.DestroyImmediate(objSource);
		}

		private void OnDrawGizmos()
		{
			if (properties != null)
			{
				if (properties.width > 0 && properties.height > 0)
				{
					Gizmos.color = new Color(0, 1, 0);

					float makeBigger = -0f; // actually, make smaller

					float actualWidth = properties.width * (float)City.pixelsPerCityUnit / 100 + makeBigger;
					float actualHeight = (float)properties.height / 100 + makeBigger;

					Gizmos.DrawWireCube(transform.position + new Vector3(actualWidth / 2, actualHeight / 2) - new Vector3(makeBigger / 2, makeBigger / 2), new Vector3(actualWidth, actualHeight, 0));
				}
			}
		}
	}
}