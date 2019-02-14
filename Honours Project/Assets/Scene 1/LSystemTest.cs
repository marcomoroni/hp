using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemTest : MonoBehaviour
{
	LSystem<char> myProduction;

    // Start is called before the first frame update
    void Start()
    {
		myProduction = new LSystem<char>(new List<char> { 'a', 'b', 'c' }, new List<char> { 'a', 'a', 'b' });
		myProduction.AddRule('a', () => { return new List<char> { 'a', 'c' }; });
		Print();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("l"))
		{
			myProduction.Expand();
			Print();
		}
	}

	private void Print()
	{
		string output = "";

		foreach(var c in myProduction)
		{
			output += c;
		}

		Debug.Log(output);
	}
}
