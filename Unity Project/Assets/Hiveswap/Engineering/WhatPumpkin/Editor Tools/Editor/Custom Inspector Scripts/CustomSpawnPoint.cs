#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 22, 2015
#endregion 

#region using 
using UnityEngine;
using System.Collections;
using WhatPumpkin.Sgrid.Markers;
using UnityEditor;
using WhatPumpkin;
#endregion


[CustomEditor(typeof(SpawnPoint))]

	public class CustomSpawnPointInspector : Editor {

		public override void OnInspectorGUI () {
	

			// Draw the default inspector
			DrawDefaultInspector ();
			
			/*
			GUI.backgroundColor = Color.green;
			if (GUILayout.Button ("Use In Editor Mode (Doesn't Work)")) {
				
				// Get the game controller in this scen
				GameController gameController = GameObject.FindObjectOfType<GameController>();
				
				if(gameController != null && Selection.activeGameObject != null) {

					gameController.SetEditorSpawnPoint(Selection.activeGameObject.GetComponent<SpawnPoint>());
				}
			}*/
		}

		
	
	}

