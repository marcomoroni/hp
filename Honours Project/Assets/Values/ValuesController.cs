using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Architect;

public class ValuesController : MonoBehaviour
{
	public InputField minNeighbourhoods;
	public InputField maxNeighbourhoods;
	public InputField minWidth;
	public InputField maxWidth;
	public InputField minHeight;
	public InputField maxHeight;
	public InputField vegetation;
	public InputField neighbourhoodXScatter;

    void Start()
    {
		minNeighbourhoods.text = GameManagerData.cityProperties.minNeighbourhoods.ToString();
		maxNeighbourhoods.text = GameManagerData.cityProperties.maxNeighbourhoods.ToString();
		minWidth.text = GameManagerData.cityProperties.minWidth.ToString();
		maxWidth.text = GameManagerData.cityProperties.maxWidth.ToString();
		minHeight.text = GameManagerData.cityProperties.minHeight.ToString();
		maxHeight.text = GameManagerData.cityProperties.maxHeight.ToString();
		vegetation.text = GameManagerData.cityProperties.vegetation.ToString();
		neighbourhoodXScatter.text = GameManagerData.cityProperties.neighbourhoodXScatter.ToString();
	}
}
