using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { AUTO, MANUAL };

public class PlayerController : MonoBehaviour
{
	public float speed = 5.0f;
	public float maxXPos = 3;
	private Vector3 origin;
	public float maxZoomIn = 1;
	public float maxZoomOut = 3;

	private PlayerState playerState = PlayerState.MANUAL;
	private float timerToAuto;
	private Vector3 pivotForAuto;
	[Header("Floating")]
	public Vector3 maxFloatingDistance;
	public Vector3 floatingSpeed;
	private float floatingTime = 0;
	private bool xDir;

	private void Start()
	{
		origin = transform.position;
		timerToAuto = 2;

		pivotForAuto = transform.position;
		xDir = Random.Range(0, 2) != 0;
	}

	void Update()
    {
		if (timerToAuto > 0) timerToAuto -= Time.deltaTime;
		if (timerToAuto <= 0 && playerState != PlayerState.AUTO)
		{
			playerState = PlayerState.AUTO;
			pivotForAuto = transform.position;
			floatingTime = 0;
			xDir = Random.Range(0, 2) != 0;
		}

		if (Input.GetKey("left") && transform.position.x > -maxXPos)
		{
			playerState = PlayerState.MANUAL;
			timerToAuto = 6;
			transform.position = transform.position.With(x: transform.position.x - speed * Time.deltaTime);
		}

		if (Input.GetKey("right") && transform.position.x < maxXPos)
		{
			playerState = PlayerState.MANUAL;
			timerToAuto = 6;
			transform.position = transform.position.With(x: transform.position.x + speed * Time.deltaTime);
		}

		if (Input.GetKey("up") && transform.position.z < origin.z + maxZoomIn)
		{
			playerState = PlayerState.MANUAL;
			timerToAuto = 6;
			transform.position = transform.position.With(z: transform.position.z + speed * Time.deltaTime);
		}

		if (Input.GetKey("down") && transform.position.z > origin.z - maxZoomOut)
		{
			playerState = PlayerState.MANUAL;
			timerToAuto = 6;
			transform.position = transform.position.With(z: transform.position.z - speed * Time.deltaTime);
		}

		if (playerState == PlayerState.AUTO)
		{
			//float dx = Mathf.PerlinNoise(0 + Time.deltaTime * walkSpeed, 1000 + Time.deltaTime * walkSpeed) * walkDistance.x * 2 - walkDistance.x;
			//float dz = Mathf.PerlinNoise(2000 + Time.deltaTime * walkSpeed, 3000 + Time.deltaTime * walkSpeed) * walkDistance.z * 2 - walkDistance.z;
			//transform.position = pivotForAuto + new Vector3(pivotForAuto.x + dx, pivotForAuto.y, pivotForAuto.z + dz);

			transform.position = pivotForAuto + new Vector3(
				maxFloatingDistance.x * Mathf.Sin(floatingTime * floatingSpeed.x) * (xDir == false ? -1 : 1),
				maxFloatingDistance.y * Mathf.Sin(floatingTime * floatingSpeed.y),
				maxFloatingDistance.z * Mathf.Sin(floatingTime * floatingSpeed.z));
			floatingTime += Time.deltaTime;
		}
	}
}
