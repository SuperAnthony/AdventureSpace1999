using UnityEngine;
using System.Collections;

using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Screens;

namespace WhatPumpkin {

	public class PerformActionOnScreenEvent : MonoBehaviour {

		/// <summary>
		/// The collection of screen keys that could determine whether or not this action should be performed.
		/// </summary>

		[SerializeField] string [] _screenKeys;

		/// <summary>
		/// The type of the screen event that triggers the action sequence.
		/// </summary>

		[SerializeField] ScreenEventType _screenEventType = ScreenEventType.Close;

		/// <summary>
		/// The action sequence that is performed.
		/// </summary>

		[SerializeField] ActionSequence _actionSequence;

		// Use this for initialization
		void Start () {
			GameScreen.ScreenEvent += HandleScreenEvent;
		}

		void HandleScreenEvent (object sender, GameScreenEventArgs e)
		{

			// Are the conditions met to activate the action sequence
			if (_screenEventType == e.ScreenEvent && HasKeyInList (e.Key)) {
				if(_actionSequence != null) {_actionSequence.Play();}
			}
		}

		/// <summary>
		/// Determines whether this instance has key in list the specified key.
		/// </summary>
		/// <returns><c>true</c> if this instance has key in list the specified key; otherwise, <c>false</c>.</returns>
		/// <param name="key">Key.</param>

		bool HasKeyInList(string key) {
			foreach (string item in _screenKeys) {
				if(key == item) {
					return true;
				}
			}

			return false;
		}
		
		void OnDestroy() {
		

			GameScreen.ScreenEvent -= HandleScreenEvent;


		}
	}
}