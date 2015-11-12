#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 30, 2015
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.States;
#endregion

namespace WhatPumpkin.States {

	/// <summary>
	/// State group primarily used for tracking the behavior of characters.
	/// </summary>

	public class StateType : MonoBehaviour {

		#region fields

		/// <summary>
		/// The active state in this group of states.
		/// </summary>

		State _active;

		/// <summary>
		/// The type of state this refers to (examples: Emotion, Active Prop, Health, etc.).
		/// </summary>

		[SerializeField] string _type;

		/// <summary>
		/// The controller parameter.
		/// </summary>

		[SerializeField] string _controllerParameter;

		/// <summary>
		/// The _states.
		/// </summary>

		[SerializeField] List<State> _states = new List<State>();

		/// <summary>
		/// Activate this state on start.
		/// </summary>

		[SerializeField] string _activateStateOnStart = "";

		/// <summary>
		/// The _animator component if there is one attached.
		/// </summary>

		Animator _animator;

		/// <summary>
		/// The character.
		/// </summary>

		WhatPumpkin.Sgrid.Characters.Character _character;

		#endregion

		#region properties

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>

		public string Type { get { return _type; } }

		/// <summary>
		/// Gets the active state.
		/// </summary>
		/// <value>The active.</value>

		public State Active{ get { return _active; } }
		


		#endregion

		#region methods

		/// <summary>
		/// Occurs on Start.
		/// </summary>

		void Start() {
		
			// Get the character component
			_animator = this.GetComponent<Animator> ();
			_character = this.GetComponent<WhatPumpkin.Sgrid.Characters.Character> ();

			if (_activateStateOnStart != null && _activateStateOnStart != "") {
			

				ChangeState(_activateStateOnStart);
			
			}
		
		}

		/// <summary>
		/// Changes the state.
		/// </summary>
		/// <param name="state">State.</param>

		void ChangeState(State state) {

			//Debug.Log ("Change State");

			// Deactivate the active state
			if(_active != null){_active.Deactivate ();}

			// Activate this state
			_active = state;
			_active.Activate (_animator);

			// Set the state
			if (_character != null) {
				_character.AnimatedCharacter.SetInteger(_controllerParameter, state.IntValue);
			}


		}

		public void ChangeState(string key) {

			foreach (State state in _states) {
			
				if(state.Key == key) {
				
					// Key found, change the state
					ChangeState(state);
					return;
				}
			}


			// Let the developer know that the key was not found
			Debug.Log ("The state '" + key + "' was not found on the object '" + this.gameObject.name + "'");


		}

		#endregion

	}
}
