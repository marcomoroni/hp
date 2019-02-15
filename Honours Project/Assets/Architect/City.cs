using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Heights are in pixels (1 px = 1/100 Unity unit)
// Widths are pixelsPerUnit wide

namespace Architect
{
	[AddComponentMenu("Architect/City")]
	public class City : MonoBehaviour
	{
		[Header("Properties")]
		[Tooltip("Leave it null to create random properties.")]
		public CityProperties properties;

		[Header("Prefabs")]
		public GameObject neighborhoodPrefab;

		public static readonly int pixelsPerCityUnit = 64; // Divide by 100 (Unity unit size) when using for translation

		private void Update()
		{
			if (Input.GetKeyDown("space"))
			{
				Generate();
			}
		}

		public void Generate()
		{
			// If properties is null, generate random one
			if (properties == null)
			{
				properties = ScriptableObject.CreateInstance<CityProperties>();
				properties.Randomize();
			}

			// TEMP: Generate 1 neighborhood
			CreateNeighborhood();
		}

		private (GameObject, Neighborhood) CreateNeighborhood()
		{
			GameObject go = Instantiate(neighborhoodPrefab, transform.position, transform.rotation);
			go.name = neighborhoodPrefab.name;
			Neighborhood neighborhood = go.GetComponent<Neighborhood>();
			neighborhood.properties = new NeighborhoodProperties(properties);
			neighborhood.Generate();

			return (go, neighborhood);
		}

		[MenuItem("GameObject/Architect/City", priority = 1)]
		static void CreateBlockPrefab(MenuCommand menuCommand)
		{
			// https://docs.unity3d.com/ScriptReference/MenuItem.html
			GameObject source = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Architect/City.prefab", typeof(Object));
			GameObjectUtility.SetParentAndAlign(source, menuCommand.context as GameObject);
			GameObject newCity = (GameObject)PrefabUtility.InstantiatePrefab(source);
			Undo.RegisterCreatedObjectUndo(newCity, "Create " + newCity.name);
			Selection.activeObject = newCity;
		}
	}
}