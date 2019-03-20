﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Architect;

public class ValuesSceneManager : MonoBehaviour
{
	public ValuesController values;

	public void Generate()
	{
		// Set CityProperties values
		CityProperties cp = GameManagerData.cityProperties;
		cp.minNeighbourhoods = int.Parse(values.minNeighbourhoods.text);
		cp.maxNeighbourhoods = int.Parse(values.maxNeighbourhoods.text);
		cp.minWidth = int.Parse(values.minWidth.text);
		cp.maxWidth = int.Parse(values.maxWidth.text);
		cp.minHeight = int.Parse(values.minHeight.text);
		cp.maxHeight = int.Parse(values.maxHeight.text);
		cp.vegetation = int.Parse(values.vegetation.text);
		cp.neighbourhoodXScatter = int.Parse(values.neighbourhoodXScatter.text);

		// Generate
		SceneManager.LoadScene(0);
	}
}
