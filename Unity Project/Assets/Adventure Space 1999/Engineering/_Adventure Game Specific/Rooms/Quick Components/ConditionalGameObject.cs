#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 4, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;

using WhatPumpkin.ScriptingLanguage;
#endregion

// TODO: Move to different folder in Project

namespace WhatPumpkin.Sgrid {

	/// <summary>
	/// For game objects that are active or inactive based on setting certain critera that must be met when activation is attempted
	/// </summary>

	[System.Serializable]

	public class ConditionalGameObject : ConditionalObject, IEnable {

		/// <summary>
		/// The game object that will be activated or not activated provided the conditions are met.
		/// </summary>

		[SerializeField] GameObject _gameObject;


		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.Environments.ConditionalGameObject"/> class.
		/// </summary>

		public ConditionalGameObject() {


		}


		/// <summary>
		/// Update this instance. Check to see if conditions are met, is so then activate, if not then deactivate
		/// </summary>

		public override void Update ()
		{
//			Debug.Log ("Update Conditional Game Object");
			if (AreConditionsMet ()) {
			
				Enable();
			
			}
			else {
			
				Disable();

			}
		}

		/// <summary>
		/// Enable this instance.
		/// </summary>

		public virtual void Enable() {
		
			if (_gameObject != null) {
								_gameObject.SetActive (true);
						}

		}

		/// <summary>
		/// Disable this instance.
		/// </summary>

		public virtual void Disable() {

			if (_gameObject != null) {
								_gameObject.SetActive (false);
						}
		
		}



	}
}