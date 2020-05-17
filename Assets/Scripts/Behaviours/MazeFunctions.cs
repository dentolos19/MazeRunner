using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeFunctions : MonoBehaviour
{

	public void Exit()
	{
		Application.Quit();
	}

	public void Restart()
	{
		SceneManager.LoadScene(1);
	}

}