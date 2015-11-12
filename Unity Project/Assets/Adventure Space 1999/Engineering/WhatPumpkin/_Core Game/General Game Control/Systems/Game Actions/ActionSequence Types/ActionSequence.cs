#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 14, 2014
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities;
using WhatPumpkin.ScriptingLanguage;
using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin.Actions.Sequences {

	[System.Serializable]


	/// <summary>
	/// Action sequence. 
	/// A class that stores a list of actions
	/// </summary>

	public class ActionSequence : Keyed, IFrequency, IPerform   {

		#region constants

		const string KEY_PREFIX = "AS_";

		#endregion

		#region delegates and events

		// This is very messy refactor these events

		/// <summary>
		/// Occurs when action sequence played.
		/// </summary>

		static public event ActionSequenceEvent ActionSequencePlayed;

		/// <summary>
		/// Occurs when an action sequence attempted to play (may not play if conditions are not met).
		/// </summary>

		static public event System.Action ActionSequenceAttempted;



		static List<ActionSequence> _sequencesPlaying = new List<ActionSequence>();
		static public int NumSequencesPlaying { get { return _sequencesPlaying.Count; } }

		public delegate void ActionSequenceEvent(ActionSequence actionSequence);

		/// <summary>
		/// Occurs when sequence ended.
		/// </summary>

		public event ActionSequenceEvent SequenceEnded;

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets the play order.
		/// </summary>
		/// <value>The play order.</value>
		
		public int PlayOrder { get; set;}


		#endregion

		#region fields

		/// <summary>
		/// The key.
		/// </summary>

		//[SerializeField] protected string _key;

		/// <summary>
		/// The _conditions.
		/// </summary>

		[SerializeField] protected string _conditions;

		/// <summary>
		/// List of actions that will be performed
		/// </summary>

		[SerializeField] protected List<Action> _actions = new List<Action>();

		/// <summary>
		/// The required items (by key) in the active player's inventory to perform this action sequence.
		/// </summary>
		
		[SerializeField] protected string _requiredItems;

		/// <summary>
		/// The current action.
		/// </summary>

		int _currentAction;


		/// <summary>
		/// How often did this sequence occur.
		/// </summary>
		[SerializeField] protected int _played = 0;

		/// <summary>
		/// The frequency this action sequence can be played (for instance can this be played once, always etc...).
		/// </summary>

		protected string _frequency;

		#endregion

		#region properties

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
	
		public override string Key { 
			get 
			{ 
				// Prevent a null reference
				if(_key == null) {return "";}

				return _key; 
			} 
		}

		/// <summary>
		/// Gets the conditions.
		/// </summary>
		/// <value>The conditions.</value>

		public string Conditions { 
			get {

				// Prevent a null reference
				if(_conditions == null) {return "";
				}

				return _conditions; 
			} 
		}

		/// <summary>
		/// Gets a list of required items keys.
		/// </summary>
		/// <value>The required items.</value>
		
		public List<string> RequiredItems { get { return Scripting.GetArguments (_requiredItems); } }

		/// <summary>
		/// Gets the actions.
		/// </summary>
		/// <value>The actions.</value>

		public List<Action> Actions { get { return _actions; } }

		/// <summary>
		/// Gets the frequency.
		/// </summary>
		/// <value>The frequency.</value>

		public string Frequency { get 
			{

				// Prevent null reference
				if(_frequency == null) {
					return "";
				}


				return _frequency; 
			} 
		}

		#endregion

		#region methods

		public ActionSequence() {

			_played = 0;

			// Retrieve the number of times this action sequence has played
			// TODO: This may need to be saved 
			/*
			if (DialogueLua.DoesVariableExist (_key+"_played")) {
				_played = DialogueLua.GetVariable(_key+"_played").AsInt;
			}*/

		}

		/// <summary>
		/// Reset this instance. This was created for the ECCC demo.
		/// </summary>

		public void Reset() {
		
			_played = 0;


		}

		/// <summary>
		/// Checks to see if Conditionses are met.
		/// </summary>
		/// <returns><c>true</c>, if conditions were met, <c>false</c> otherwise.</returns>

		public bool ConditionsAreMet() {

			return (Scripting.AreConditionsMet (_conditions) && ActionUtils.MeetsFrequencyConditions(this, _played));

		}

		/// <summary>
		/// Plays the action sequence.
		/// </summary>

		private IEnumerator PlayActionSequence() {

			// TODO: Change to void and deprecate 

			if(ConditionsAreMet()) {

				// Let the game know that this action sequence has occured again

				_played++;
				// Set LUA played variable to keep track of the number of times this action sequence played
				DialogueLua.SetVariable(_key+"_played", _played);
				// Set to the current action to the first (0)
				_currentAction = 0;
				// Play the next action in the sequence
				PlayNextAction ();
			
			}
			else { // If the action sequence sequences aren't complete then complete
			
				// The conditions are met, broadcast that this instance performance is complete
				OnSequenceComplete();
			
			}

			return null;
		}

		#region IPerform

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>

		public event EventHandler FinishedPlaying;

		/// <summary>
		/// Play this instance.
		/// </summary>

		public void Play() {

			//Debug.Log ("Play Action Sequence");

			_sequencesPlaying.Add (this); // This is to keep track of which sequences are playing
			PlayActionSequence ();
		
		}

		/// <summary>
		/// Stop this instance.
		/// </summary>

		public virtual void Stop() {
		
			// TODO:

		}

		/// <summary>
		/// Pause this instance.
		/// </summary>

		public virtual void Pause() {
		
			// TODO:

		}

		/// <summary>
		/// Resume this instance.
		/// </summary>

		public virtual void Resume() {
		
			// TODO:

		}


		#endregion

		/// <summary>
		/// Raises the action sequence played event.
		/// </summary>
		/*
		static void OnActionSequencePlayed() {
		
			if (ActionSequencePlayed != null) {
				ActionSequencePlayed.Invoke();
			}

		}*/

		/// <summary>
		/// Raises the action sequence attempted event.
		/// </summary>

		static void OnActionSequenceAttempted() {

			if (ActionSequenceAttempted != null) {
				ActionSequenceAttempted.Invoke();
			}

		}

		/// <summary>
		/// Plays the next action in the sequence.
		/// This will get invoked when the previous action has ended
		/// If there is no next action the method will just do nothing
		/// </summary>

		void PlayNextAction() {

			// Check to see that there really is another action
			if (_currentAction < _actions.Count) {

			
				// Check to see if we can play the current action
				// If we can't then we'll skip it, skip it, hey now kids come gather 'round... 
				if (!CanPlayCurrentAction ()) {
					SkipAction();
				}
				else {

				
					ActionType actionType = ActionTypeCollection.Instance[_actions[_currentAction].ActionType];
					
					if(actionType != null) {
						
						// Subscribe the OnCurrentActionEnd event
						actionType.OnActionEnd+=OnCurrentActionEnd;

						// Begin the action pass along the action type the correct parameters
						ActionController.Instance.PerformAction(actionType, _actions[_currentAction].Parameters);
					}
					else {
						// TODO: Cannot find an action but should - Error
					}
				}
			}
			else {
		
				// The action sequence is complete, end
				OnSequenceComplete();

			}
		}

		/// <summary>
		/// Broadcasts the end of the action sequence.
		/// </summary>

		void OnSequenceComplete() {

			//Debug.Log ("Sequence Complete: " + this.Key);

			// Invoke the static sequence ended method
			if (SequenceEnded != null) {SequenceEnded.Invoke (this);} // Static
			// Invoke the instance Finished Playing method
			if (FinishedPlaying != null) {
				FinishedPlaying.Invoke(this, null); // Instnace
			//	Debug.Log ("Finished Playing Invoked");
			}
			else {
			//	Debug.Log ("Finished Playing Not Invoked");
			}

			// Invoke the action sequence played method (static message)
			if (ActionSequencePlayed != null) {ActionSequencePlayed(this);} // TODO: This is too much

			try{
				_sequencesPlaying.Remove (this);
			}
			catch {
			//	Debug.Log ("Can't remove sequence from list");			
			}
		}

		/// <summary>
		/// Determines whether this instance can play the current action.
		/// </summary>
		/// <returns><c>true</c> if this instance can play action; otherwise, <c>false</c>.</returns>

		bool CanPlayCurrentAction() {

			// Check to see if the conditionals are met, if not return false
			if(!Scripting.AreConditionsMet(_actions[_currentAction].Conditions)){return false;}

			// Make sure the player has the required items
			if (GameController.PartyManager.ActivePC != null) {
								if (!GameController.PartyManager.ActivePC.Inventory.HasItems (RequiredItems)) {
										return false;
					}
			}

			return ActionUtils.MeetsFrequencyConditions (_actions [_currentAction], _played);
		
		}

		/// <summary>
		/// Skips the action.
		/// </summary>

		void SkipAction() {
			// Set to next action
			_currentAction++;
			// Play the next action in the sequence
			PlayNextAction ();
		}


		/// <summary>
		/// Raises the current action end event. When the action that is currently playing end, this gets raised.
		/// And then moves us along to the next action in the sequence.
		/// </summary>

		void OnCurrentActionEnd(params object[] parameters) {

			// Clear the action type complete event
			ActionType actionType = ActionTypeCollection.Instance[_actions[_currentAction].ActionType];
			actionType.OnActionEnd -= OnCurrentActionEnd;

			// Set to next action
			_currentAction++;

			// Play the next action in the sequence
			PlayNextAction ();
		}


		#endregion


		// The following are primarily, if not exclusively, for sgrid actions

		/// <summary>
		/// Removes the action.
		/// </summary>
		/// <param name="action">Action.</param>

		public void RemoveAction(Action action) {
		
			try {
			_actions.Remove (action);
			}
			catch {
			
				Debug.LogError("Could not find the specified action in the list");

			}
		}

		/// <summary>
		/// Adds an action to the action sequence list.
		/// </summary>
		
		public void AddAction() {

			Debug.Log ("Add Action");

			_actions.Add (new Action ());
			
		}

		/// <summary>
		/// Adds an action to the action sequence list.
		/// </summary>
		/// <param name="actionType">Action type.</param>
		/// <param name="actionParams">Action parameters.</param>
		/// <param name="actionConditions">Action conditions.</param>
		/// <param name="actionFrequency">Action frequency.</param>

		public void AddAction(string actionType, string actionParams, string actionConditions = "", string actionFrequency = "") {

			Action action = new Action ();
			action.SetProperties(actionType, actionParams, actionConditions, actionFrequency); 
			_actions.Add (action);

		}

		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="conditions">Conditions.</param>
		/// <param name="frequency">Frequency.</param>

		public virtual void SetProperties(string key, string conditions, string frequency) {
		
			// Prevent Blank Key and make certain it begins with hte required prefix
			if (!key.StartsWith (KEY_PREFIX)) {
				key = KEY_PREFIX + key;
			}

			// Change key only if it differs
			if (_key != key) {
				this.RenameKey(key);
						}

			_conditions = conditions;
			_frequency = frequency;

		}


	}
}
