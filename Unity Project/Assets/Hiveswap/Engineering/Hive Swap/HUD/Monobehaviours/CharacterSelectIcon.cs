#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 1, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using WhatPumpkin;
using WhatPumpkin.Sgrid.Characters;
#endregion

/// <summary>
/// Character select icon.
/// </summary>

public class CharacterSelectIcon : MonoBehaviour {

	/// <summary>
	/// The associated Player Character that will be activated when this button is clicked.
	/// </summary>

	[SerializeField] PlayerCharacter AssociatedPC;

	/// <summary>
	/// The sprite that's used when the associated pc is active.
	/// </summary>

	[SerializeField] Sprite _activePartyMemberSprite;

	/// <summary>
	/// The sprite that's used when this party member is available for switching but not active
	/// </summary>

	[SerializeField] Sprite _availablePartyMemberSprite;

	/// <summary>
	/// The sprite that's used when this party member has joined the party but is currently unavailable
	/// </summary>

	[SerializeField] Sprite _unavailablePartyMemberSprite;

//	bool _isActive = false;
	
	Image _image;

	public void HandleClick() {
	
		SwitchActivePC ();

	}


	/// <summary>
	/// Switchs the active PC to the associated PC.
	/// It is anticipated that a button script will invoke this method
	/// </summary>

	void SwitchActivePC() {

		if (AssociatedPC.IsAvailablePartyMember) {
			// Activate the party member
			//GameController.PartyManager.Activate(AssociatedPC);
			AssociatedPC.Activate();


		}
	}

	void Start() {

		_image = this.GetComponent<Image> ();	
	
		if (AssociatedPC != null) {
		
			// TODO: One Event with event args

			// Register to PC events
			AssociatedPC.JoinedParty += HandleJoinedParty; 
			AssociatedPC.MadeAvailableToParty += HandleMadeAvailableToParty;
			AssociatedPC.MadeUnavailableFromParty += HandleMadeUnavailableFromParty;
		
	
		}

		SaveLoad.LoadSceneComplete += HandleLoadSceneComplete;



	}

	void HandleLoadSceneComplete (object sender, SaveEventArgs e)
	{
	
		if (AssociatedPC.IsAvailablePartyMember) {
			HandleMadeAvailableToParty();		
		}

	}

	void HandleMadeUnavailableFromParty ()
	{

		//_isActive = false;

		if (_image != null) {
			_image.sprite = _unavailablePartyMemberSprite;


		}

		// Disallow interaction
		Button button = this.GetComponent<Button> ();
		if (button != null) {
			button.interactable = false;	
		}

	}

	void HandleMadeAvailableToParty ()
	{
		//_isActive = true;

		if (_image != null) {
			_image.sprite = _availablePartyMemberSprite;
		}

		// Allow interaction
		Button button = this.GetComponent<Button> ();
		if (button != null) {
			button.interactable = true;	
		}
	}

	void HandleJoinedParty ()
	{
		// Don't think there's much to do here
	}

	void OnDestroy() {
		AssociatedPC.JoinedParty -= HandleJoinedParty; 
		AssociatedPC.MadeAvailableToParty -= HandleMadeAvailableToParty;
		AssociatedPC.MadeUnavailableFromParty -= HandleMadeUnavailableFromParty;
		SaveLoad.LoadSceneComplete -= HandleLoadSceneComplete;
	}

}
