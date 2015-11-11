using UnityEngine;
using System.Collections;

/// <summary>
///  What is required to be a mini game
/// </summary>

namespace WhatPumpkin.HiveSwap.MiniGames {

	public interface IMiniGame  {

		void Pause(); // Pauses the game
		void Unpause(); // Unpauses the game 
		void Restart(); // Handles the game being restarted
		void StartGame(); // What happens when the game is activated
		void EndGame(); // What happens when the game is shut down

	}

}
