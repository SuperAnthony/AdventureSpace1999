#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 12, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.UI.FX {

	[RequireComponent(typeof(Button))]

	public class HideButtonOnClick : MonoBehaviour, IHideable {

		#region fields

		Button _button;

		#endregion

		#region methods

		// Use this for initialization
		void Start () {

			_button = this.GetComponent<Button> ();

			if (_button != null) {_button.onClick.AddListener(Hide);}

		}


		/// <summary>
		/// Hide this instance.
		/// </summary>

		public void Hide() {


			// Check to see if this object is enabled 
			if (this.enabled) {
								// Hide button Image
								_button.image.enabled = false;

								// Hide Button Text
								foreach (Transform t in this.transform) {
			
										Text text = t.GetComponent<Text> ();

										if (text != null) {
												text.enabled = false;
										}
			
								}
						}

		}

		/// <summary>
		/// Show this instance.
		/// </summary>

		public void Show () {

			// Check to see if this object is enabled - I'd have thought this went without saying
			/*
			if (this.enabled) {

								// Hide button Image
								_button.image.enabled = true;
			
								// Hide Button Text
								foreach (Transform t in this.transform) {
				
										Text text = t.GetComponent<Text> ();
				
										if (text != null) {
												text.enabled = true;
										}
				
								}
						}*/

		
		}

		#endregion
		
	}
}
