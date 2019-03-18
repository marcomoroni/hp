using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideshowLabel : MonoBehaviour
{
	private TextMeshProUGUI text;

	private void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
		GameManager.slideshowModeChanged.AddListener(OnSlideshowModeChanged);
		OnSlideshowModeChanged(GameManagerData.slideshowMode);
	}

	private void OnSlideshowModeChanged(bool mode)
	{
		if (mode)
		{
			text.text = "SLIDESHOW ON";
		}
		else
		{
			text.text = "SLIDESHOW OFF";
		}
	}
}
