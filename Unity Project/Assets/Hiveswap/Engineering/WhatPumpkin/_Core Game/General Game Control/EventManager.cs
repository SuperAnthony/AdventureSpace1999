using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities;
using WhatPumpkin.Sgrid.Markers;
using WhatPumpkin.Dialogue;
using WhatPumpkin.Screens;
using WhatPumpkin.Sgrid.Environment;
using WhatPumpkin.Sgrid.Triggers; // TODO: Remove this when IHotSpotController is complete

using WhatPumpkin.FX;

namespace WhatPumpkin {

	/// <summary>
	/// Event manager. Manages common events.
	/// </summary>

	public class EventManager : MonoBehaviour {

		#region fields

		bool _subscribedEvents = false;

		#endregion

		#region static fields

		/// <summary>
		/// The _instance.
		/// </summary>

		static EventManager _instance;
	

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>

		static public EventManager Instance { get { return _instance; } }

		/// <summary>
		/// Occurs when update.
		/// </summary>

		// TODO: These really should not be static - would also prefer event handlers

		static event Action _update; 
		  
		static public event Action MonoBehavioursStarted; // TODO: Remove?

		static public event Action ApplicationClose; // TODO: Decide whether or not this is all going to be static or not (It should not be - I am not happy with this)

		/// <summary>
		/// Occurs when application start.
		/// </summary>

		public event Action ApplicationStart;


		/// <summary>
		/// Occurs when user loads scene.
		/// </summary>

		//public event EventHandler<SaveEventArgs> UserLoadsScene;


		#endregion

		#region static properties


		/// <summary>
		/// Occurs when on update. Subcribe types that are not mono behaviour to update
		/// </summary>

		static public event Action OnUpdate { add { _update+=value;} remove { _update-=value; } }

		#endregion

		#region methods

		/// <summary>
		/// Start this instance.
		/// </summary>
		
		public void Start() {

			// Set up singleton instance
			_instance = this;


			// Broadcast the start of the application
			if (ApplicationStart != null) {ApplicationStart.Invoke();}


		
		}
	

		void Awake() {

			// TODO: Make sure this gets awoken before other entities and entity types
			// Particularly the entity that broadcasts on awake

			// Subscribe the events
			SubscribeEvents ();


			//Debug.Log ("Event Manager Awoken");

			if (MonoBehavioursStarted != null) {
								MonoBehavioursStarted.Invoke ();
						}
		}

		/// <summary>
		/// Subscribes the events.
		/// </summary>
		
		public void SubscribeEvents() {

			if (_subscribedEvents)
								return;

			//Hint.HintAlert += OnHintAlert;

			if(GameController.SceneManager != null) {
				GameController.SceneManager.SceneLoadEnd += HandleSceneLoaded;
			}

			if(GameController.Instance != null) {
				GameController.Instance.ApplicationClose += HandleApplicationClose;
			}

			
			if (GameController.SceneManager != null) {
				GameController.SceneManager.SceneDataLoaded += HandleSceneDataLoaded; 
			}

			SpawnPoint.SpawnPointActivated += HandleSpawnActivated;

			EntityInfo.Awoken += HandleEntityAwoken;

			ConversationControl.ConversationStart += HandleConversationStart;

			
		}


		/// <summary>
		/// Handles the scene data loaded.
		/// </summary>

		void  HandleSceneDataLoaded()
		{
			// Broadcast the scene data being loaded
			//if(SceneDataLoaded)
		}

		void HandleKeyCreated(object obj, EventArgs e) {
		
			Keyed keyedObject = (Keyed)obj;

			Debug.Log ("Keyed Object Created: " + keyedObject.Key);

		}

		/// <summary>
		/// Unsubscribes the events.
		/// </summary>


		public void UnsubscribeEvents() {

		
			// TODO:
			//Hint.HintAlert -= OnHintAlert;

			if (GameController.SceneManager) {
				GameController.SceneManager.SceneLoadEnd -= HandleSceneLoaded;
			}

			if (GameController.Instance) {
				GameController.Instance.ApplicationClose -= HandleApplicationClose;
			}


			SpawnPoint.SpawnPointActivated -= HandleSpawnActivated;
			EntityInfo.Awoken -= HandleEntityAwoken;
			ConversationControl.ConversationStart -= HandleConversationStart;


		}

		/// <summary>
		/// Handles the application close.
		/// </summary>

		public void HandleApplicationClose() {

			if (ApplicationClose != null) {
			
				// Invoke application close
				ApplicationClose();

			}

			UnsubscribeEvents ();
		}


		public void Update() {


			if(_update != null) {
				_update.Invoke();
			}
		}

#endregion

        #region event handlers

        /// <summary>
        /// Raises the strife start event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>

        void HandleStrifeStart(object sender, EventArgs e) {
		
			// Change the game state
			GameController.GameStateMachine.ChangeGameState (GameController.GameStateMachine.OnStrife, GameController.CamManager.ActiveCameraNode);
		
		}

	
		/// <summary>
		/// Handles the strife end.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleStrifeEnd(object sender, EventArgs e) {
			GameController.GameStateMachine.ReturnToPreviousState (null);
		}

		/// <summary>
		/// Raises the cutscene start event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		/// 
		void OnCutsceneStart(object sender, EventArgs e) {
		
			// Get the camera manager
			CameraManagement.CameraController camManager = (CameraManagement.CameraController)sender;

			// Change the game state
			GameController.GameStateMachine.ChangeGameState (GameController.GameStateMachine.OnCloseup, camManager.ActiveCameraNode);

			
			// Disable the room hotspots
			this.GetComponent<HotSpotController>().DisableRoomHotSpots ();

			// Show the closeup screen
			ScreenManager.Instance.OpenScreen ("Closeup Screen");


		}

		/// <summary>
		/// Handles the conversation start.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleConversationStart(object sender, EventArgs e) {
		

			ConversationControl convoControl = (ConversationControl)sender;

			// Set State
			GameController.GameStateMachine.ChangeGameState (GameController.GameStateMachine.OnConversation, GameController.CamManager.ActiveCameraNode);
			// Set the new camera position
			GameController.CamManager.ActiveCameraNode = convoControl.ConversationCamera;

		}

		/// <summary>
		/// Handles the entity awoken.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleEntityAwoken(object sender, EventArgs e) {
		
			// Add to the scene manager collection
			Entity entity = (Entity)sender;
			// Add this entity to the scene
			if(entity != null){GameController.SceneManager.AddSceneEntity (entity);}

		
		}

		/// <summary>
		/// Raises the hint alert event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleHintAlert(object sender, EventArgs e) {
		
			Debug.Log ("Hint Alert");

		}

		/// <summary>
		/// Raises the scene loaded event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSceneLoaded(object sender, EventArgs e) {
		
			SceneLoadArgs sceneLoadEventArguments = (SceneLoadArgs)e;

			// Initialize the rooms
			Room.InitRooms ();


			// Find a spawn point by key if it exists in the scene | TODO: Can be found in entity collection scene manager instead
			SpawnPoint spawnPoint =  SpawnPoint.FindSpawnPoint (sceneLoadEventArguments.SpawnPoint);

			if (spawnPoint != null) {
								spawnPoint.Activate ();
						}

			// Start the camera manager
			GameController.CamManager.Start (); // TODO: I don't like this


			try {
				GameController.PartyManager.ActivePC.ChangeScene (Application.loadedLevelName);
			}
			catch {
				Debug.Log ("Error trying to change Active PC Scene");		
			}
		}


		void HandleSpawnActivated(object sender, EventArgs e) {

			// Get the spawn point info
			ISpawnPoint spawnPoint = (ISpawnPoint)sender;

			// Check to see if this spawn point should activate the active pc and if so, spawn the player at this point
			// Spawn the active player character if that's needs to be done
			if (spawnPoint != null ) {

				if(spawnPoint.SpawnActivePC) {

					// Set the target mover
					GameController.Instance.MoveTarget.transform.position = new Vector3 (spawnPoint.transform.position.x,
					                                                                     spawnPoint.transform.position.y,
					                                                                     spawnPoint.transform.position.z);
					// Set the active player
					if(GameController.PartyManager.ActivePC != null) {
						GameController.PartyManager.ActivePC.Spawn(spawnPoint);
					}
					else {
					
						Debug.LogError("No active character found on spawn");
					}
				}

				// Set the camera
				if(spawnPoint.CameraNode) {
					GameController.CamManager.ActiveCameraNode = spawnPoint.CameraNode;
				}

				//GameController.PartyManager.ActivePC.ChangeScene (Application.loadedLevelName);
			}
		
		}

	
		#endregion
	

	}
}
