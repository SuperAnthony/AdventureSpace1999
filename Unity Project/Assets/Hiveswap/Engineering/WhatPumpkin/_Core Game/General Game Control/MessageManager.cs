#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 17, 2014
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using WhatPumpkin.Data.XML;
using WhatPumpkin.Localization;
using WhatPumpkin.Screens; 
#endregion

namespace WhatPumpkin {


	/// <summary>
	/// Text type.
	/// </summary>
	// TODO: Don't hard code these strings, set them up in the developer settings
	public enum TextType {VERB_, NAR_, BARK_, HST_,TXT_};


	/// <summary>
	/// Message management
	/// Intended to be attached to the game controller which will manage messages.
	/// Messages include, Narrator Text, Character Text
	/// </summary>

	public class MessageManager : MonoBehaviour, IMessageManager<string> {

		#region constants

		const string MULTI_PROMPT_MESSAGE = "MultiPrompt Screen";

		#endregion

		#region member fields

		public event EventHandler NarrativeMessageStopped;

		/// <summary>
		/// The narrator text collection.
		/// </summary>
		[SerializeField] LocalizedText[] _narratorTextCollection;

		/// <summary>
		/// The verb text collection.
		/// </summary>

		[SerializeField] LocalizedText [] _verbTextCollection;

		/// <summary>
		/// The bark text collection.
		/// </summary>

		[SerializeField] LocalizedText [] _barkTextCollection;

		/// <summary>
		/// The hot spot text collection.
		/// </summary>

		[SerializeField] LocalizedText [] _hotSpotTextCollection;

		/// <summary>
		/// The _effects and user interface text collection.
		/// </summary>

		[SerializeField] LocalizedText [] _effectsAndUITextCollection;

		/// <summary>
		/// The duration of the narrative text.
		/// </summary>

		[SerializeField] float _narrativeTextDuration;



		/// <summary>
		/// The bark that will get instantiated and displayed by the message manager
		/// </summary>

		[SerializeField] Bark _bark;

		/// <summary>
		/// The options.
		/// </summary>

		[SerializeField] ASOption [] _options;

		/// <summary>
		/// The duration of the _min bark.
		/// </summary>

		[SerializeField] float _minBarkDuration; // TODO: Not getting used?

		/// <summary>
		/// The duration of barks.
		/// </summary>

		[SerializeField] float _barkDuration; // TODO: Not getting used?

	
		/// <summary>
		/// The current narrator message collection.
		/// </summary>

		string[]_currentNarMessageCollection;

		/// <summary>
		/// The current message in the collection.
		/// </summary>

		int _currentMessage = 0;


		#endregion

		#region member properties

		/// <summary>
		/// Gets the narrator text collection.
		/// </summary>
		/// <value>The narrator text collection.</value>

		public LocalizedText[] NarratorTextCollection { get { return _narratorTextCollection; } }

		/// <summary>
		/// Gets the verb text collection.
		/// </summary>
		/// <value>The verb text collection.</value>

		public LocalizedText[] VerbTextCollection{ get { return _verbTextCollection; } }

		/// <summary>
		/// Gets the bark text collection.
		/// </summary>
		/// <value>The bark text collection.</value>

		public LocalizedText[] BarkTextCollection{ get { return _barkTextCollection; } }

		/// <summary>
		/// Gets the hot spot text collection.
		/// </summary>
		/// <value>The hot spot text collection.</value>
		
		public LocalizedText[] HotSpotTextCollection{ get { return _hotSpotTextCollection; } }

		/// <summary>
		/// Gets a value indicating whether this instance is showing a narrator message.
		/// </summary>
		/// <value><c>true</c> if this instance is showing narrator message; otherwise, <c>false</c>.</value>

		public bool IsShowingNarratorMessage { get; private set; }

		#endregion

		#region static fields

		/// <summary>
		/// The instance of the message manager.
		/// </summary>

		static public MessageManager _instance; 

		#endregion

		#region static properties

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
			 
		static public MessageManager Instance { get { return _instance; } } 

		#endregion

		#region methods
		void Update() {


		}

		/// <summary>
		/// Gets the localized text from a collection based on the key and language.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="key">Key.</param>
	
		public virtual string GetMessage(string key) {

//			Debug.Log ("Get Message: " + key);

			// The localized text that will be returned 
			ILocalizedText localizedText = null;
			// Which text collection will we reference (Verb, Narrator, Bark, etc.)
			ILocalizedText [] textCollection = null;
			textCollection = GetMessageCollectionType (key);

		

			if(textCollection != null) {
				// Search through each verb collection and check to see if the keys match
				foreach(ILocalizedText lText in textCollection) {

					if(key == lText.Key) {
						// If the keys match then set the localizedText to the lText of the matching key
						localizedText = lText;
					}
				}
			}
		
		

				// Make sure there's a localizedText
				if(localizedText != null) {
					// Retrun the string with the appropriate language
					return localizedText.Text[(int)GameController.BuildSettings.Language];
				}


				return key;
		

		}

		/// <summary>
		/// Gets the type of the message collection by they key.
		/// </summary>
		/// <returns>The message collection type.</returns>
		/// <param name="key">Key.</param>

		LocalizedText [] GetMessageCollectionType(string key) {

			if(key != null && key != "") {

				if (key.StartsWith (TextType.VERB_.ToString ())) {
					return _verbTextCollection;
				}
				
				if (key.StartsWith (TextType.NAR_.ToString ())) {
					return _narratorTextCollection;
				}
				
				if (key.StartsWith (TextType.BARK_.ToString ())) {
					return _barkTextCollection;
				}

				if (key.StartsWith (TextType.HST_.ToString ())) {
					return _hotSpotTextCollection;
				}

				if (key.StartsWith (TextType.TXT_.ToString ())) {
					return _effectsAndUITextCollection;
				}

			}
			return null;
		}

		/// <summary>
		/// Displays the narrator message.
		/// </summary>
		/// <param name="message">Message.</param>

		public void StartNarratorMessage(string key) {


		
			// Get a list of messages
			try {

			

			string indicator = "//";
			_currentNarMessageCollection = GetMessage (key).Split (indicator.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			}
			catch {
			
				Debug.LogError ("Failed to get narrator message");

			}



			_currentMessage = 0;


			// Show
			/*
			foreach (string message in _messages) {
				Debug.Log (message);
			}*/



			//Debug.Log ("Nar Message: " + _currentNarMessageCollection [_currentMessage]);

			ShowLocalizedNarratorMessage (_currentNarMessageCollection[_currentMessage]);


		}

		/// <summary>
		/// Goto and display the next message in the current message collection
		/// </summary>
		/// <returns><c>true</c>, if there was a next message to go to, <c>false</c> otherwise.</returns>

		bool GotoNextMessage() {
		
			// Go to the next message
			_currentMessage++;
			// Check to see if it exist
			if (_currentMessage < _currentNarMessageCollection.Length) {
				// Display the localized text
				ShowLocalizedNarratorMessage(_currentNarMessageCollection[_currentMessage]);
				// Message does exist so return true
				return true;
			}


			// Message does not exist so return false
			return false;
		

		}

		/// <summary>
		/// Raises the clicked next message event.
		/// </summary>

		public void OnClickedNextMessage() {

			if(!GotoNextMessage()){StopNarratorMessage();}
		}

		/// <summary>
		/// Stops the narrator message.
		/// </summary>

		void StopNarratorMessage() {

			if (NarrativeMessageStopped != null) {NarrativeMessageStopped(this, null);}

			IsShowingNarratorMessage = false;
			_currentNarMessageCollection = null;
			_currentMessage = 0;
			ScreenManager.Instance.CloseScreen ("NarratorScreen"); // TODO: I don't like this
		}

		/// <summary>
		/// Shows the localized narrator message.
		/// </summary>
		/// <param name="message">Message.</param>

		void ShowLocalizedNarratorMessage(string message) {

			//Debug.Log ("Show Localized Narrator Message: " + message);

			IsShowingNarratorMessage = true;
			ScreenManager.Instance.DisplayMessage ("NarratorScreen", message); 
		
		}

		// TODO: Remove this

		public void ShowHotSpotRollover(string key) {
		
			// TODO: Remove hard coding
			ScreenManager.Instance.DisplayMessage ("HotSpotRolloverScreen", GetMessage(key));
		}

		public void HideHotSpotRollover() {
		
			ScreenManager.Instance.CloseScreen ("HotSpotRolloverScreen");
		
		}


		public Bark Bark(string key, Vector3 pos, float duration = 3F ) {

			// Instantiate a bark
			Bark bark = (Bark)GameObject.Instantiate (_bark);

			// Get the Main Camera (Ugh!!!)
			Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

			// Set the bark's properties
			Debug.Log ("Position: " + camera.WorldToScreenPoint (pos));



			ScreenManager.Instance.DisplayMessage (bark, GetMessage(key));
			Debug.Log ("Bark: " + bark.name);
			bark.SetProperties (camera.WorldToScreenPoint(pos), duration);

			return bark;
		}

		/// <summary>
		/// Opens the multi option message.
		/// </summary>
		/// <param name="messageKey">Message key.</param>
		/// <param name="optionKeys">Option keys.</param>

		public void OpenMultiOptionMessage(string messageKey, string [] optionKeys) {

			// List of options
			IOption [] options = new IOption[optionKeys.Length];

			// Generate a list of the options from the option keys
			for (int i = 0; i < optionKeys.Length; i++) {

	

				options[i] = (IOption)Keyed.FindInCollection<IOption>(optionKeys[i], _options);

			}

			GameController.ScreenManager.OpenMessageOptionsScreen (MULTI_PROMPT_MESSAGE, messageKey, options);
		
		}




		public void ShowHotSpot(string key) {

			ScreenManager.Instance.DisplayMessage ("HotSpotRolloverScreen", GetMessage (key)); 
		
		}

		// Use this for initialization
		void Start () {

			IsShowingNarratorMessage = false;
		
		}

		void Awake() {
			// Initiate singleton instance 
			_instance = this;
		}

		/// <summary>
		/// Parses the XML data.
		/// </summary>
		/// <returns><c>true</c>, if XML data was parsed, <c>false</c> otherwise.</returns>
		/// <param name="xmlData">Xml data.</param>

		public bool ParseXMLData(XmlDocument xmlData, params object[] parameters) {

//			Debug.Log ("Message Manager: Parse XML Data");

			// Get the parent node
			XmlNode parentNode = xmlData.ChildNodes [0];
		
			

			// Parse node data into the new action sequences
			if(parameters[0].ToString() == "Verb Text") {
				GoogleDocToXMLFormatParser.Parse<LocalizedText>(out _verbTextCollection, parentNode);
				return true;
			}

			if(parameters[0].ToString() == "Narrator Text") {

//				Debug.Log ("Narrator Text");

				GoogleDocToXMLFormatParser.Parse<LocalizedText>(out _narratorTextCollection, parentNode);
//				Debug.Log ("Done Parsing");
				//Debug.Log (_narratorTextCollection[0].Key);

				return true;
			}
			/*
			if(parameters[0].ToString() == "Bark Text") {
				GoogleDocToXMLFormatParser.Parse<LocalizedText>(out _barkTextCollection, parentNode);
				return true;
			}

			if(parameters[0].ToString() == "HotSpot Text") {
				GoogleDocToXMLFormatParser.Parse<LocalizedText>(out _hotSpotTextCollection, parentNode);
				return true;
			}

			// TODO: Load This
			if(parameters[0].ToString() == "UI Text") {
				GoogleDocToXMLFormatParser.Parse<LocalizedText>(out _hotSpotTextCollection, parentNode);
				return true;
			}*/


			return false;




		}


	}

	#endregion
}
