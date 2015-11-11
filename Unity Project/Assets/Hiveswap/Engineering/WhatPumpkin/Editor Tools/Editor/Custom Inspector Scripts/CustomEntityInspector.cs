#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 22, 2015
#endregion 

#region using 
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;
using UnityEditor;
#endregion


[CustomEditor(typeof(EntityInfo))]

	public class CustomEntityInspector : Editor {

		public override void OnInspectorGUI () {
	

			// Draw the default inspector
			DrawDefaultInspector ();
			SetKeyToGameObjectName ();

		}

		static public void SetKeyToGameObjectName() {

			// Create a button to set te name
			GUI.backgroundColor = Color.cyan;
			if (GUILayout.Button ("Set Key to GameObject Name  [alt+k]")) {
				ShortcutItems.SetKeyToGameObjectName();
			}
			
		}
	
	}

