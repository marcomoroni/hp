using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5.0f;
	public float maxXPos = 3;

    void Update()
    {
        if(Input.GetKey("left") && transform.position.x > -maxXPos)
		{
			transform.position = transform.position.With(x: transform.position.x - speed * Time.deltaTime);
		}

		if (Input.GetKey("right") && transform.position.x < maxXPos)
		{
			transform.position = transform.position.With(x: transform.position.x + speed * Time.deltaTime);
		}
	}
}
