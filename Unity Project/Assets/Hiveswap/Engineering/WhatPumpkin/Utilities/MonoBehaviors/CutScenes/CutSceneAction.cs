#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 31, 2015
#endregion 

using UnityEngine;
using System.Collections;

using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.CutScenes;

public class CutSceneAction : MonoBehaviour {

	/// <summary>
	/// The cutscene that's required to perform the referenced action sequence
	/// </summary>

	[SerializeField] string _cutScene = "";

	/// <summary>
	/// The action sequence.
	/// </summary>

	[SerializeField] ActionSequence _actionSequence; 

	/// <summary>
	/// Occurs on start
	/// </summary>
	
	void Start() {
	
		CutScene.StartCutscene += HandleStartCutscene;

	}

	/// <summary>
	/// Handles the start of a cutscene.
	/// </summary>

	void HandleStartCutscene ()
	{
		if (CutScene.CurrentlyPlaying == _cutScene) {
			_actionSequence.Play();
		}
	}

	/// <summary>
	/// Occurs on Destroy
	/// </summary>
	
	void OnDestroy() {

		CutScene.StartCutscene -= HandleStartCutscene;
	}
}
