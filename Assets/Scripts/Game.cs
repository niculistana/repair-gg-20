using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using GG;

public class Game : MonoBehaviour
{
	private bool isGamePaused;

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Cancel")) {
			isGamePaused = !isGamePaused;
		}

		if (isGamePaused) {
			PauseGame();
		} else {
			ResumeGame();
		}
	}

	void PauseGame()
	{
		UIView.ShowView("General", "Main Menu");
		UIView.HideView("Game", "Game Menu");
		UIView.HideView("Game", "Dialogue Menu");
	}

	void ResumeGame()
	{
		UIView.HideView("General", "Main Menu");
		UIView.ShowView("Game", "Game Menu");
		UIView.ShowView("Game", "Dialogue Menu");
	}
}
