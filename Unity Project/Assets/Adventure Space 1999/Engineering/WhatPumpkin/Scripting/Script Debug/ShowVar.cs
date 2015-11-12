#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 4th 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using PixelCrushers.DialogueSystem;
using WhatPumpkin.ScriptingLanguage;
#endregion



	[RequireComponent(typeof(Text))]

	/// <summary>
	/// Show variable.
	/// </summary>

	public class ShowVar : MonoBehaviour {

		Text _text;

		[SerializeField] string variableName = "";

		// Use this for initialization
		void Start () {
			_text = this.GetComponent<Text> ();

		}
		
		// Update is called once per frame
		void Update () {
			_text.text = "" + variableName + ": " + DialogueLua.GetVariable (variableName).AsString;

		}
	}
