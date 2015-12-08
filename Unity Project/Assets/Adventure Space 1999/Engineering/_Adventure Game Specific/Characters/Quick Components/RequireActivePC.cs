#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 9th, 2015
#endregion 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WhatPumpkin.Sgrid.Characters {
	
	/// <summary>
	/// Require active PC.
	/// </summary>
	
	public class RequireActivePC : MonoBehaviour {
		
		/// <summary>
		/// The required PC.
		/// </summary>
		
		[SerializeField] string _requiredPC;

		/// <summary>
		/// The affected game object.
		/// </summary>

		[SerializeField] Button _affectedButton;
		
		/// <summary>
		/// Awake this instance.
		/// </summary>
		
		void Awake() {	PlayerCharacter.PCActivated += HandlePCActivated;   }
		
		/// <summary>
		/// Handles the PC activated.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		
		void HandlePCActivated (object sender, System.EventArgs e)
		{
			PlayerCharacter pc = (PlayerCharacter)sender;
			
			if (pc != null && _affectedButton != null) {

				//Debug.Log ("PC Key: " + pc.Key);

				if(pc.Key == _requiredPC) {
					ChangeState(true);
				}
				else {
					ChangeState(false);
				}
			}
		}

		/// <summary>
		/// Changes the state.
		/// </summary>
		/// <param name="isActive">If set to <c>true</c> is active.</param>

		void ChangeState(bool isActive) {

			//Debug.Log ("Change State");

			_affectedButton.interactable = isActive;
			Image image = _affectedButton.GetComponent<Image>();
			if(image != null) {
				//Debug.Log ("Set image: " + isActive.ToString());
				image.enabled = isActive;
			}
				
		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		
		void OnDestroy() {
			PlayerCharacter.PCActivated -= HandlePCActivated;
		}
	}
}