#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 2, 2015
#endregion 


#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#endregion

namespace WhatPumpkin.GameSettings {

	public class PlayerSettingsEventAgs : EventArgs {
	
		//public enum UpdateType { 
	
	}

	[System.Serializable]

	public class PlayerSettings  {

		static public event EventHandler<PlayerSettingsEventAgs> SettingsUpdate;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>

		static public PlayerSettings Instance { get; private set; }

		const string FILE_NAME = "/PlayerSettings.sav";

		public const int SAVE_SLOTS = 3;

		#region fields

		/// <summary>
		/// The save load slot settings.
		/// </summary>

		[SerializeField] List<SaveLoadSlotSettings> _saveLoadSlotSettings = new List<SaveLoadSlotSettings>();

		/// <summary>
		/// The audio settings.
		/// </summary>

		[SerializeField] WhatPumpkin.GameSettings.AudioSettings _audioSettings = new WhatPumpkin.GameSettings.AudioSettings(1F,1F,1F,1F);

		/// <summary>
		/// The quality settings.
		/// </summary>

		//[SerializeField] int _qualitySettings = 0;

		#endregion

		#region properties

		/// <summary>
		/// Gets the slot settings.
		/// </summary>
		/// <value>The list of slot settings.</value>

		public List<SaveLoadSlotSettings> SlotSettings { get { return _saveLoadSlotSettings; } }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.GameSettings.PlayerSettings"/> class.
		/// </summary>

		public PlayerSettings() {
		
			if (Instance == null) {Instance = this;}

			for (int i = 0; i < SAVE_SLOTS; i++) {
			
				_saveLoadSlotSettings.Add(new SaveLoadSlotSettings("Empty", "", "",""));
			
			}

		}

		/// <summary>
		/// Adjusts the audio settings.
		/// </summary>
		/// <param name="audioSettings">Audio settings.</param>

		public void AdjustAudioSettings(WhatPumpkin.GameSettings.AudioSettings audioSettings) {
			_audioSettings = audioSettings;
		}
	
		/// <summary>
		/// Adjusts the slot setting. 
		/// Adds a new slot at the end of the index if the slot does not exist (this does not guaruntee that it will be added at the correct slot and must be accounted for).
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="dateTime">Date time.</param>
		/// <param name="thumbnailKey">Thumbnail key.</param>
		/// <param name="slot">Slot.</param>

		public void AdjustSlotSetting(string title, string dateTime, string activeThumbnailKey, string inactiveThumbnailKey, int slot) {

			// TODO: Start the game by creating three (X) blank ones

			// Make sure the slot exists and then adjust the settings
			if (_saveLoadSlotSettings.Count > slot) {
				_saveLoadSlotSettings[slot].Set(title,dateTime,activeThumbnailKey,inactiveThumbnailKey);						
			}
			else {

				// Add new slot
				_saveLoadSlotSettings.Add(new SaveLoadSlotSettings(title, dateTime, activeThumbnailKey,inactiveThumbnailKey));
			}

			Update ();


		}

		void Update() {

			Debug.Log ("Update Player Settings");

			if (SettingsUpdate != null) {
				SettingsUpdate(this, new PlayerSettingsEventAgs());			
			}

			// Save data to file
			Save ();
		}

		/// <summary>
		/// Save.
		/// </summary>

		static public void Save() {
		
			// Create Binary formater
			BinaryFormatter bf = new BinaryFormatter();
			// Create file
			FileStream file = File.Create (Application.persistentDataPath + FILE_NAME);
			// Serialize the data to file
			bf.Serialize (file, Instance); 
			// Close the file
			file.Close();


		}

		/// <summary>
		/// Load this instance.
		/// </summary>
	
		static public bool Load() {


//			Debug.Log ("Loading Player Settings: " + Application.persistentDataPath + FILE_NAME);

			bool loaded = false;

			if (File.Exists (Application.persistentDataPath + FILE_NAME)) {

//				Debug.Log ("File Found");

				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Open);

				try {
					Instance  = (PlayerSettings)bf.Deserialize(file);
				}
				catch (System.Runtime.Serialization.SerializationException e) {

					Debug.Log ("Serialization Exception - Creating New Player Settings");
					// Handle serialization exception
					Instance = new PlayerSettings ();
					Instance.Update();

					throw(e);

					return false;


				}


				loaded = true;
			}
			else {

				Debug.Log ("File Not Found - Creating Player Settings");
				// File doesn't exist, create new one
				Instance = new PlayerSettings ();

			}


			Instance.Update ();
			return loaded;
		}

	}

	[System.Serializable]
	
	public class SaveLoadSlotSettings {
	
		#region fields

		/// <summary>
		/// The date time.
		/// </summary>

		string _dateTime = "";

		/// <summary>
		/// The _active thumbnail key.
		/// </summary>

		string _activeThumbnailKey;

		/// <summary>
		/// The _inactive thumbnail key.
		/// </summary>

		string _inactiveThumbnailKey;

		/// <summary>
		/// The title.
		/// </summary>

		string _title;

		#endregion

		#region properties

		/// <summary>
		/// Gets the date time.
		/// </summary>
		/// <value>The date time.</value>

		public string DateTime { get { return _dateTime; } }


		/// <summary>
		/// Gets the active thumbnail key.
		/// </summary>
		/// <value>The thumbnail key.</value>

		public string ActiveThumbnailKey { get { return _activeThumbnailKey; } }

		/// <summary>
		/// Gets the inactive thumbnail key.
		/// </summary>
		/// <value>The inactive thumbnail key.</value>

		public string InactiveThumbnailKey { get { return _inactiveThumbnailKey; } }

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>

		public string Title { get { return _title; } }

		#endregion

		#region methods

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.GameSettings.SaveLoadSlotSettings"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="dateTime">Date time.</param>
		/// <param name="thumbnailKey">Thumbnail key.</param>

		public SaveLoadSlotSettings(string title, string dateTime, string _activeThumbnailKey, string _inactiveThumbnailKey) {
		
			Set (title, dateTime, _activeThumbnailKey, _inactiveThumbnailKey);
		
		}


		/// <summary>
		/// Set the specified title, dateTime, activeThumbnailKey and inactiveThumbnailKey.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="dateTime">Date time.</param>
		/// <param name="activeThumbnailKey">Active thumbnail key.</param>
		/// <param name="inactiveThumbnailKey">Inactive thumbnail key.</param>

		public void Set(string title, string dateTime, string activeThumbnailKey, string inactiveThumbnailKey) {
		
			_title = title;
			_dateTime = dateTime;
			_activeThumbnailKey = activeThumbnailKey;
			_inactiveThumbnailKey = inactiveThumbnailKey;
		}

		#endregion



	}

	// TODO: Place in audio controller

	[System.Serializable]

	public class AudioSettings {
	
		/// <summary>
		/// The _master volume.
		/// </summary>

		float _masterVolume = 0F;

		/// <summary>
		/// The sound effects volume
		/// </summary>

		float _sfxVolume = 0F;

		/// <summary>
		/// The ambient volume.
		/// </summary>

		float _ambientVolume = 0F;
	

		/// <summary>
		/// The music volume.
		/// </summary>

		float _musicVolume = 0F;

		/// <summary>
		/// Gets the SFX volume.
		/// </summary>
		/// <value>The SFX volume.</value>

		public float SFXVolume { get { return _sfxVolume; } }

		/// <summary>
		/// Gets the ambient volume.
		/// </summary>
		/// <value>The ambient volume.</value>

		public float AmbientVolume { get { return _ambientVolume; } }

		/// <summary>
		/// Gets the music volume.
		/// </summary>
		/// <value>The music volume.</value>

		public float MusicVolume { get { return _musicVolume; } }
	
		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.AudioSettings"/> class.
		/// </summary>
		/// <param name="sfxVolume">Sfx volume.</param>
		/// <param name="ambientVolume">Ambient volume.</param>
		/// <param name="musicVolume">Music volume.</param>

		public AudioSettings(float masterVolume, float sfxVolume, float ambientVolume, float musicVolume) {
		
		
			Set (masterVolume, sfxVolume, ambientVolume, musicVolume);
		
		}

		public void Set(float masterVolume, float sfxVolume, float ambientVolume, float musicVolume) {
		
			_sfxVolume = sfxVolume;
			
			_ambientVolume = ambientVolume;
			
			_musicVolume = musicVolume;


		}

		//TODO: Place in Video Settings Controller

		[System.Serializable]

		public class QualitySettings {
		
			// TODO:

		}
	}

}