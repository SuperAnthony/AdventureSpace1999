// TODO: I'm not sure this is ever used

#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 15, 2015
#endregion 


#region using
using System;
using UnityEngine;
using System.Collections;

using WhatPumpkin.Entities;
#endregion


namespace WhatPumpkin {

	#region required components
	[RequireComponent(typeof(Animator))]
	#endregion

	public class AnimatingEntity : Entity, IPerformParams<string> {

		#region events

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>

		public event EventHandler FinishedPlaying;

		#endregion

		#region fields
	
		/// <summary>
		/// The animator component that should be attached to this object.
		/// </summary>

		Animator _animator;

		/// <summary>
		/// Has this animating entity started playing this animation
		/// </summary>

//		bool startedPlaying = false;

		#endregion

		#region methods

		void Start() {
		
			// Get the animator component
			_animator = MonoBehaviorUtils.GetForcedComponent<Animator> (this.gameObject);

		}

		/// <summary>
		/// Determines whether this instance has completed playback of it's animation.
		/// </summary>
		/// <returns><c>true</c> if this instance has completed playback; otherwise, <c>false</c>.</returns>

		public bool HasCompletedPlayback() {
		
			// TODO (how do I determine what layer is being played?)
			float length = _animator.GetCurrentAnimatorStateInfo (0).length;
			float currentTime = _animator.GetCurrentAnimatorStateInfo (0).normalizedTime;

			Debug.Log ("Length: " + length + " Current Time: " + currentTime); 

			if (currentTime >= length) {
				return true;
			}

			return false;


		}

		/// <summary>
		/// Raises the finish playing event.
		/// </summary>

		public void OnFinishPlaying() {

			// Invoke the finished playing method
			FinishedPlaying.Invoke (this, null);

			// Unregister the on update method
			EventManager.OnUpdate -= HandleOnUpdate;
		}

		public void Play(string [] parameters) {
		
			string animationState = "";

			if(parameters.Length > 0) {

				animationState = parameters[0].ToString(); 
			
			}

			if(animationState != null && animationState != "" && _animator != null) {

				_animator.Play(animationState);
				//startedPlaying = true;

				// Register to the event manager's on update method to check each frame if the animation that started playing has finished playing
				EventManager.OnUpdate += HandleOnUpdate;
			}
			else {

				// Animation was not found, finish playing and warn the developer
				Debug.LogWarning("The animation state'" + animationState + "' was not found on the object '" + this.Key + "' playback complete.");
				OnFinishPlaying();
			}


		}


		/// <summary>
		/// Handles update.
		/// </summary>

		void HandleOnUpdate ()
		{
			if (HasCompletedPlayback()) {
				OnFinishPlaying();
			}

			
		}

		#endregion

	
	}
}
