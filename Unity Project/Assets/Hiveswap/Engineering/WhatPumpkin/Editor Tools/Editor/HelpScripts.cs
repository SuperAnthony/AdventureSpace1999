using UnityEngine;
using System.Collections;

#region UNITY_EDITOR
using UnityEditor;

using WhatPumpkin;
using WhatPumpkin.Actions;
using WhatPumpkin.Actions.Sequences;
#endregion

public class HelpScripts : MonoBehaviour {

	[MenuItem("Anthony/CheckData")]

	static private void CheckData () {

	//	Debug.Log ("Remove Garbage");

		//System.GC.Collect ();

		//Resources.

		/*
		foreach (Keyed key in Resources.FindObjectsOfTypeAll<Keyed> ()) {
		
			Debug.Log ("Key: " + key);
		
		}*/

		//Keyed.KeyList.Clear ();
		//Keyed.KeyedDictionary.Clear ();

		if (Keyed.KeyList != null && Keyed.KeyList.Count > 0) {

						Debug.Log ("Keyed Objects Count: " + Keyed.KeyList.Count);
				} 
		else {
			Debug.Log ("No keyed objects, hurray!");		
		}
	}

	[MenuItem("Anthony/ListData")]

	static private void ListData () {
		
		//	Debug.Log ("Remove Garbage");
		
		//System.GC.Collect ();
		
		
		
		//Keyed.KeyList.Clear ();
		//Keyed.KeyedDictionary.Clear ();
		if (Keyed.KeyList != null && Keyed.KeyList.Count > 0) {
			
			foreach(Keyed key in Keyed.KeyList) {
			
				Debug.Log (key.Key);

			}
		} 
		else {
			Debug.Log ("No keyed objects, hurray!");		
		}
	}


	[MenuItem("Anthony/Clear Keyed List")]
	
	static private void ClearKeyedList () {

		Resources.UnloadUnusedAssets ();
		Keyed.KeyList.Clear ();
	}




//	[MenuItem("Anthony/Transfer Persistent Data")]
/*
	static private void TransferData() {
	

		GameData sceneData = GameObject.Find ("SceneData").GetComponent<GameData> ();
		sceneData.ActionSequences = GameObject.Find ("GameController").GetComponent<ActionController> ().PersistentActionSequences;
		sceneData.VerbActionSequences = GameObject.Find ("GameController").GetComponent<ActionController> ().VerbActionSequences;

*/
	/*

		ActionSequence [] sequences = GameController.PersisentGameData.ActionSequences; 

		ActionController GC = GameObject.Find ("GC").GetComponent<ActionController> ();
		foreach (ActionSequence sequence in  GC.PersistentActionSequences) {
				
		
			sequences = DataUtilities.AddArrayElement<ActionSequence>(sequences, sequence);
		
		}

		GameController.PersisentGameData.ActionSequences = sequences;
*/

		//GameController.PersisentGameData.ReceiveCombineActionSequences (GameController.ActionControl.CombineActionSequence);
		//GameController.PersisentGameData.ReceiveVerbActionSequences (GameController.ActionControl.SceneVerbActionSequence);
		//GameController.PersisentGameData.ReceiveActionSequences (GameController.ActionControl.PersistentActionSequences);

	}
	/*
	bool IsDuplicate<T>(T [] array, T item) where T : Keyed {
	}*/
//}
