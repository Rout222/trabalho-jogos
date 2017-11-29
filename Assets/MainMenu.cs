using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Play()
	{
		PlayerPrefs.SetInt ("edit", 0);
		SceneManager.LoadScene("Main");
	}
	public void Edit()
	{
		PlayerPrefs.SetInt ("edit", 1);
		SceneManager.LoadScene("EditMenu");
	}

	public void Quit()
	{
		Debug.Log("Exciting...");
		Application.Quit ();
	}
}
