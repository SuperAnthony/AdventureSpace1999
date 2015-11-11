// May 18, 2015

using UnityEngine;
using System.Collections;

namespace WhatPumpkin {

	/// <summary>
	/// Quick script to play a specified animation state on a character.
	/// </summary>

	//[RequireComponent(typeof(Animator))]

	public class PlayAnimationState : MonoBehaviour {

		/// <summary>
		/// Animation state to be played.
		/// </summary>

		[SerializeField] string _animationState;
	
		/// <summary>
		/// Play this animation on start?
		/// </summary>

		[SerializeField] bool _playOnStart;

		Animator _animator;

		// Use this for initialization
		void Start () {

			_animator = MonoBehaviorUtils.GetForcedComponent<Animator> (this.gameObject);

			if (_playOnStart) {
				Play ();
			}
		
		}

		/// <summary>
		/// Play this instance.
		/// </summary>

		public void Play() {

			if (_animator != null) { _animator.Play (_animationState);}
		}
		
	}
}