using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	bool gameHasEnded = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			GMSL_LOADSCENE(0);
		}
    }

    public void EndGame()
	{
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			SceneManager.LoadScene(2);
			Debug.Log("GAME OVER");

		}

	}

	public void ExitGame()
    {
		Debug.Log("game Ended");
		Application.Quit();

	}

	public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}


	public void KembaliMainMenu()
	{
		SceneManager.LoadScene(0);
	}


	public void GMSL_LOADSCENE(int sceneIndex)
    {
		SceneManager.LoadScene(sceneIndex);
    }

}
