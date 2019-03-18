using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class UnityUtil
{
	public static string GetSelectedPathOrFallback()
	{
		string path = "Assets";

		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);
			if (!string.IsNullOrEmpty(path) && File.Exists(path))
			{
				path = Path.GetDirectoryName(path);
				break;
			}
		}
		return path;
	}
}
