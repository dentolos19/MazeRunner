using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFunctions : MonoBehaviour
{

	public void BackToMenu()
	{
		SceneManager.LoadScene(0);
	}
	
	public void Quit()
	{
		Application.Quit();
	}

}