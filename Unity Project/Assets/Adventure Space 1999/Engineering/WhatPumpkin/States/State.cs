#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 16, 2015
#endregion

#region using
using System;
using System.Collections.Generic;
using UnityEngine;
using WhatPumpkin.Entities;
using WhatPumpkin.Sound;
using WhatPumpkin.Actions.Sequences;

using WhatPumpkin.Sgrid.Environment; // TODO: Remove this from this namespace asap
#endregion

namespace WhatPumpkin.States {

	/// <summary>
	/// The state of a scene entity such as a character. These can be placed in StateTypes (which are groups of states) one of which can be active. For instance, an instance of a state can be Nervous and this can be part of a state type called Emotion. Animations, actions and sounds can also be associated with a state (TODO).
	/// </summary>

	[System.Serializable]

	public class State : Keyed, ISwitchable   {

		//enum ValueType {FLOAT, INT};

		#region fields



		/// <summary>
		/// The animation entry point that gets used when this state gets activated.
		/// </summary>

		[SerializeField] string _animationEntryPoint;


		/// <summary>
		/// The float value.
		/// </summary>

		//[SerializeField] float _floatValue;

		/// <summary>
		/// The int value.
		/// </summary>

		[SerializeField] int _intValue;

		/// <summary>
		/// An action sequence to play whenever this state is activated.
		/// </summary>

		[SerializeField] ActionSequence _actionSequence;

		/// <summary>
		/// Objects associated with the character for this state
		/// They deactivate when this state is deactivated
		/// They attempt to activate when this state is activated
		/// </summary>

		[SerializeField] TimeOfDayCondition [] _conditionalObjects;

	
		/// <summary>
		/// The looping audio.
		/// </summary>

//		ISound _loopingAudio;

		/// <summary>
		/// Is this state active
		/// </summary>

		bool _isActive = false;
		

		#endregion
		
		#region properties

		/// <summary>
		/// Gets the int value.
		/// </summary>
		/// <value>The int value.</value>

		public int IntValue { get { return _intValue; } }

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get {return _key; } }

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>

		public bool IsActive { get { return _isActive; } }

		#endregion

		#region methods


		/// <summary>
		/// Stop this instance
		/// </summary>

		public void Stop() {
		
			StopSound ();

		}

		/// <summary>
		/// Plays the sound.
		/// </summary>

		public void PlaySound() {
		/*
			if(_loopingAudio != null) {
				_loopingAudio.Play ();
			}*/

		}

		/// <summary>
		/// Stops the sound.
		/// </summary>

		public void StopSound() {
		/*
			if(_loopingAudio != null) {
				_loopingAudio.Stop ();
			}*/

		}

		/// <summary>
		/// Activate this state. Apply animation to the animator passed
		/// </summary>
		/// <param name="animator">Animator.</param>

		public void Activate(Animator animator) {
		
			// Play animations associated with this state
			if(animator != null && _animationEntryPoint != null && _animationEntryPoint != "") {
					try{
						animator.Play(_animationEntryPoint);			
					}
					catch {
					
						Debug.LogError("Could not play the animation '" + _animationEntryPoint + "'. Perhaps this animation is not set up in the animation contoller for the '" + animator.name + "' character.");
					}
			}

			// Plan an action sequence if there is one
			if (_actionSequence != null) {_actionSequence.Play();}

			// Complete activation
			Activate ();

		
		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public void Activate() {


			_isActive = true;

			// Go through the conditional objects and attempt to activate them
			foreach (TimeOfDayCondition item in _conditionalObjects) {
				// Update. I don't need to explicitly enable here becasue update will take care of that if it's necessary
				item.Update();
			}

		
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public void Deactivate() {
			_isActive = false;
			foreach (TimeOfDayCondition item in _conditionalObjects) {
				// Disable all objects associated with this state
				item.Disable();
			}
		}




		#endregion

		#if UNITY_EDITOR

		public void SetProperties(string key) {
		
			_key = key;
		
		}
	
		#endif

	}
}