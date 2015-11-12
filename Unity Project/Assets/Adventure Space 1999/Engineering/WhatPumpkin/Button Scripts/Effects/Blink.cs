#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 14, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.FX {

	/// <summary>
	/// Blink.
	/// </summary>

	//[RequireComponent(typeof

	public class Blink : Effect, ISwitchable {

		#region using

		/// <summary>
		/// The color to lerp to
		/// </summary>

		[SerializeField] Color _toColor = new Color(1F,1F,1F,1F);

		Color _targetColor;

		/// <summary>
		/// The color to lerp from
		/// </summary>

		Color _fromColor;

		/// <summary>
		/// The speed.
		/// </summary>

		[SerializeField] float _speed = 1F;

		float _progress = 0F;

		/// <summary>
		/// The animation curve.
		/// </summary>

		[SerializeField] AnimationCurve _animationCurve;

		/// <summary>
		/// The image component attached.
		/// </summary>

		Image _image;

		bool _lerpingToColor = true;

		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>

		public bool IsActive { get { return _isActive; } }

		#endregion

		#region methods

		/// <summary>
		/// Occurs on Start
		/// </summary>

		void Start() {
		
			_image = this.GetComponent<Image> ();

			_fromColor = _image.color;

			_targetColor = _toColor;

		
		}



		// Update is called once per frame
		void Update () {



			//Debug.Log ("Hint Receiver Progress Value: " + _animationCurve.Evaluate(_progress).ToString());
		
			if (_isActive) {

				_progress += _speed * Time.deltaTime;

				if(_progress > 1 ) {

					_progress = 0;

					if(_lerpingToColor == true) {
						_targetColor = _fromColor;
					}
					else {
					
						_targetColor = _toColor;


					}

					_lerpingToColor = !_lerpingToColor;
				
				}
			
		
				LerpToColor(_animationCurve.Evaluate( _progress) );



			}


		}

		/// <summary>
		/// Lerps to the to color
		/// </summary>

		void LerpToColor(float value) {
		
			if (_image != null) {
			
				//_image.color = _toColor;

				_image.color = Color.Lerp(_image.color, _targetColor, value);

			}
		


		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {
		
			// Activate
			_isActive = true;

		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {
		
			// Deactivate
			_isActive = false;

			// Return to the from color
			_image.color = _fromColor;

		}

		#endregion

	}
}