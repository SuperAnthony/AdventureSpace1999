#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 13, 2015
#endregion 


#region using
using System;
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Screens {

	/// <summary>
	/// Bark. Barks open a MessageScreen, controlling that message location and duration
	/// </summary>

	public class Bark : GameScreen, IBark {

		/// <summary>
		/// Occurs when bark end.
		/// </summary>

		public event EventHandler BarkEnd; 

		#region fields

		/// <summary>
		/// The message screen that gets displayed.
		/// </summary>

		[SerializeField] MessageScreen _messageScreen;

		/// <summary>
		/// The duration of time the bark gets displayed for before getting destroyed.
		/// </summary>

		[SerializeField] float _duration = 3F;

		/// <summary>
		/// The _counter.
		/// </summary>

		float _counter = 0F;

		/// <summary>
		/// The is active.
		/// </summary>

		bool _isActive = false;

		#endregion

		#region methods

		/// <summary>
		/// Update this instance.
		/// </summary>

		void Update() {
		
			if (_isActive) {
			
				_counter += Time.deltaTime;
				if(_counter >= _duration) {
					// Destroy this because we no longer want it

					BroadcastBarkEnd();

					GameObject.Destroy(this.gameObject);

				}
			}
		}


		/// <summary>
		/// Broadcasts the bark end.
		/// </summary>

		void BroadcastBarkEnd() {
				
			if (BarkEnd != null) {
				BarkEnd.Invoke(this, null);
			}
		
		}

		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="pos">Position.</param>
		/// <param name="duration">Duration.</param>

		public void SetProperties(Vector3 pos, float duration) {
		
			Debug.Log ("Set Message Screen Properties pos: " + pos);

			//this.transform.position = pos;
			//_messageScreen.transform.position = new Vector3 (pos.x, pos.y, 0);
			RectTransform _rectTransform = _messageScreen.GetComponent<RectTransform> ();
			_rectTransform.anchoredPosition = new Vector2 (pos.x * .5F, pos.y * .5F);
			_duration = duration;
		
		}

		/// <summary>
		/// Open a message screen and specifies a message.
		/// Activates this instance
		/// </summary>
		/// <param name="message">Message.</param>

		public void Open(string message, bool isGraphicMessage = false) {
		
			_isActive = true;
			_messageScreen.Open (message);

		}

		#endregion

	}
}
