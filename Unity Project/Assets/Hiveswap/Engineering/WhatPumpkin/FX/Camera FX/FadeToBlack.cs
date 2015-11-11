#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - October 16, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.FX {

	public class FadeToBlack : FadeFX  {

		#region singleton

		/// <summary>
		/// Singleton instance of this event
		/// </summary>

		static FadeToBlack _instance;

		/// <summary>
		/// Gets the singleton instance of fade to black for easy access.
		/// </summary>
		/// <value>The instance.</value>

		public static FadeToBlack Instance { get { return _instance; } }

		#endregion

		#region fields

		/// <summary>
		/// The speed variant.
		/// </summary>

		[SerializeField] float _speed = .5F;

		/// <summary>
		/// The image.
		/// </summary>

		[SerializeField] Image _image; 

		/// <summary>
		/// The _direction. Goes in reverse when negative 1.
		/// </summary>

		int _direction = 1;

		#endregion

		#region methods

		// Initialize
		void Start () {

			// Deactivate the overlay on start
			if (_image != null) {_image.gameObject.SetActive(false);}

			_elapesedTime = 0F;

			base.Start ();

			_instance = this;

		}

		void Update() {


			if (_isActive)
			{
				// Progress the fade along curve
				_elapesedTime += (Time.smoothDeltaTime * _direction * _speed);
				var timeOnCurve = _fadeCurve.Evaluate(_elapesedTime);
			
				// Change value 
				ChangeAlphaValue(timeOnCurve);

				// Is it time to deactivate the effect?
				if(_direction == 1 && _elapesedTime > _duration) {
					Deactivate();
				}
				else if(_direction == -1 && _elapesedTime < 0F) {
					// Deactivate the overlay
					if (_image != null) {_image.gameObject.SetActive(false);}
					Deactivate();
				}
					
				
			}


		}

		/// <summary>
		/// Activates the effect.
		/// </summary>
		public override void Activate()
		{
			// Activate the overlay
			if (_image != null) {_image.gameObject.SetActive(true);}
			// Set to active state
			_isActive = true;
		}

		/// <summary>
		/// Deactivate the effect.
		/// </summary>
		public override void Deactivate()
		{

			// Will go in opposite direction the next time it is activated
			ReverseDirection();

			// No longer active
			_isActive = false;

			// Raise the finished playing event
			this.FinishPlaying ();

		}

		/// <summary>
		/// Reverses the direction for the next fade.
		/// </summary>

		protected void ReverseDirection() {

			_direction = _direction * -1;

		}

		/// <summary>
		/// Use this to implement making the fading item completely transparent.
		/// </summary>

		protected override void MakeInvisible() {
		
			_image.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0f);

		
		
		}

		/// <summary>
		/// Use this on a loop to implement fading.
		/// </summary>
		/// <param name="alphaValue"></param>

		protected override void ChangeAlphaValue(float alphaValue) {
		
			_image.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, alphaValue);
		
		}

		#endregion

	}
}