using System;
using UnityEngine;
using System.Collections;

namespace WhatPumpkin.FX {

	// TODO: Not sure if I use this

	public class CameraTransitions  {

		/// <summary>
		/// Occurs when fade to black complete.
		/// </summary>

		static public event Action fadeToBlackComplete;

		/// <summary>
		/// The _fade.
		/// </summary>

		//static float _fade = 100;

		/// <summary>
		/// The _is fading to black.
		/// </summary>

		//static bool _isFadingToBlack = false; 
	

		/// <summary>
		/// Starts the fade to black.
		/// </summary>

		static public void StartFadeToBlack(int fadeReturnSeconds = 0) {
		
			ProtoFadeScript protoFadeScript = GameObject.FindObjectOfType<ProtoFadeScript> ();

			protoFadeScript.StartTransition (false);



		}

		static public void StartFadeFromBlack() {
		
			ProtoFadeScript protoFadeScript = GameObject.FindObjectOfType<ProtoFadeScript> ();
			
			protoFadeScript.StartTransition (true);


		}


		/// <summary>
		/// Raises the fade to black complete event.
		/// </summary>

		static public void OnFadeToBlackComplete() {

			//Debug.Log ("On Fade To Black Complete");


			if(fadeToBlackComplete != null) {
				fadeToBlackComplete.Invoke ();
			}

//			_isFadingToBlack = false;

			// TODO: Add Code here (TODO: Unregister this event) (Or really this should only register when the fade to balck starts... what is happening?)
			//ProtoFadeScript.fadedToBlack -= OnFadeToBlackComplete;

			// Fade From black
			StartFadeFromBlack ();
		
		}

	}
}
