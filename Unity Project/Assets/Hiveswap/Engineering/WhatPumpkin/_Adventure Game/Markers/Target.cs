#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 21, 2015
#endregion

#region using
using System;
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Sgrid.Markers {

	/// <summary>
	/// Target for the active character.
	/// </summary>

	public class Target : Marker, IPerform {

		#region static members

		static public event EventHandler<TargetActivatedEventArgs> TargetActivated;

		#endregion

		#region fields

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>
		
		public event EventHandler FinishedPlaying;

		/// <summary>
		/// The pathing character.
		/// </summary>

		AIPath _pathingCharacter;

		/// <summary>
		/// Is this target playing - as in, is the player moving to the target
		/// </summary>

		bool _isPlaying = false;

		#endregion

		#region proprties

		/// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>
		
		public int PlayOrder { get; set;}

	
		#endregion


		#region methods

		/// <summary>
		/// Raises the draw gizmos event.
		/// </summary>

		[ExecuteInEditMode]
		void OnDrawGizmos() {
			Gizmos.DrawIcon(this.transform.position, "MoveTargetIcon.png", true);
		}

		bool _startedPlaying = false;

		public void Play() {
			_previousFrameDistance = 100F;

			_isPlaying = true;
			_startedPlaying = false;

			if (TargetActivated != null) {
				TargetActivated.Invoke(this , new TargetActivatedEventArgs(this.transform, this.AffectCharacterRotation));
			}

			// Get the active pc
			_pathingCharacter = GameController.PartyManager.ActivePC.GetComponent<AIPath>();

			

		}

		float _previousFrameDistance = 100F;

		void Update() {


			// If playing then check to see if the character has reached it's target
			if (_isPlaying && _startedPlaying == false) {


				if(_pathingCharacter.TargetReached 	== false) {
					_startedPlaying = true;
				}
			

			}
			else if (_isPlaying && _pathingCharacter.TargetReached) {

				Stop ();
			}


			// If the distance hasn't change and it's a relatively small distance then stop
			if(_isPlaying) {
				float _currentDistance = Vector3.Distance (this.transform.position, _pathingCharacter.transform.position);
				if (_previousFrameDistance == _currentDistance && _currentDistance <= .5F) {
					Stop();		
				}
				_previousFrameDistance = _currentDistance;
			}

		
		}


		public void Stop() {
		
			_isPlaying = false;
			_startedPlaying = false;

			// The target has finished playing
			if (FinishedPlaying != null) {FinishedPlaying.Invoke(this, null);}

			if (_affectCharacterRotation) {
				_pathingCharacter.transform.rotation = this.transform.rotation;
			}

		}



		#endregion


	}

	/// <summary>
	/// Target activated event arguments.
	/// </summary>

	public class TargetActivatedEventArgs : EventArgs {
	
		/// <summary>
		/// Gets the transform info.
		/// </summary>
		/// <value>The transform info.</value>

		public Transform TransformInfo { get; private set; } 
		public bool AffectCharacterRoation { get ; private set; }

		public TargetActivatedEventArgs(Transform transform, bool affectCharacterRotation) {
		
			TransformInfo = transform;
			AffectCharacterRoation = affectCharacterRotation;

		}
	}
}
