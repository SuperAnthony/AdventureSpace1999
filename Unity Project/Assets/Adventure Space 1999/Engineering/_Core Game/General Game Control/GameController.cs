#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - October, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Screens;
using WhatPumpkin.Sgrid;
using WhatPumpkin.Dialogue;
using WhatPumpkin.Sgrid.Characters;
using WhatPumpkin.CameraManagement;
using WhatPumpkin.Actions;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// GameController
	/// </summary>


	public class GameController : MonoBehaviour {

		#region static fields

		/// <summary>
		/// Occurs when application close.
		/// </summary>

		public event System.Action ApplicationClose;

		/// <summary>
		/// Singleton of the active controler.
		/// </summary>

		static GameController _instance; 

		#endregion


		/// <summary>
		/// List of the characters in the game
		/// </summary>
		
		public List<PlayerCharacter> characters = new List<PlayerCharacter> ();


		#region member fields

		/// <summary>
		/// The move target.
		/// </summary>

		[SerializeField] Transform _moveTarget;

	
		#endregion

		#region static properties

		/// <summary>
		/// Gets the singleton instance of the game controler.
		/// </summary>
		/// <value>The active controler.</value>

		static public GameController Instance { 
			get { 


				if(_instance == null) {


					try {
						GameObject.FindObjectOfType<GameController>().Init();
					}
					catch {
					
					}
				}

				return _instance;
			} 
		}

		/// <summary>
		/// Gets the active state machine.
		/// </summary>
		/// <value>The active state machine.</value>
		//TODO: May not want to allow direct access to the state machine
		static public StateMachine GameStateMachine { get { return Instance.GetComponent<StateMachine>(); } }


		/// <summary>
		/// Gets the active player character.
		/// </summary>
		/// <value>The active player character.</value>

		//static public PlayerCharacter ActivePlayerCharacter { get { return PlayerCharacter.Active; } }
	

		/// <summary>
		/// Gets the camera manager.
		/// </summary>
		/// <value>The camera manager.</value>

		static public CameraController CamManager {
			get {
					return Instance.GetComponent<CameraController> ();
			}
		}

		/// <summary>
		/// Gets the scene manager.
		/// </summary>
		/// <value>The scene manager.</value>


		static public SceneController SceneManager {
			 get {

				return Instance.GetComponent<SceneController>();
			}
		}

        /*
		static public SaveLoad SaveLoadManager {
		
            //TODO:  Rewriting this

			get {
				return Instance.GetComponent<SaveLoad>();			
			}

		}*/

		/// <summary>
		/// Gets the Game Variable Controller
		/// </summary>
		/// <value>The Game Variable Controller.</value>
		
		/*
		static public GameVariableController GameVariableController {
			get {
				
				return Instance.GetComponent<GameVariableController>();
			}
		}*/

		/// <summary>
		/// Gets the Party Manager
		/// </summary>
		/// <value>The Party Manager.</value>
				
		static public PartyManager PartyManager {
			get {
				
				return Instance.GetComponent<PartyManager>();
			}
		}

		/// <summary>
		/// Gets the message manager object.
		/// </summary>
		/// <value>The message manager.</value>

		static public IMessageManager<string> MessageManager {
		
			get {
				return Instance.GetComponent<IMessageManager<string>>();
			}
		
		}

		/// <summary>
		/// Gets the narrator message manager.
		/// </summary>
		/// <value>The narrator message manager.</value>

		static public INarratorMeessageManager<string> NarratorMessageManager {
		
			get {
				return Instance.GetComponent<INarratorMeessageManager<string>>();
			}

		}

		/// <summary>
		/// Gets the hot spot message manager.
		/// </summary>
		/// <value>The hot spot message manager.</value>

		static public IHotsSpotMessageManager<string> HotSpotMessageManager {
			
			get {
				return Instance.GetComponent<IHotsSpotMessageManager<string>>();
			}
			
		}

		/// <summary>
		/// Gets bark message manager.
		/// </summary>
		/// <value>The I bark message manager.</value>

		static public IBarkMessageManager<string> IBarkMessageManager {
			
			get {
				return Instance.GetComponent<IBarkMessageManager<string>>();
			}
			
		}


		/// <summary>
		/// Gets the action control.
		/// </summary>
		/// <value>The action control.</value>


		static public ActionController ActionControl {
			
			get {
				return Instance.GetComponent<ActionController>();
			}
			
		}

		/// <summary>
		/// Gets the inventory manager.
		/// </summary>
		/// <value>The inventory manager.</value>

		static public IInventoryManager InventoryManager {

			get{
					return Instance.GetComponent<IInventoryManager> ();
					
				}

		}

		/// <summary>
		/// Gets the hot spot controller.
		/// </summary>
		/// <value>The hot spot controller.</value>

		static public IHotSpotController HotSpotController {
		
			get{
				return Instance.GetComponent<IHotSpotController> ();
			}

		}

		/// <summary>
		/// Gets the variables.
		/// </summary>
		/// <value>The variables controller.</value>

		static public IGameVariableControl GameVariableController {
			
			get{

				
				return Instance.GetComponent<IGameVariableControl> ();
			}
			
		}

		/// <summary>
		/// Gets the conversation controller.
		/// </summary>
		/// <value>The conversation controller.</value>

		static public ConversationControl ConversationController { 
		
			get{
				return ConversationControl.Instance;
			}
		}

		/// <summary>
		/// Gets the screen manager.
		/// </summary>
		/// <value>The screen manager.</value>

		static public IScreenManager ScreenManager {
		
			get{
				return Instance.GetComponent<IScreenManager> ();
				}
		
		}


		/// <summary>
		/// Gets the cursor control.
		/// </summary>
		/// <value>The cursor control.</value>
		
		static public ICursorControl CursorControl { 
			get {
				return Instance.GetComponent<InputManager>();
			}
		}

		/// <summary>
		/// Gets the cursor control.
		/// </summary>
		/// <value>The cursor control.</value>
		
		static public IInputManager InputManager { 
			get {
				return Instance.GetComponent<IInputManager>();
			}
		}

		/// <summary>
		/// Sets the cut scene manager.
		/// </summary>
		/// <value>The cut scene manager.</value>

		static public ICutSceneManager CutsceneManager {
		
			get {
				return Instance.GetComponent<ICutSceneManager>();
			}

		}

		// TODO: The Inventory controller and this can combine to use an interface for item controll (because this is getting ridiculous)

		static public ICombineItemController CombineItemController {
			
			get {
				if(Instance != null) {
					return Instance.GetComponent<ICombineItemController>();
				}

				return null;
			}
			
		}

		static public IApplicationState ApplicationState {

			get {
				return Instance.GetComponent<IApplicationState>();
			}
		
		}


		#endregion
	
		#region propreties

		// I'm using this for the Use action aparently. I'm not happy about this and I want to make changes

		public IEntity SelectedEntity { get; set; } // TODO: See if I can remove this

		/// <summary>
		/// Gets or sets the move target.
		/// </summary>
		/// <value>The move target.</value>

		public Transform MoveTarget { get { return _moveTarget; } set { _moveTarget = value; } } // TODO: Significant changes need to be made here

		#endregion

		#region methods

		/// <summary>
		/// Clears the entity selection.
		/// </summary>

		public void ClearEntitySelection() {
			SelectedEntity = null;
		}

		/// <summary>
		/// Occurs on awake
		/// </summary>


		void Awake() {

			Init ();

		}

		/// <summary>
		/// Initialize
		/// </summary>

		void Init() {
									
			// Set up the active game controller singleton
			_instance = this;
		
		}

		void Start() {


		}


		/// <summary>
		/// On Update.
		/// </summary>

		void Update() {

		}

		/// <summary>
		/// Closes the application.
		/// </summary>

		public void CloseApplication() {
		
			if (ApplicationClose != null) {
				ApplicationClose.Invoke();
			}

			Application.Quit ();
		}

	

		#endregion

	}

} 
