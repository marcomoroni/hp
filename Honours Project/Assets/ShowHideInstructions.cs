using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideInstructions : MonoBehaviour
{
	public GameObject[] things;

	private void Start()
	{
		UpdateVisibility();
	}

	void Update()
    {
        if (Input.GetKeyDown("c"))
		{
			GameManagerData.hideUI = !GameManagerData.hideUI;
			UpdateVisibility();
		}
	}

	private void UpdateVisibility()
	{
		bool hideUI = GameManagerData.hideUI;

		foreach (var t in things)
			t.SetActive(!hideUI);
	}
}
