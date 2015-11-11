#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  Novemeber 7, 2014
#endregion 


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using WhatPumpkin.Actions.UI; // TODO: Remove this when I can
#endregion

namespace WhatPumpkin.Screens {

	/// <summary>
	/// ScreenManager - Used to manage our screens in the game
	/// </summary>


	public class ScreenManager : MonoBehaviour {

		#region static fields
		// Singleton for one Screen Manager
		static public ScreenManager _instance;
		#endregion

		#region fields

		/// <summary>
		/// The _game screens.
		/// </summary>

		Dictionary<string, IGameScreen> _gameScreens = new Dictionary<string, IGameScreen>();

		/// <summary>
		/// The _game screens list.
		/// </summary>

		[SerializeField] GameScreen [] _gameScreensList;  

		/// <summary>
		/// The persistent hud.
		/// </summary>

		[SerializeField] GameScreen _persistentHud;


		/// <summary>
		/// The container screen.
		/// </summary>
		[SerializeField] GameScreen _containerScreen;

		/// <summary>
		/// This is the method that is invoked when the user escapes. 
		/// </summary>
		
		private List<System.Action> _screenEscapes = new List<System.Action>();


		/// <summary>
		/// The verb coin panel.
		/// </summary>

		[SerializeField] VerbCoinPanel _verbCoinPanel; // TODO: Do this better

		// Create a list of all screens in our game
		// In theory all screens will have a unity canvas attached - This may change if I want to create a custom type
		//List<GameScreen> _screens = new List<GameScreen>();
		#endregion

		#region member properties

		// Properties
		//public VerbCoinPanel VerbCoinPanel { get { return _verbCoinPanel; } }
	
		#endregion

	
		#region static properties
		// TODO: Replace static properties for instance properties

		// Return singleton of the active screen manager
		static public ScreenManager Instance { get { return _instance; } } 

	
		#endregion

		#region members
		// Use this for initialization
		void Start () {

		
		
		}

		void Awake() {

			// If there is not active screen manager then set it to this instance - there should only be one
			if (_instance == null) {
				_instance = this;
				
				// Add Game Screens at start
				AddGameScreens ();
			}

		}

		/// <summary>
		/// Adds the game screens to the gameScreens collection.
		/// This is intended to be invoked at the start of the application
		/// </summary>

		void AddGameScreens() {
		
			//Debug.Log ("Add Game Screens");

			// Get all objects of type gamescreen in the scene
			IGameScreen [] gameScreens = GameObject.FindObjectsOfType(typeof(GameScreen)) as IGameScreen [];

			// Search through each game screen in the scene and add it to our collection
			foreach (IGameScreen gameScreen in gameScreens) {

				//Debug.Log("Screen Name: " + gameScreen.Name);

				try {
					// Object may not get added - most likely reason is that the user either
					// gave the same name to an object or added the same component to an object twice
					_gameScreens.Add(gameScreen.Name, gameScreen);
				}
				catch (System.ArgumentException argumentException) {
					Debug.LogWarning("Duplicate Key Names: Was unable to add the gamescreen ''" + gameScreen.Name + "'' to the game screen collection. This is likely because two gamescreens share the exact same name your gameobject has the same component added to it multiple times.");
					throw (argumentException);	
				}
				catch {
						Debug.LogError ("Unable to add a gamescreen to the _gameScreens list");
				}

			}

		}
		
		// Update is called once per frame
		void Update () {

			
			// If the escape key is presed then handle
			
			if(Input.GetKeyDown(KeyCode.Escape)) {
				
				if (_screenEscapes.Count > 0) {
					
					_screenEscapes[_screenEscapes.Count - 1].Invoke();
					_screenEscapes.Remove(_screenEscapes[_screenEscapes.Count - 1]);

				}
				else {
					
					// Escape is null, use default behavior
					
				}
			}

		
		}

		/// <summary>
		/// Receives the escape delegate.
		/// </summary>
		/// <param name="escape">Escape.</param>

		
		internal void ReceiveEscapeDelegate(System.Action escape) {

			// Check to see if it's already added, if so, then remove  it  so that when added it will simply be moved to the front of the line
			if (_screenEscapes.Contains (escape)) {
				_screenEscapes.Remove(escape);
			}

			_screenEscapes.Add (escape);
			
		}


		/// <summary>
		/// Closes all screens.
		/// </summary>
		
		public void CloseAllScreens() {

			// Search through all screens and remove
			foreach (KeyValuePair<string, IGameScreen> gameScreenKVP in _gameScreens) {
				CloseScreen(gameScreenKVP.Value);
			}

		}

		/// <summary>
		/// Closes the screen.
		/// </summary>
		/// <param name="key">Key.</param>

		public void CloseScreen(string key) {

			//Debug.Log ("Close Screen: " + key);
		
			try {
				CloseScreen (_gameScreens [key]);
			}
			catch {

				Debug.Log ("Could not find screen in dictionary, using find screen to get the screen instead");

				GameScreen gameScreen = FindScreen(key);

				if(gameScreen != null) {				
					CloseScreen(gameScreen);
				}
				else {
					Debug.LogError("Could not locate the game screen '" + key + "'");
				}
			}
		
		}

		/// <summary>
		/// Closes the screen.
		/// </summary>
		/// <param name="screen">Screen.</param>

		public void CloseScreen(IGameScreen screen) {

			//Debug.Log ("Close Screen");

			//Debug.Log ("Close Screen: " + screen);

			// Open the save window
			if(screen != null) {
			//	Debug.Log ("Game Screen is not null");
				screen.Close();
			}
			else { // Error: No save window
				Debug.LogError ("Error: Cannot find the specified screen. Did you remember to set up the screen game object to the GameController in the inspector?");
			}
		}

		/// <summary>
		/// Displays a message to a specified screen.
		/// </summary>
		/// <param name="screenName">Screen name.</param>
		/// <param name="message">Message.</param>

		public void DisplayMessage(string screenName, string message) {

			//Debug.Log ("Display Message - screenName: " + screenName);

			//Debug.Log ("Display Message");
		
			//Debug.Log (message);

			// Find the the specified message screen from the game screen collection
			IMessageScreen messageScreen = null;
			IGameScreen gameScreen;
			//messageScreen = (IMessageScreen)_gameScreens [screenName];

			// Try to get the gamescreen of the given key
			_gameScreens.TryGetValue (screenName, out gameScreen);

			// Get the message screen interface
			if(gameScreen != null){messageScreen = (IMessageScreen)gameScreen;}
			else {
				gameScreen = FindScreen(screenName);
				messageScreen = (IMessageScreen)gameScreen;
			}

			// Display the message
			if (messageScreen != null) {
				messageScreen.Open(message);
			}
			else {
					Debug.LogError("Could not locate the screen " + screenName);
			}

		}

		/// <summary>
		/// Displays the message.
		/// </summary>
		/// <param name="messageScreen">Message screen.</param>
		/// <param name="message">Message.</param>

		public void DisplayMessage(IMessageScreen messageScreen, string message) {
		
			// Display the message
			if (messageScreen != null) {
				messageScreen.Open(message);
			}
			else {
				Debug.LogError("Null reference. Could not locate the specified screen.");
			}


		
		}


		/// <summary>
		/// Displays a message.
		/// </summary>
		/// <param name="messageScreen">Message screen.</param>
		/// <param name="message">Message.</param>
		/// <param name="options">Options.</param>

		public void DisplayMessage(IMessageOptionsScreen messageScreen, string message, IOption [] options) {


			if (messageScreen != null) {
			
				messageScreen.Open(message, options);

			}
			else {
				Debug.LogError("Null reference. Could not locate the specified screen.");
			}

		
		}

		/// <summary>
		/// Opens a message options screen.
		/// </summary>
		/// <param name="screenName">Screen name.</param>
		/// <param name="message">Message.</param>
		/// <param name="options">Options.</param>

		public void OpenMessageOptionsScreen(string screenName, string message, IOption [] options) {
		
			IMessageOptionsScreen screen = (IMessageOptionsScreen)FindScreen (screenName);

			if (screen != null) {
				screen.Open (message, options);
			}
			else {
				Debug.Log ("Screen not found");
			}


		}
			
		/// <summary>
		/// Gets the message options screen.
		/// </summary>
		/// <returns>The message options screen.</returns>
		/// <param name="screenName">Screen name.</param>
		/*
		public IMessageOptionsScreen GetMessageOptionsScreen(string screenName) {
		
			// Find the the specified message screen from the game screen collection
			IMessageOptionsScreen messageScreen = null;
			IGameScreen gameScreen;
			//messageScreen = (IMessageScreen)_gameScreens [screenName];
			
			// Try to get the gamescreen of the given key
			_gameScreens.TryGetValue (screenName, out gameScreen);
			
			// Get the message screen interface
			if(gameScreen != null){messageScreen = (IMessageOptionsScreen)gameScreen;}
			
			return messageScreen;

		}*/

		/// <summary>
		/// Opens the screen.
		/// </summary>
		/// <param name="key">Key.</param>

		// TODO: Change this to activate the screen differently (With IKeyed method)
		// TODO: Create a screen manager interface

		public void OpenScreen(string key) {
		
			// Invoke OpenScreen(GameScreen gameScreen) method which will check to see
			// if the referenced screen is null
			try {
				OpenScreen (_gameScreens [key]);
			}
			catch {

//				Debug.Log ("Failed to find screen. Trying to find screen in list");
				GameScreen gameScreen = FindScreen(key);

				if(gameScreen != null) {
					OpenScreen(gameScreen);
				}

			}

		}

		/// <summary>
		/// Find a game screen by key name
		/// </summary>
		/// <returns>The screen.</returns>
		/// <param name="key">Key.</param>


		// TODO: Don't need to search through entire list

		GameScreen FindScreen(string key) {

			//Debug.Log("Find Screen");

			foreach (GameScreen gameScreen in _gameScreensList) {
			
				//Debug.Log ("GameScreen: " + gameScreen.name);

				// TODO: Force Game Screens to be Keyed and search by key
				if(gameScreen.name == key) {
				
					//Debug.Log ("Screen returned: " + key);
					return gameScreen;

				}
			
			}

			return null;
		
		}

		/// <summary>
		/// Opens the screen.
		/// </summary>
		/// <param name="screen">Screen.</param>

		// TODO: Refactor accordingly

		public void OpenScreen(IGameScreen gameScreen) {
			
			// Open the save window
			if(gameScreen != null) {
				gameScreen.Open();
			}
			else { // Error: No save window
				Debug.LogError ("Error: Cannot find the specified screen. Did you remember to set up the screen game object to the GameController in the inspector?");
			}
		}
		#endregion
	}
}
