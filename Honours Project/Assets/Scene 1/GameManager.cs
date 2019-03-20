using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	private GameManagerStates currentState;

	private float slideshowTimer = 20.0f;
	public class UnityBoolEvent : UnityEvent<bool> { }
	public static UnityBoolEvent slideshowModeChanged = new UnityBoolEvent();

	public static UnityEvent ExitingScene = new UnityEvent();

	private void Start()
	{
		currentState = GameManagerStates.Normal;
	}

	private void Update()
	{
		if (Input.GetKeyDown("r") && currentState == GameManagerStates.Normal)
		{
			StartCoroutine(ReloadLevel());
		}

		if (Input.GetKeyDown("s"))
		{
			GameManagerData.slideshowMode = !GameManagerData.slideshowMode;
			slideshowTimer = 20.0f;
			slideshowModeChanged.Invoke(GameManagerData.slideshowMode);
		}

		if (GameManagerData.slideshowMode && currentState == GameManagerStates.Normal)
		{
			slideshowTimer -= Time.deltaTime;

			if (slideshowTimer <= 0)
			{
				StartCoroutine(ReloadLevel());
			}
		}
	}

	IEnumerator ReloadLevel()
	{
		currentState = GameManagerStates.Exiting;
		ExitingScene.Invoke();

		AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		operation.allowSceneActivation = false;

		while (!operation.isDone && operation.progress < 0.9f)
		{
			yield return null;
		}

		//yield return new WaitUntil(() => { return Input.GetKeyDown("space"); }); // change to readytoexit
		yield return new WaitForSeconds(4.0f);
		operation.allowSceneActivation = true;
	}
}

public enum GameManagerStates
{
	Normal,
	Exiting,
	ReadyToExit
}

public static class GameManagerData
{
	public static bool slideshowMode = false;
	public static bool hideUI = false;

	// City properties
}