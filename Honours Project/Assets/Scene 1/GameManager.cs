using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	private GameManagerStates currentState;

	public static UnityEvent ExitingScene = new UnityEvent();

	private void Start()
	{
		currentState = GameManagerStates.Normal;
	}

	private void Update()
	{
		if (Input.GetKeyDown("r") && currentState == GameManagerStates.Normal)
		{
			Debug.Log("Reloading");
			ExitingScene.Invoke();
			currentState = GameManagerStates.Exiting;
			StartCoroutine(ReloadLevel());
		}
	}

	IEnumerator ReloadLevel()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		operation.allowSceneActivation = false;

		while (!operation.isDone && operation.progress < 0.9f)
		{
			Debug.Log(operation.progress);
			yield return null;
		}

		//yield return new WaitUntil(() => { return Input.GetKeyDown("space"); }); // change to readytoexit
		yield return new WaitForSeconds(5.0f);
		operation.allowSceneActivation = true;
	}
}

public enum GameManagerStates
{
	Normal,
	Exiting,
	ReadyToExit
}
