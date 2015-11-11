#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 25, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.FX {

	/// <summary>
	/// Mosaic Camera Effect.
	/// </summary>

	public class MosaicFX : Effect {

		[SerializeField] Camera _camera;


		[SerializeField] float _speed = .1F;

		int originalWidth = 0;
		int originalHeight = 0;

		int width = 0; // The current width
		int height = 0; // The current height


		int wSpeed = -12;
		int hSpeed = -9;

		[SerializeField] bool _hideOnOut;

		[SerializeField] RawImage _overlayImage;


		public override void Play (string[] parameters) {

			wSpeed = Mathf.FloorToInt (_speed * Screen.width) * -1;
			hSpeed = Mathf.FloorToInt (_speed * Screen.height) * -1;

			if (parameters.Length > 0 && parameters [0] == "OUT") {



				_isActive = true;
				wSpeed = Mathf.Abs(wSpeed);
				hSpeed = Mathf.Abs(hSpeed);

				Debug.Log ("OUT: " + wSpeed);
			}
	

			Play ();
			
		}


		// Use this for initialization
		void Start () {
		

			width = Screen.width;
			height = Screen.height;

			originalWidth = width;
			originalHeight = height;

			_overlayImage.enabled = false;


		}

		public override void Activate() {

			// Locate the main camera
			_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();

		
			_isActive = true;
			_overlayImage.enabled = true;


		}

		public override void Deactivate() {
		
			_isActive = false;
			_camera.targetTexture = null;
		
		}

		void Hide() {
		
			_overlayImage.enabled = false;

		}


		RenderTexture renderTexture;

		// Update is called once per frame
		void Update () {

			if (_isActive) {

				Debug.Log("width: " + width);

								if (_camera.targetTexture != null) {
										_camera.targetTexture.Release ();
								}


								width += wSpeed;
								height += hSpeed;
			
								if (height < originalHeight * .1) {
										height = Mathf.FloorToInt(originalHeight * .1F);
								}
			
			
								if (width < Mathf.FloorToInt(originalWidth * .1F)) {
										width = Mathf.FloorToInt(originalWidth * .1F);
										FinishPlaying();
										//Deactivate();
								}

								renderTexture = new RenderTexture (width, height, 24);

								_camera.targetTexture = renderTexture;

								_overlayImage.texture = renderTexture;

								

						}



		}
	}
}
