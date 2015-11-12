using UnityEngine;
using System.Collections;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;

public class GameLoader : MonoBehaviour {

	
	// The persistent, serializable game data for saving and loading
	
	//[System.Serializable]
	/*
	public class GameData {
		
		// List of all the characters
		public List<CharacterData> characterDatas = new List<CharacterData> ();


		public GameData() {}

		// Use this to clear all data
		public void ClearData() {
			// Clear all data in the gamedata
			characterDatas.Clear();
		}

		// Receive data about characters
		public void ReceiveCharacterData(ICharacterData data) {
		
			// Send that character data to the serializable chracter data
			// create a new character data
			CharacterData cd = new CharacterData();
			// Parse the data
			cd.ReceiveCharacterData (data); // Store the concrete data
			// Add this character data to the list
			characterDatas.Add (cd); // Add the concrete data to the character datas list
		}

	}*/
	



	// **************************  TODO: Create Save/Load Class *******************************//
	/*
		/// <summary>
		/// Gets the name of the save slot file (autosave for -1).
		/// </summary>
		/// <returns>The save slot file name.</returns>
		/// <param name="slot">Slot.</param>

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

		/// <summary>
		/// Stores the necessary data from the GameController's data to data to be serialized to a file.
		/// This is where most of the parsing out data will need to happen
		/// </summary>
		/// <param name="data">Data.</param>

		private void StoreData(GameData data) {
			// Clear any data previous stored
			data.ClearData ();
			// Store chracters
			foreach (ICharacterData pc in characters) {data.ReceiveCharacterData(pc);}
		}
		
		/// <summary>
		/// Saves to the autosave slot
		/// </summary>

		public void AutoSave() {
			Save (-1);
		}


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
			GameData data = new GameData();

		
			// Store Data
			StoreData (data);

			// Serialize the data to file
			bf.Serialize (file, data); 


			// Close the file
			file.Close();
		}   

		/// <summary>
		/// Load the specified slot.
		/// </summary>
		/// <param name="slot">Slot.</param>

		public void Load(int slot) {

			Debug.Log ("Load");

			string saveFileName = "/saveslot" + slot.ToString () + ".sav";

			// Make sure hte correct file exists before attempting to open it (TODO: Add exception)
			if(File.Exists(Application.persistentDataPath + saveFileName)) {
				Debug.Log ("File Exists");
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + saveFileName, FileMode.Open);
				GameData data  = (GameData)bf.Deserialize(file);

				// Close the file
				file.Close();

				// TODO: Send Data Back
				int i = 0;
				foreach (ICharacterData pc in data.characterDatas) {
					// Make certain the characer is there
					if(characters[i] != null) {
						characters[i].ReceiveCharacterData(pc);
					}
					else {
						// Todo: Create new game character
					}
					i++;
				}

			}

			// TODO: Broadcast event - save state was loaded if any methods are subscribed
			/*
			if (OnSaveStateLoaded != null) { 
				// Invoke method
				OnSaveStateLoaded ();
			}

			// TODO: Temp - Update inventory data to the active character // TODO: This gets invoked more times than it should be
			GameController.InventoryManager.SelectedPlayerContainerScreen.SwitchCointainer (PlayerCharacter.Active.GetComponent(typeof(IContainer)) as IContainer);
			//InventorySlot.SwitchDisplayContainer (HSPlayerCharacter.Active);

		}*/

}
