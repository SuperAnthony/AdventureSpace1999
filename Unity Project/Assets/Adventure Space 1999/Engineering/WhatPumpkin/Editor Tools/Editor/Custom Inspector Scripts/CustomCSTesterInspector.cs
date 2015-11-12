using UnityEngine;
using System.Collections;
using UnityEditor;
using WhatPumpkin.CutScenes;

[CustomEditor(typeof(CutSceneTester))]

public class CustomCSTesterInspector : Editor {

	public override void OnInspectorGUI () {
		
		
		// Draw the default inspector
		DrawDefaultInspector ();
		AttachActorComponent();
		
	}

	public void AttachActorComponent(){
		GUI.backgroundColor = Color.magenta;
		if (GUILayout.Button ("Attach Actor Component to Actors.")) {
			GameObject CSTester = Selection.activeGameObject;
			foreach (Transform actor in CSTester.transform){
				if (actor.gameObject.GetComponent<Actor>() == null){
					actor.gameObject.AddComponent<Actor>();
				}

			}

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
