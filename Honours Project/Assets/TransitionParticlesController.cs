using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionParticlesController : MonoBehaviour
{
	private ParticleSystem particleSystem;

	void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
		particleSystem.Stop();
		GameManager.ExitingScene.AddListener(OnExitingScene);
	}

	private void OnExitingScene()
	{
		particleSystem.Play();
	}
}
