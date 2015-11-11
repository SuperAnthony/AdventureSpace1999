#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 22, 2015
#endregion 

#region using 
using UnityEngine;
using System.Collections;
using WhatPumpkin.Sgrid.Environment;
using UnityEditor;
#endregion


[CustomEditor(typeof(Room))]

	public class CustomRoomInspector : Editor {

		public override void OnInspectorGUI () {
	

			// Draw the default inspector
			DrawDefaultInspector ();
			ApplyFog ();

		}

		static public void ApplyFog() {

			// Create a button to set te name
			GUI.backgroundColor = Color.green;
			if (GUILayout.Button ("Apply Fog")) {

			Room activeRoom = Selection.activeGameObject.GetComponent<Room>();					

			activeRoom.FogSettings.ApplyFogEditor();


			}
			
		}
	
	}

