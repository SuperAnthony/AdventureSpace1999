#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 21, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// TODO: Don't like the way this is structured
using WhatPumpkin.ScriptingLanguage;
using WhatPumpkin.Entities.Inventory; 
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Save and load game states.
	/// </summary>

	public class SaveLoad : MonoBehaviour {

		#region static members

		const string DEFAULT_SLOT_TITLE = "Intro";

		/// <summary>
		/// The auto save slot number. Not convinced this is the best way. May want the auto save slot to be 0.
		/// </summary>

		public const int AUTO_SAVE_SLOT = 0;

		/// <summary>
		/// The singlton _instance.
		/// </summary>

		static public SaveLoad _instance;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>

		static public SaveLoad Instance { get { return _instance;} }

		/// <summary>
		/// Occurs when user loads scene.
		/// </summary>
		
		static public event EventHandler<SaveEventArgs> UserLoadsScene;

		/// <summary>
		/// Occurs when load scene is complete.
		/// </summary>

		static public event EventHandler<SaveEventArgs> LoadSceneComplete;


		#endregion

		/// <summary>
		/// A collection of thumbnails
		/// </summary>

		[SerializeField] SceneThumbnail [] _thumbnails;


		/// <summary>
		/// The _current thumbnail key that will be used when saving to a save slot.
		/// </summary>

		string _currentThumbnailKey;

		void Awake() {
		
			GameSettings.PlayerSettings.Load ();

			GameController.SoundController.SettingChange += HandleSoundSettingChange;

			//GameController.SoundController
		
		}

		void HandleSoundSettingChange (object sender, UserSoundSettingArgument e)
		{
			GameSettings.PlayerSettings.Instance.AdjustAudioSettings(new GameSettings.AudioSettings(e.MasterVolume, e.SfxVolume, e.AmbientVolume, e.MusicVolume));
		}

	

		void Start() {

			// Set the singleton instance of save load
			_instance = this;

		}



		// For Slot Control
		private void AddSlotSetting(int slot) {

			string title = DEFAULT_SLOT_TITLE;

			if (WhatPumpkin.Sgrid.Environment.Room.ActiveRoom != null) {
				title = WhatPumpkin.Sgrid.Environment.Room.ActiveRoom.Key;
			}

		
			GameSettings.PlayerSettings.Instance.AdjustSlotSetting (title, System.DateTime.Now.ToString (), 
			                                                        WhatPumpkin.Sgrid.Environment.Room.ActiveRoom.ThumbnailHighlightedKey, 
			                                                        WhatPumpkin.Sgrid.Environment.Room.ActiveRoom.ThumbnailUnhighlightedKey, 
			                                                        slot);
			
		}





			// TODO: Broadcast event - save state was loaded if any methods are subscribed
			/*
			if (OnSaveStateLoaded != null) { 
				// Invoke method
				OnSaveStateLoaded ();
			}*/

			// TODO: Temp - Update inventory data to the active character // TODO: This gets invoked more times than it should be
			//GameController.InventoryManager.SelectedPlayerContainerScreen.SwitchCointainer (PlayerCharacter.Active.GetComponent(typeof(IContainer)) as IContainer);
			//InventorySlot.SwitchDisplayContainer (HSPlayerCharacter.Active);
		#region data storage

		// TODO: This belongs in the GameData_Act1 type and should become CollectData

		private void StoreData(GameData_Act1 data) {
			// Clear any data previous stored
			data.ClearData ();
			
			// Find all objects of the type character data act one and receive it 

			// Store the Character Data
			foreach (ICharacterSaveData_Act1 pc in GameController.FindObjectsOfType<WhatPumpkin.Sgrid.Characters.PlayerCharacter>()) 
			{
				data.ReceiveCharacterData(pc);
			}

			// Store the scene info
			foreach (SceneInfo sceneInfo in GameController.SceneManager.SceneInfos) {
			
				data.ReceiveSceneInfoData(sceneInfo);
			
			}

			// Store the Game Variables
			foreach (GameVariable gameVariable in GameController.GameVariableController.GameVariables) {
				data.ReceiveGameVariableData(gameVariable);
			}

			// Store the active player character
			try {
				data.ActiveCharacterKey = GameController.PartyManager.ActivePC.Key;
			}
			catch {
				Debug.LogError ("Could not assign an active PC");			
			}
		}
		
		
		
		
		
		
		#endregion
		
		#region save and save slots
		
		/// <summary>
		/// Saves to the specified slot.
		/// </summary>
		/// <param name="slot">Slot.</param>
		/// 
		public void Save(int slot) {

			// Select file name
			string saveFileName = GetSaveSlotFileName(slot);
			
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + saveFileName);
			
			// Create data
			GameData_Act1 data = new GameData_Act1();
			// Store Data
			StoreData (data);
			
			// Serialize the data to file
			bf.Serialize (file, data); 
			
			
			// Close the file
			file.Close();

			AddSlotSetting (slot);
		}   

		// TODO: Use a delegate for this stuff
		
		public void Load(int slot) {
			
			// TODO: Invoke another delegate here | That load delegate can be specific to handling that load
			
			string saveFileName = "/saveslot" + slot.ToString () + ".sav";


			// Invoke the User Loads Scene Event
			if(UserLoadsScene != null){UserLoadsScene (this, new SaveEventArgs (slot));}

			// Make sure hte correct file exists before attempting to open it 
			if(File.Exists(Application.persistentDataPath + saveFileName)) {
			
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + saveFileName, FileMode.Open);
				GameData_Act1 data  = (GameData_Act1)bf.Deserialize(file);
				
				// Close the file
				file.Close();


				foreach (ICharacterSaveData_Act1 pcData in data.CharacterDatas) {


					foreach(WhatPumpkin.Sgrid.Characters.PlayerCharacter pc in GameController.FindObjectsOfType<WhatPumpkin.Sgrid.Characters.PlayerCharacter>()) {

						if(pcData.Key == pc.Key) {
							pc.ReceiveData(pcData);
						}
						
					
					}
				}

				// Send the Scene info to the Scene Manager
				foreach(SceneInfo sceneInfo in data.SceneInfos) {
					
					GameController.SceneManager.ReceiveSceneInfo(sceneInfo);
				}

				foreach(GameVariable gameVariable in data.GameVariables) {
				
					GameController.GameVariableController.AddVariable(gameVariable);
				}

				// Activate the correct character
				GameController.PartyManager.Activate(data.ActiveCharacterKey);


			}

			// Invoke scene load complete
			if(LoadSceneComplete != null) { LoadSceneComplete(this, new SaveEventArgs(slot));} // TODO: Load Scene Complete? How is this being used and why is it named "scene" and not data for instance

		

			// Load
			GameController.InventoryManager.SelectedPlayerContainerScreen.UpdateDisplay ();
		}

		public bool CanContinue() {
		
			for(int i = 0; i < GameSettings.PlayerSettings.SAVE_SLOTS; i++) {

				if(File.Exists(GetSaveSlotFileName(i))) {
					return true;
				}

			}

			return false;



		}

		/// <summary>
		/// Continue this instance.
		/// </summary>

		public void Continue() {

			Debug.Log ("Continue");
		
			Load (GetNewestSaveSlot () );
		
		
		}

		int GetNewestSaveSlot() {
		
			// Search through all slots and use the newest slot to compare
			int slot = 0;
			int nNewestFile = 0;
			string newestFile = "";

			Debug.Log ("Get Save Slot");

			for (int i = 0; i < GameSettings.PlayerSettings.SAVE_SLOTS; i++) {

				if(File.Exists(GetSaveSlotFileName(i))){

					Debug.Log ("File exist: " + i);
					nNewestFile = i;
					newestFile = GetSaveSlotFileName (i);
					slot = i;
					break;
				}
			}


			
			// Search through each file
			for (int i = slot; i < GameSettings.PlayerSettings.SAVE_SLOTS; i++) {
				
				// Check to see if the file exits, if so, then compare
				string filePath = GetSaveSlotFileName(i);

				if(File.Exists(filePath)){

					Debug.Log ("File exist: " + i);
					
					int compareVal = DateTime.Compare( File.GetLastWriteTime(filePath), File.GetLastWriteTime(newestFile) );		

					Debug.Log ("Compare Val: " + compareVal);

					// TODO: I'm making an assumption here - look up doc to learn what values to use for compare
					if(compareVal > 1) {
					
						// Set to the newest
						nNewestFile = i;
						newestFile = filePath;

					}


				}

			}

			return nNewestFile;

		}
			
		private string GetSaveSlotFileName(int slot) {
				
				string saveFileName = "";
				
				if (slot == -1) {
					// set save slot to autosave
					saveFileName = "autosave.sav";
				}
				else {
					// Set save file name
					saveFileName = "/saveslot" + slot.ToString () + ".sav";
				}
				
				return saveFileName;
		}
			
		#endregion
	
	}
	



	// Data Types

	
	[System.Serializable]

	public class GameData_Act1 {
		
		// List of all the characters
		public List<CharacterSaveData_Act1> CharacterDatas = new List<CharacterSaveData_Act1> ();
		public List<SceneInfo> SceneInfos = new List<SceneInfo>();
		public List<GameVariable> GameVariables = new List<GameVariable> ();

		public string ActiveCharacterKey = "";

		// Scene
		// Active PC
		// Camera

		public GameData_Act1() {}

		// Use this to clear all data

		public void ClearData() {
			// Clear all data in the gamedata
			CharacterDatas.Clear();
			SceneInfos.Clear ();
		}

		// TODO: Generify these methods

		// Receive data about characters
		public void ReceiveCharacterData(ICharacterSaveData_Act1 data) {
			
			// Send that character data to the serializable chracter data
			// create a new character data
			CharacterSaveData_Act1 cd = new CharacterSaveData_Act1();
			// Receive the data
			cd.ReceiveData (data); // Store the concrete data
			// Add this character data to the list
			CharacterDatas.Add (cd); // Add the concrete data to the character datas list
		}

		public void ReceiveSceneInfoData(SceneInfo data) {
			SceneInfos.Add (data);
		}

		public void ReceiveGameVariableData(GameVariable data) {
			GameVariables.Add (data);
		}



	}




	[System.Serializable]
	public class CharacterSaveData_Act1 : ICharacterSaveData_Act1 {

		#region fields and properties

		/// <summary>
		/// The key.
		/// </summary>

		private string _key;
		public string Key { get { return _key; } }

		/// <summary>
		/// The scene.
		/// </summary>

		private string _scene;
		public string Scene { get { return _scene; } }

		/// <summary>
		/// The items.
		/// </summary>

		private List<IItem> _items = new List<IItem> ();
		public List<IItem> Items { get { return _items; } }

		/// <summary>
		/// The last cam since active.
		/// </summary>

		private string _lastCamSinceActive;
		public string LastCamSinceActive { get { return _lastCamSinceActive; } }

		private SerializableTransform _transformData;
		public SerializableTransform  TransformData { get {return _transformData; } }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.CharacterSaveData_Act1"/> class.
		/// </summary>

		public CharacterSaveData_Act1(){
			// Constructor
		}
		
		/// <summary>
		/// Receives the character data and parses it
		/// </summary>
		/// <param name="data">Data.</param>
		
		public void ReceiveData(ICharacterSaveData_Act1 data) {

			_key = data.Key;
		
			_scene = data.Scene;

			_lastCamSinceActive = data.LastCamSinceActive;

			_transformData = data.TransformData;

			/*
			_transform.position = new Vector3 (data.Transform.position.x, data.Transform.position.y, data.Transform.position.z);
			_transform.rotation = new Quaternion (data.Transform.rotation.x, data.Transform.rotation.y, data.Transform.rotation.z, data.Transform.rotation.w);
			_transform.localScale = new Vector3 (data.Transform.localScale.x, data.Transform.localScale.y, data.Transform.localScale.z);
			*/

			
			// Add item
			foreach (IItem item in data.Items) {
				_items.Add((WhatPumpkin.Entities.Inventory.Item)item);
			}

		}
		
		
	}
	


	[System.Serializable]

	public class SceneThumbnail : Keyed  {

		
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public override string Key { get { return _key; } }

		/// <summary>
		/// The image.
		/// </summary>

		[SerializeField] Sprite _image;

	
	
	}

	public interface ISceneThumbnail {
	
		Sprite Image { get; }
	
	}

	/// <summary>
	/// Save event arguments.
	/// </summary>

	public class SaveEventArgs : EventArgs { 
	
		/// <summary>
		/// The _save slot.
		/// </summary>

		int _saveSlot;
	
		/// <summary>
		/// Gets the save slot.
		/// </summary>
		/// <value>The save slot.</value>

		public int SaveSlot { get { return _saveSlot; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.SaveEventArgs"/> class.
		/// </summary>
		/// <param name="slot">Slot.</param>

		public SaveEventArgs(int slot) {
				
			_saveSlot = slot;

		}
	}
}