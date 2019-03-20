using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
	Image overlay;

	// Start is called before the first frame update
	void Start()
    {
		overlay = GetComponent<Image>();
		GameManager.ExitingScene.AddListener(OnSceneExiting);
		StartCoroutine(FadeOut());
    }

	private void OnSceneExiting()
	{
		StartCoroutine(FadeIn());
	}

	IEnumerator FadeIn()
	{
		yield return new WaitForSeconds(0.6f);

		while (overlay.color.a < 1)
		{
			overlay.color = new Color(
				1,
				1,
				1,
				overlay.color.a + 0.5f * Time.deltaTime);

			yield return null;
		}
	}

	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(0.4f);

		while (overlay.color.a > 0)
		{
			overlay.color = new Color(
				1,
				1,
				1,
				overlay.color.a - 0.2f * Time.deltaTime);

			yield return null;
		}
	}
}
