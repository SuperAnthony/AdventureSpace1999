#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 28, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	[System.Serializable]

	public class SceneObject : ISceneObject<string> {

		
		/// <summary>
		/// Scene objects are not "Keyed" they do however reference a key to pass values into the keyed object (presumably Entities) later.
		/// </summary>
		
		[SerializeField] string _keyReference;

		/// <summary>
		/// The _is active.
		/// </summary>

		[SerializeField] bool _isActive = true;

		/// <summary>
		/// The state of the animation if this object has an animation component attached.
		/// </summary>
		
		[SerializeField]  string _animationState;

		/// <summary>
		/// Gets the key that this object is referencing.
		/// </summary>
		/// <value>The key.</value>
		
		public string Key { get { return _keyReference; } }
		
		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.SceneObject`1"/> is Active.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		
		public bool IsActive { get { return _isActive; } }
		
		/// <summary>
		/// Gets the state of the animation.
		/// </summary>
		/// <value>The state of the animation.</value>
		
		public string AnimationState { get { return _animationState; } }

		/// <summary>
		/// Receives the data.
		/// </summary>
		/// <param name="item">Item.</param>

		public void ReceiveData (ISceneObject<string> item) {


			_keyReference = item.Key;

			_isActive = item.IsActive;

			//_animationState = item.AnimationState;
			
		}

	}
}
