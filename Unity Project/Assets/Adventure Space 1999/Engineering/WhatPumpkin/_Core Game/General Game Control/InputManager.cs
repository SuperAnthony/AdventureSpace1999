#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 2, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;

using WhatPumpkin.Sgrid.Markers;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Controls for cursors.
	/// </summary>

	public class InputManager : MonoBehaviour, ICursorControl, IInputManager {

		#region constants and static fields

		/// <summary>
		/// The game object name of the target that gets moved around
		/// </summary>
		
		const string TARGET_NAME = "Target";

		/// <summary>
		/// The instance of this cursor control.
		/// </summary>

		static InputManager _instance;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>

		static public InputManager Instance { get { return _instance; } }

		#endregion


		#region fields

		public const int DEFAULT_CURSOR = 0;
		public const int DEFAULT_ROLLOVER_CURSOR = 1;
		public const int TARGET_CURSOR = 2;
		public const int TARGET_ROLLOVER_CURSOR = 3;

		public event EventHandler<TargetEventArgs> TargetChangeEvent;

		// TODO: Collapse these events to one event handler

		[SerializeField] Transform _target;

		// TODO: Create a mouse event type that handles all of this and deprecate these actions

		/// <summary>
		/// Occurs when left mouse click.
		/// </summary>

		public event Action LeftMouseClick;

		/// <summary>
		/// Occurs when right mouse click.
		/// </summary>

		public event Action RightMouseClick;


		/// <summary>
		/// The available cursors.
		/// </summary>
		
		[SerializeField] WhatPumpkin.Cursor [] _cursors;


		/// <summary>
		/// The _default cursor.
		/// </summary>

		[SerializeField] int _defaultCursor = 0;

		/// <summary>
		/// The _default mouse over cursor.
		/// </summary>

		[SerializeField] int _defaultRollOverCursor = 1;

		/// <summary>
		/// The time between frames.
		/// </summary>

		[SerializeField] float _timeBetweenFrames = .5F;

		/// <summary>
		/// The _time since frame change.
		/// </summary>

		float _timeSinceFrameChange = 0F;

		Cursor _currentCursor = null;

		#endregion

		#region properties

		public Transform Target { 
			get {

				if(_target != null) {
					return _target;
				}
				else {
					_target = GameObject.Find(TARGET_NAME).transform;
					return _target;
				
				}
			} 
		}

		#endregion

		#region methods

		void Start() {

			WhatPumpkin.Sgrid.Markers.Target.TargetActivated += HandleTargetActivated;

		}

		void Update() {
		
			if (Input.GetMouseButtonDown (0)) {
			
				// If the left mouse button was down then raise the left mosue button down event
				if(LeftMouseClick != null) {LeftMouseClick();}
			
			}

			if (Input.GetMouseButtonDown (1)) {
				
				// If the right mouse button was down then raise the right mosue button down event
				if(RightMouseClick != null) {RightMouseClick();}
				
			}





		}

		/// <summary>
		/// Updates the cursor animation.
		/// </summary>

		void UpdateCursorAnimation() {

			if (_timeSinceFrameChange >= _timeBetweenFrames && _currentCursor != null && _currentCursor.IsAnimated) {
				
				
				_timeSinceFrameChange = 0F;
				_currentCursor.GoToNextFrame();
				SetCursor(_currentCursor);
			}
			
			_timeSinceFrameChange += Time.deltaTime;
				
		}

		void Awake() {
			_instance = this;

			// Initialize the default cursor
			UseDefaultCursor ();
		}

		/// <summary>
		/// Sets the cursor using the cursors collection the controller.
		/// </summary>
		/// <param name="index">Index.</param>

		public void SetCursor(int index) {

			//Debug.Log ("Set Cursor: " + _cursors[index].Key);

			// Check to see that the cursor exists
			if(index < _cursors.Length) {

				// Set the cursor
				UnityEngine.Cursor.SetCursor(_cursors[index].Icon, _cursors[index].Offset, CursorMode.Auto);

				// Set the current cursor
				_currentCursor = _cursors[index];
			} 
			else {
				Debug.LogWarning("Could not find cursor texture");
			}
		}

		/// <summary>
		/// Sets the cursor using the cursors collection the controller.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="key">Key.</param>

		public void SetCursor(string key) {

			if (key == _currentCursor.Key) {
				return;			
			}
		
			Cursor cursor = Keyed.FindInCollection<Cursor> (key, _cursors);
		
			if (cursor != null) {
				SetCursor (cursor);
			}
			else {
			
				Debug.LogWarning("Cursor '" + key + "' not found."); 

			}

		}

		
		/// <summary>
		/// Sets the cursor.
		/// </summary>
		/// <param name="cursorIcon">Cursor icon.</param>
		
		public void SetCursor(Cursor cursor) {

			//Debug.Log ("Set Cursor: " + cursor.Key);

			// If this is already the cursor then return
			//if(_currentCursor == cursor){return;}

			// Set the cursor
			UnityEngine.Cursor.SetCursor(cursor.Icon, cursor.Offset, CursorMode.Auto);
			_currentCursor = cursor;	
		}

		/// <summary>
		/// Changes the default cursor.
		/// </summary>
		/// <param name="defaultCursor">Default cursor.</param>
		/// <param name="defaultRolloverCursor">Default rollover cursor.</param>

		public void ChangeDefaultCursor (int defaultCursor, int defaultRolloverCursor) {
		
			_defaultCursor = defaultCursor;
			_defaultRollOverCursor = defaultRolloverCursor;

		}
		
		/// <summary>
		/// Uses the default cursor.
		/// </summary>
		
		public void UseDefaultCursor() {

			//Debug.Log ("Use Default Cursor");

			UnityEngine.Cursor.SetCursor(_cursors[_defaultCursor].Icon, _cursors[_defaultCursor].Offset, CursorMode.Auto);
			_currentCursor = _cursors [_defaultCursor];
		}

		public void HandleRollover(Cursor defaultRolloverCursorOverride = null) {

			// Are we overriding the default mouse rollover cursor
			if(defaultRolloverCursorOverride.Icon != null) {


				GameController.CursorControl.SetCursor(defaultRolloverCursorOverride);

				}
				else 
				{
		
					GameController.CursorControl.SetCursor (_defaultRollOverCursor);
				}
				
		
		}

		public void HandleRollout() {
		
			UseDefaultCursor ();

		}


		void HandleTargetActivated (object sender, TargetActivatedEventArgs e)
		{
			
			TargetMover.Instance.SetTargetPosition (e.TransformInfo.position);
			WhatPumpkin.Sgrid.Markers.Target tar = this.Target.GetComponent<WhatPumpkin.Sgrid.Markers.Target> ();
			tar.AffectCharacterRotation = e.AffectCharacterRoation;
			

		}


		/// <summary>
		/// Receives a target that's moved.
		/// </summary>
		/// <param name="target">Target.</param>

		public void ReceiveTargetEvent(GameObject target) {

			// I think I'm over complicating this here		
			if(TargetChangeEvent != null) {
				TargetChangeEvent(this, new TargetEventArgs(target, TargetEventType.Moved));
			}



		}

		#endregion


	}
}
