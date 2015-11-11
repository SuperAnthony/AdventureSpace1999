#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 13, 2014
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// For types of actions that can be performed 
	/// Actions should be easy for developers (writers, designers, etc)
	/// to invoke, such as, when hot spots are
	/// All actions will require a begin method and an end method
	/// </summary>

	public abstract class ActionType : ArgumentReceiver {

		#region implement ArgumentReceiver

		/// <summary>
		/// Gets a value indicating whether this instance has unlimited arguments.
		/// </summary>
		/// <value><c>true</c> if this instance has unlimited arguments; otherwise, <c>false</c>.</value>
		
		protected override bool HasUnlimitedArguments {get { return true; } } // Set Default
		
		
		/// <summary>
		/// Gets the max arguments if the instance does not have unlimited arguments.
		/// </summary>
		/// <value>The max arguments.</value>
		
		protected override int MaxArguments { get { return 1; } } // Set Default

		#endregion

		/// <summary>
		/// Delegate for completed event
		/// </summary>

		public delegate void Completed(params object[] parameters); 

		/// <summary>
		/// Delegate for start event
		/// </summary>
		
		public delegate void StartEvent(params object[] parameters);

		// Fields

		/// <summary>
		/// Occurs when action completed.
		/// </summary>

		public event Completed _onActionEnd; 


		/// <summary>
		/// The name of the action.
		/// </summary>
		
		protected string _name;

		// Properties

		/// <summary>
		/// Occurs when the action is completed.
		/// Subscribe methods to this event to invoke the action is complete
		/// </summary>

		public event Completed OnActionEnd { add {_onActionEnd += value;} remove { _onActionEnd -= value; } }


		/// <summary>
		/// Occurs when on action begin.
		/// </summary>

		//public event StartEvent OnActionBegin { add {_onActionBegin += value;} remove { _onActionBegin -= value; } }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>

		public override string Name { get { return _name; } }

		// Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.HiveSwap.Actions.Action"/> class.
		/// </summary> 

		public ActionType() {
		
		

		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="WhatPumpkin.Actions.ActionType"/> is reclaimed by garbage collection.
		/// </summary>

		~ActionType() {

			// Unregister Key Created and Key Destroyed events
			Keyed.KeyedObjectCreated -= HandleKeyCreated;
			Keyed.KeyedObjectDestroyed -= HandleKeyCreated;
		}

		/// <summary>
		/// Performs the action.
		/// </summary>

		public void PerformAction(params object [] parameters) {

			BeginAction (parameters);
			EndAction ();
		}

		/// <summary>
		/// Begin the action.
		/// </summary>
		/// <param name="">.</param>

		public abstract IEnumerator BeginAction(params object[] parameters);

		/// <summary>
		/// Ends the action.
		/// </summary>
		/// <param name="parameters">Parameters.</param>

		protected  void EndAction(params object[] parameters) {
			//Debug.Log ("Action Ended");
			if(_onActionEnd != null){_onActionEnd.Invoke();}
		}

		static protected List<string> GetArguments(string arguments) {
			return ScriptingLanguage.Scripting.GetArguments(arguments);
		}


	}
}