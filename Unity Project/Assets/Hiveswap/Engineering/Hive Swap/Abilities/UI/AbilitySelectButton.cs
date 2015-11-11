#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 3, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using WhatPumpkin;
using WhatPumpkin.Hiveswap.Abilities;
#endregion

namespace WhatPumpkin {

	[RequireComponent(typeof(Button))]

	/// <summary>
	/// Ability select button.
	/// </summary>

	public class AbilitySelectButton : MonoBehaviour {

		/// <summary>
		/// The associated ability key.
		/// </summary>

		[SerializeField] string _associatedAbilityKey;

		/// <summary>
		/// The associated ability.
		/// </summary>

		Ability _associatedAbility; // Received by the activate ability event

		/// <summary>
		/// The active image.
		/// </summary>

		[SerializeField] Sprite _activeImage;

		/// <summary>
		/// The inactive image.
		/// </summary>

		[SerializeField] Sprite _inactiveImage;

		Image _image;

		// Is this button active
		bool _isActive;

		/// <summary>
		/// Handles the click.
		/// </summary>

		public void HandleClick() {
		
			if (_isActive) {
				AccessAbility();
			}

		}

		/// <summary>
		/// Accesses the ability.
		/// </summary>

		void AccessAbility() {
			_associatedAbility.OpenVerbCoin(this.transform);
		
		}

		/// <summary>
		/// Occurs on start
		/// </summary>

		public void Start() {

			// Get the image component
			_image = this.GetComponent<Image> ();	
		
			Ability.AbilityActivated += HandleAbilityActivated;

		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>

		void OnDestroy() {
		
			Ability.AbilityActivated -= HandleAbilityActivated;
		}

		/// <summary>
		/// Handles the an ability activated.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleAbilityActivated (object sender, AbilityActivatedArgs e)
		{
			if(e.ActivatedAbility.Key == _associatedAbilityKey) {
				// Check to see if the correct ability was activated, if so, then activate this button
				Activate ();
				_associatedAbility = e.ActivatedAbility;
			}

		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		void Activate() {
		
			_isActive = true;

			if (_image != null) {
			
				_image.sprite = _activeImage;

			}
		}

		void Deactivate () {

			if (_image != null) {
				
				_image.sprite = _inactiveImage;
				
			}
		
		}

	}
}
