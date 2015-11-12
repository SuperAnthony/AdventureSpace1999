#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 6, 2014
#endregion 

#region summary
/// <summary>
/// Follow - Script used for following other characters or moveable objects around
/// </summary>
#endregion

using UnityEngine;
using System.Collections;

namespace WhatPumpkin.HiveSwap {

	public class AIFollow : MonoBehaviour {


		[SerializeField] GameObject _iMoveableTarget; // The chracter that this is following | This must be a moveable object
		IMoveable _followingTarget; // The object that is being followed will be equal to the iMoveableTarget if the game object has a component that implements iMoveable
		[SerializeField] float _followSpeed; // The speed this character will follow others at
		bool _isFollowing = false; // Used to track whether or not this object is following something
		bool _isTooClose = false; // Is the object that the player is following too close to continue moving
		
		AIPath aiPath; // Stores the aiPath component that is required to be attached

		
		// Use this for initialization
		void Start () {

			// Set the following target as an Imoveable 
			if(_iMoveableTarget != null) {
				SetFollowingTarget (_iMoveableTarget.GetComponent (typeof(IMoveable)) as IMoveable);
			}
			// Get the AIPath 
			aiPath = GetComponent<AIPath>();
			
			// If there is a character to follow, set the aiPath's target to the following characters transform
			if (_followingTarget != null) {
				aiPath.target = _followingTarget.transform;
			}
			
		}

		public void SetFollowingTarget(IMoveable moveable) {
			if(moveable != null) {
				_followingTarget = moveable;
			}
		}

		void Follow() {
			// Enable the aiPath Script again to follow
			aiPath.enabled = true;
			// Set Following to true
			_isFollowing = true;
		}
		
		void Follow(float spd) {
			// Enable following again
			Follow ();
			// Re-Set the speed
			aiPath.speed = spd;
		}
		
		void StopFollowing() {
			// Stop following by disabling the aiPath script
			aiPath.enabled = false;
			// Set following to false
			_isFollowing = false;
		}
		
		// Update is called once per frame
		void Update () {

			// Make sure the following target is not a null reference
			if (_followingTarget != null) {
				
				// If the object not following but the target is moving
				if (!_isFollowing && _followingTarget.IsMoving () && !_isTooClose) {
					// Then follow the object
					Follow();
				}
				// Otherwise, if the object is following but the target has stopped moving
				else if ((_isFollowing && !_followingTarget.IsMoving ()) || _isTooClose) {
					// Then stop following
					StopFollowing();
				}
			}
			
		}

		void OnTriggerEnter(Collider col) {
			// If the target has entered the trigger then the following target is too close to be followed
			//Debug.Log (col.name);

			if(col.transform == _followingTarget.transform) {
				_isTooClose = true;
				// Stop following if it is too close to be followed
				StopFollowing();
			}
		}

		void OnTriggerExit(Collider col) {
			// If the target has exited the trigger then the target is no longer too close
			if(col.transform == _followingTarget.transform) {
				_isTooClose = false;
			}
		}
	}
}
