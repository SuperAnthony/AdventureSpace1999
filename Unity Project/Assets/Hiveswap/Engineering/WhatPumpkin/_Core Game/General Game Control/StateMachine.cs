#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 14, 2014
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using WhatPumpkin.HiveSwap.MiniGames;
using WhatPumpkin.CameraManagement;
using WhatPumpkin.CutScenes;
using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin {

	//public enum GameState {MiniGame, Conversation, CutScene, Explore, SaveScreen, LoadScreen, PauseMenu }
	/// <summary>
	/// The game's state machine.
	/// </summary>

	public class StateMachine : MonoBehaviour {

		// Static Fields

		/// <summary>
		/// Singleton for the active state machine.
		/// </summary>

		static StateMachine _instance;

		// Fields

		/// <summary>
		/// Delegate for handling different game states
		/// </summary>

		public delegate void GameStateDelegate();

		/// <summary>
		/// Occurs when state changed.
		/// </summary>

		public event EventHandler StateChanged;

		/// <summary>
		/// The active state of the game.
		/// </summary>
		
		GameStateDelegate _activeGameState;

		/// <summary>
		/// The previous state of the game.
		/// </summary>
		
		GameStateDelegate _previousState;

		// Static Properties

		/// <summary>
		/// Gets the instance of the state machine.
		/// </summary>
		/// <value>The instance.</value>
		static public StateMachine Instance { get { return _instance; } }


		// Properties

		/// <summary>
		/// Gets a value indicating whether this instance can explore.
		/// </summary>
		/// <value><c>true</c> if this instance can explore; otherwise, <c>false</c>.</value>


		public bool CanExplore { 

			// TODO: I think this belongs in the game controller (specific to adventure games)

			get { return 
					_activeGameState == OnExplore &&
				
					!GameController.MessageManager.IsShowingNarratorMessage; // Showing a narrator message?
					
					//&& !VerbCoinPanel.IsOpen;
			} 
		}

		/// <summary>
		/// Gets the current game state.
		/// </summary>
		/// <value>The state of the current.</value>
		/*
		public string CurrentState { get { 



				return _activeGameState.ToString (); } 
		}*/


		/// <summary>
		/// The camera return node. This is the node that the camera will be returned to when a state has finished.
		/// </summary>
		CameraNode CameraReturnNode {get; set;} // Private set?


		void Awake() {

			// Initialize the Game State

			// make sure the active game state is null just in case
			_activeGameState = null;

			// Set the game state to explore
			_activeGameState += OnExplore;
		
		}

		// Use this for initialization
		void Start () {
			// This is the active state machine
			_instance = this;


		
		}
		
		public void OnExplore() {
			
			// When the gamestate is in exploration mode
			// Default

		}
		
		public void OnMiniGame() {
			
			// When the game is playing a mini game
			
		}

		/// <summary>
		/// Raises the cut scene event.
		/// </summary>

		public void OnCutScene() {
		
			//Debug.Log ("On Cut Scene");

			// When the gamestate is in cut scene mode

			// Check to see if the cut scene should still be playing, if not, then end the cut scene 
			// and return to the previous state

			//Debug.Log ("Has Started Playing: " + CutScene.Active.HasStartedPlaying);

			// Has the cutscene started playing and is the cutscene still playing even after it has started
			/*
			if (CutScene.Active != null && !CutScene.Active.IsPlaying && CutScene.Active.HasStartedPlaying) {
			

				// End the cut scene


					CutScene.Active.Stop(); // The stop method will attempt to play the next cutscene


					// Return to the previous state if there are no more cutscenes
					if(CutScene.Active != null && !CutScene.Active.HasNextCutScene && CutScene.Active.HasStartedPlaying ){

						
						ReturnToPreviousState (null);
					}

			}

			// TODO: Active is null
			if (CutScene.Active == null) {
				ReturnToPreviousState (null);
			}*/

		}
	
		public void OnCloseup() {}

		public void EndCloseup() {
			// Only end the closeup if we're actually in the closeup state
			if (_activeGameState == OnCloseup) {	
				GameController.HotSpotController.EnaleRoomHotSpots();
				ReturnToPreviousState(null);
			}
		
		}


		/// <summary>
		/// When the user is conversing
		/// </summary>

		public void OnConversation() {

			// Check to see if the conversation is over. If so, end the conversation:
			if (!DialogueManager.IsConversationActive) {
				ReturnToPreviousState (null);
			}

		}

		/// <summary>
		/// Raises the strife event.
		/// </summary>

		public void OnStrife() {
		
			// Not much to do here
			
		}


		/// <summary>
		/// Returns the state of the to previous.
		/// </summary>
		/// <param name="returnCam">Return cam.</param>

		public void ReturnToPreviousState(CameraNode returnCam) {
			//Debug.Log ("Return to previous state: " + _previousState.ToString());
			//ChangeGameState (_previousState, null);
		
			// TODO: This is very very temp
			ChangeGameState (OnExplore, null);

		}

		/// <summary>
		/// Changes the state of the game.
		/// </summary>
		/// <returns><c>true</c>, if game state was changed, <c>false</c> otherwise.</returns>
		/// <param name="newActiveState">New active state.</param>
		/// <param name="returnCam">Camera that will be returned when state is over.</param>

		public bool ChangeGameState(GameStateDelegate newActiveState, CameraNode returnCam) {

			// Keep track of the current state so that we can return to it later
			// Clear all states
			_previousState = null;
			// Add this state
			_previousState += _activeGameState;

			// Return camera to original position if necessary
			if (CameraReturnNode != null) {
				// Set camera node to the old return cam
				GameController.CamManager.ActiveCameraNode = CameraReturnNode;
			}

			// Use the current cam node for the return state camera
			CameraReturnNode = returnCam;
			//Debug.Log (CameraReturnNode);

			// TODO: Handle the removal of a state
			// For instance, when we are no longer in a conversation state
			// What do we want to do if we are no longer in that conversation state


			// Assign to the active gameState to the new game state
			//_activeGameState = newState;
			
			// Remove all active methods 
			// TODO Note: There is a small possibility we may not necessarily want to do this if we want multiple states to run at once
			_activeGameState = null; 

			// Set the Active Game State
			_activeGameState += newActiveState;


			// Raise the state changed event
			if (StateChanged != null) {
				
				// TODO: State change arguments?
				StateChanged(this, new EventArgs());
				
			}

			// TODO: May want to return false if the user cannot change states for a particular reason
			return true; 
		}





		// Update is called once per frame
		void Update () {
			// Actively Invoke active game state if there is one
			if (_activeGameState != null) {
				_activeGameState ();
			}
		}
	}
	
}
