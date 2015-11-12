#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 20, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using WhatPumpkin.Sgrid.Characters;
#endregion

/// <summary>
/// Changes the main button color to the pc color when a PC is activated
/// </summary>

[RequireComponent(typeof(Image))]

public class ChangeToPCColor : MonoBehaviour {

	/// <summary>
	/// The image component attached to this object.
	/// </summary>

	Image _image;

	void Awake() {

//		Debug.Log ("Change Color: " + this.name);
	
		// Register to the PC activated event
		PlayerCharacter.PCActivated += HandlePCActivated;

		// Get the image attached to this component
		_image = this.GetComponent<Image> ();

	}

	/// <summary>
	/// Handles the PC activated.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>

	void HandlePCActivated (object sender, System.EventArgs e)
	{

//		Debug.Log ("Handle PC Activated: " + this.name);

		PlayerCharacter pc = (PlayerCharacter)sender;

		if (pc != null) {
		
			if(_image != null) {
				_image.color = pc.Color;
			}
		
		}

	}
}
