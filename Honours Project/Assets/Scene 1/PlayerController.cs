using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5.0f;
	public float maxXPos = 3;
	private Vector3 origin;
	public float maxZoomIn = 1;
	public float maxZoomOut = 3;

	private void Start()
	{
		origin = transform.position;
	}

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

		if (Input.GetKey("up") && transform.position.z < origin.z + maxZoomIn)
		{
			transform.position = transform.position.With(z: transform.position.z + speed * Time.deltaTime);
		}

		if (Input.GetKey("down") && transform.position.z > origin.z - maxZoomOut)
		{
			transform.position = transform.position.With(z: transform.position.z - speed * Time.deltaTime);
		}
	}
}
