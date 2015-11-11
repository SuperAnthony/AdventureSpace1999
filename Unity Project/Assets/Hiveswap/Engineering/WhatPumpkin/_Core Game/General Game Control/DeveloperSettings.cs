#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 17, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Localization;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Developer Settings - contains the global settings that the game will use.
	/// Differs from Game Settings in that these settings will deal directly with settings for developers
	/// and will not refer to game settings/preferences that the player may want to change
	/// For instance, players will not be changing the file locations of resources
	/// </summary>

	public class DeveloperSettings : MonoBehaviour {


		#region static fields
	
		/// <summary>
		/// The active instance of developer settings.
		/// </summary>

		static DeveloperSettings _instance;

		// Type Properties

		/// <summary>
		/// Gets the active developer settings.
		/// </summary>
		/// <value>The active developer settings. Root (Resources) directory by default</value>
		static public DeveloperSettings Instance { get 
			{
				// Check to see if there is an active developer settings singleton and return it
				if(_instance != null) {
					return _instance;
				}
				else { // Otherwise return the game controller version, which should be the same one. This should only occur because Developer Settings has not yet started while the game controller has
					return GameController.Instance.GetComponent<DeveloperSettings>();
				}
			} 
		}

		#endregion

		#region fields

		/// <summary>
		/// The language.
		/// </summary>

		[SerializeField] Langauge _language;

		/// <summary>
		/// The animating textures path.
		/// </summary>

		[SerializeField] string _animatingTexturesPath = "AnimatingTextures";

		/// <summary>
		/// The animating texture index multiplier. This is so that animators can index 
		/// by a tiny float variable and not a full number
		/// </summary>
		[SerializeField] int _animatingTextureIndexMultiplier = 100; 

		/// <summary>
		/// The animation material controller signifier.
		/// This is the string used to determine if a node is an animation material controller
		/// </summary>

		[SerializeField] string _animMatConSignifier = "amatcon_";

	
		#endregion

		#region properties

		public Langauge BuildLanguage { get { return _language; } }

		/// <summary>
		/// Gets the animating textures path.
		/// </summary>
		/// <value>The animating textures path.</value>

		public string AnimatingTexturesPath { get { return _animatingTexturesPath; } }

		/// <summary>
		/// Gets the animation texture index multiplier for controlling textures.
		/// </summary>
		/// <value>The animation tex index multiplier.</value>

		public int AnimTexIndexMultiplier { get { return _animatingTextureIndexMultiplier; } }

		/// <summary>
		/// Gets the animation material controller signifier.
		/// </summary>
		/// <value>The animation material controller signifier.</value>

		public string AnimMatConSignifier { get { return _animMatConSignifier; } }

	
		#endregion

		#region methods

		// Use this for initialization
		void Awake () {
			// Set the active developer settings to this instance
			_instance = this;
		}

		#endregion

	}
	
}
