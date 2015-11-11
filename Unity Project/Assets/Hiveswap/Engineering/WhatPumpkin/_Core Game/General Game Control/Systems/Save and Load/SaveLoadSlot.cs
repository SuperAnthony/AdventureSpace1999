#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace WhatPumpkin.GameSettings {

	/// <summary>
	/// Save load slot.
	/// </summary>

	public class SaveLoadSlot : MonoBehaviour {

		/// <summary>
		/// The title.
		/// </summary>

		[SerializeField] Text _title;

		/// <summary>
		/// The date time.
		/// </summary>

		[SerializeField] Text _dateTime;

		/// <summary>
		/// The preview image when active.
		/// </summary>

		[SerializeField] Image _activePreviewImage;

		/// <summary>
		/// The preview image when not active.
		/// </summary>

		[SerializeField] Image _inactivePreviewImage;

		/// <summary>
		/// Awake this instance.
		/// </summary>

		void Awake() {

			PlayerSettings.SettingsUpdate += HandleSettingsUpdate;
		}

		void OnDestroy() {
		
			try {
				PlayerSettings.SettingsUpdate -= HandleSettingsUpdate;
			}
			catch (System.Exception e) {
			
				throw(e);

			}

		}

		/// <summary>
		/// Handles the settings update.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSettingsUpdate (object sender, PlayerSettingsEventAgs e)
		{


			PlayerSettings settings = (PlayerSettings)sender;

//			Debug.Log ("num slot settings: " + settings.SlotSettings.Count);

			if (_title != null) {
				_title.text = settings.SlotSettings [this.GetComponent<Identifier> ().ID].Title;
						}

			if (_dateTime != null) {
				_dateTime.text = settings.SlotSettings [this.GetComponent<Identifier> ().ID].DateTime;
			}


			if (_activePreviewImage != null) {

				IKeyed keyedObj = Keyed.GetKey (settings.SlotSettings [this.GetComponent<Identifier> ().ID].ActiveThumbnailKey);

				KeyedSprite keyed_sprite = null;

				try {
					keyed_sprite = (KeyedSprite)keyedObj;
				}
				catch (System.InvalidCastException invalidCastException){
					throw(invalidCastException);
				}


				if (keyed_sprite != null) {
						_activePreviewImage.sprite = keyed_sprite.Sprite;
				}
			}

			if (_inactivePreviewImage != null) {
				
				IKeyed keyedObj = Keyed.GetKey (settings.SlotSettings [this.GetComponent<Identifier> ().ID].InactiveThumbnailKey);

				KeyedSprite keyed_sprite = null;
				
				try {
					keyed_sprite = (KeyedSprite)keyedObj;
				}
				catch (System.InvalidCastException invalidCastException){
					throw(invalidCastException);
				}


				//KeyedSprite keyed_sprite = (KeyedSprite)keyedObj;
				
				
				if (keyed_sprite != null) {
					_inactivePreviewImage.sprite = keyed_sprite.Sprite;
				}
			}
						

		}
	}
}