using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideInstructions : MonoBehaviour
{
	public GameObject instructions;

    void Update()
    {
        if (Input.GetKeyDown("c"))
		{
			instructions.SetActive(!instructions.activeSelf);
		}
	}
}
