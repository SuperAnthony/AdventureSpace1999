using UnityEngine;
using System.Collections;
using WhatPumpkin.CutScenes;

public class DisableObjectsOnCS : MonoBehaviour {

	/// <summary>
	/// The disable objects.
	/// </summary>
	
	[SerializeField] GameObject [] _disableObjects;

	/// <summary>
	/// Will we reactivate the objects when the cutscene is complete
	/// </summary>

	[SerializeField] bool _reactivateOnCompletion = true;


	void Awake() {
		
		CutScene.StartCutscene += HandleStartCutscene;
		
	}
	
	/// <summary>
	/// Handles the start cutscene.
	/// </summary>
	
	void HandleStartCutscene ()
	{

		// Disable the cut scenes
		foreach (GameObject gameObject in _disableObjects) {
			gameObject.SetActive(false);
		}

		CutScene.EndCutscene += HandleEndCutscene;
	}
	
	void HandleEndCutscene ()
	{
		CutScene.EndCutscene -= HandleEndCutscene;	

		if(_reactivateOnCompletion) {

			foreach (GameObject gameObject in _disableObjects) {
				gameObject.SetActive(true);
			}
		}

	}
	
	
	void OnDestroy() {
		
		try{
			CutScene.StartCutscene -= HandleStartCutscene;
		}
		catch{
			
			Debug.LogWarning("Attempted to unregister Start Cutscene event but could not");
			
		}
		
		
	}

}
