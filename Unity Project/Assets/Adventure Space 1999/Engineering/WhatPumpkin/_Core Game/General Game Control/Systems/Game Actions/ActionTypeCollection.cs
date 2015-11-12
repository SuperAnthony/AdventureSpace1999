#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Created - December 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Actions {

	public class ActionTypeCollection  {

		#region static fields

		/// <summary>
		/// The singleton instance of the ActionTypeCollection.
		/// </summary>

		static ActionTypeCollection _instance;

		#endregion


		#region fields 
		/// <summary>
		/// The collection of action types.
		/// </summary>
		List<ActionType> _actionTypes = new List<ActionType>(); 

		// Static Properties
		public static ActionTypeCollection Instance { get { return _instance; } }

		// Properties

		public List<ActionType> ActionTypes { get { return _actionTypes; } }

		// Indexers

		public ActionType this[string actionName] {
		
			get {
				// Search through all action types by name
				foreach(ActionType actionType in _actionTypes) {
				
					//Debug.Log (actionType.Name);

					// Return the action type if there's a match
					if(actionType.Name == actionName) {

						return actionType;
					}
				}

				// Did not find a match
				// Log an error and return null
				if(actionName != "") {
					Debug.LogError(actionName + " is not a valid action found. Please check spelling in database");
				}

				return null;
			}
		}

		#endregion

		#region methods

		/// <summary>
		/// Constructor
		/// Initializes a new instance of the <see cref="WhatPumpkin.HiveSwap.Actions.ActionTypeCollection"/> class.
		/// </summary>

		public ActionTypeCollection () {
			// Use this instance as the singleton object
			_instance = this;

			// Initialize a collection of actions
			AddAllActionsToCollection ();


		}

		/// <summary>
		/// Gets ActionType of the given name.
		/// </summary>
		/// <returns>The action type.</returns>
		/// <param name="actionName">Action name.</param>

		public ActionType GetActionType(string actionName) {

			
				// Search through all action types by name
				foreach(ActionType actionType in _actionTypes) {
					
					//Debug.Log (actionType.Name);
					
					// Return the action type if there's a match
					if(actionType.Name == actionName) {
						
						return actionType;
					}
				}
				
				// Did not find a match
				// Log an error and return null
				if(actionName != "") {
					Debug.LogError(actionName + " is not a valid action found. Please check spelling in database");
				}
				
				return null;
			
		
		
		}

		/// <summary>
		/// Adds all actions to the collection of action types.
		/// </summary>

		public void AddAllActionsToCollection() {

			_actionTypes.Add(new AutoSave());
			_actionTypes.Add(new Activate());
			_actionTypes.Add(new AddScene());
			_actionTypes.Add (new Continue ());
			_actionTypes.Add (new Deactivate ());
			_actionTypes.Add (new GetVar ());
			_actionTypes.Add(new Talk ());
			_actionTypes.Add(new MoveToTarget ());
			_actionTypes.Add(new SetNarratorText ()); // Name is "SetNar"
			_actionTypes.Add(new Use());
			_actionTypes.Add(new Disable());
			_actionTypes.Add(new Enable());
			_actionTypes.Add(new AddVar());
			_actionTypes.Add(new SetVar());
			_actionTypes.Add(new Stop());
			_actionTypes.Add(new StopMoving());
			_actionTypes.Add(new Start());
			_actionTypes.Add(new StartFX());
			_actionTypes.Add(new SetFOV()); // Name is "SetNar"
			_actionTypes.Add(new SwitchHand());
			_actionTypes.Add(new Play());
			_actionTypes.Add(new PlayAnimation ()); 
			_actionTypes.Add(new PlayFX());
			_actionTypes.Add(new Randomize());
			_actionTypes.Add(new StartAnimation());
			_actionTypes.Add(new RemoveItemFromActivePC());
			_actionTypes.Add(new AddItemToActivePC()); // Name is "Get"
			_actionTypes.Add(new OpenContainer());
			_actionTypes.Add(new PlayCutScene());
			_actionTypes.Add(new StartBark());
			_actionTypes.Add(new ChangeScene());
			_actionTypes.Add(new Closeup());
			_actionTypes.Add (new PlayBark ());
			_actionTypes.Add (new SetTarget ());
			_actionTypes.Add (new SetVar ());
			_actionTypes.Add (new EndCloseup ());
			_actionTypes.Add (new SetMaterial ()); // "SetMat"
			_actionTypes.Add (new PlayActionSequence ()); // "PlayActSeq" TODO: Remove
			_actionTypes.Add (new Enter());
			_actionTypes.Add (new ChangePCState()); 
			_actionTypes.Add (new JoinParty());
			_actionTypes.Add (new OpenMessage());

			// Diagnostics
			_actionTypes.Add (new ListKeys());

		}

		#endregion

	}
}