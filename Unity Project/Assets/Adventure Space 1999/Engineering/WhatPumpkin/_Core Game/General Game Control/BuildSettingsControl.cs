#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Localization;
#endregion

namespace WhatPumpkin {

	public class BuildSettingsControl : MonoBehaviour {

		#region fields

		/// <summary>
		/// The singleton instance of this component.
		/// </summary>

		static BuildSettingsControl _instance;

		/// <summary>
		/// Show the version number?
		/// </summary>

		bool _showVersionNumber = true;

		/// <summary>
		/// The build's language.
		/// </summary>

		[SerializeField] Langauge _language = Langauge.US_ENG;

		/// <summary>
		/// The version.
		/// </summary>

		[SerializeField] string _version = "";

		#endregion

		#region properties

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <value>The language.</value>

		public Langauge Language { get { return _language; } }

		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <value>The version.</value>

		public string Version { get { return _version; } }

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>

		public BuildSettingsControl Instance { get { return _instance; } }

		#endregion

		#region methods

		// Use this for initialization
		void Awake () {
			_instance = this;
		}

		void OnGUI() {
		
			if (_showVersionNumber) {
			
				GUI.Label (new Rect(Screen.width - 80,Screen.height-20,300,100), "v" + _version);

			
			}
		
		}

		#endregion
		
	}
}