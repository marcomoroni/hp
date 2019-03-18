using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Architect
{
	public static class ArchitectToolsEditor
	{
		[MenuItem("GameObject/Architect/City", priority = 1)]
		static void CreateCity(MenuCommand menuCommand)
		{
			// https://docs.unity3d.com/ScriptReference/MenuItem.html
			GameObject source = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Architect/City.prefab", typeof(Object));
			GameObjectUtility.SetParentAndAlign(source, menuCommand.context as GameObject);
			GameObject newCity = (GameObject)PrefabUtility.InstantiatePrefab(source);
			Undo.RegisterCreatedObjectUndo(newCity, "Create " + newCity.name);
			Selection.activeObject = newCity;
		}

		[MenuItem("Assets/Create/Architect/Block", priority = 2)]
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
	}
}