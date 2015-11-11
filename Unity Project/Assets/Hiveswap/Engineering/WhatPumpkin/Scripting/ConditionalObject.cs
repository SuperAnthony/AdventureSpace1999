#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 4, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion


namespace WhatPumpkin.ScriptingLanguage {

	[System.Serializable]

	/// <summary>
	/// Conditional group that are meant to be activated provided certain conditions are met.
	/// </summary>

	public abstract class ConditionalObject  {

		/// <summary>
		/// The conditions.
		/// </summary>

		[SerializeField] protected string _conditions = "";


		public void Init() {

			RegisterEvents ();
		
		}

		void RegisterEvents() {
			// On construction subscribe to game controller's set var event
			GameController.GameVariableController.SetVariable += HandleSetVariable;
			
		}

		
		protected virtual void HandleSetVariable (object sender, VariableEventArgs e)
		{
			// Update when variable changes
			Update ();
		}
	

		/// <summary>
		/// Check to see if the conditions are met
		/// </summary>
		/// <returns><c>true</c>, if conditions met was ared, <c>false</c> otherwise.</returns>

		protected virtual bool AreConditionsMet() {return Scripting.AreConditionsMet (_conditions);}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public abstract void Update(); 
	}

}
