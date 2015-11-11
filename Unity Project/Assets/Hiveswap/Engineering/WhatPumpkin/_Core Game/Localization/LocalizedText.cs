#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 17, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Entities; 
#endregion

namespace WhatPumpkin.Localization {

	public enum Langauge {US_ENG = 0, SPANISH = 1}

	[System.Serializable]

	public class LocalizedText : Keyed, IKeyed, ILocalizedText {

		#region fields

		/// <summary>
		/// The key.
		/// </summary>

		//protected string _key;

		/// <summary>
		/// The text based on language.
		/// </summary>

		[SerializeField] List <string> _text = new List<string>();

		#endregion

		#region properties

		/// <summary>
		/// Sets the key.
		/// </summary>
		/// <value>The key.</value>
		/// 

		public override string Key { 
		
			 get { return _key; } 
		
			/*
			set {

				// Will only allow the user to set the key if the key is "" or 'null'
				if(_key == null || _key == "") {
					_key = value;
				}
				else {
					Debug.LogError("You are trying to set a key which already has a value. This is not allowed");
				}

			}*/
		
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>

		public List <string> Text { get { return _text; } }


		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.HiveSwap.Messages.LocalizedText"/> class.
		/// </summary>

		public LocalizedText() {
		
			// Default constructor
		}


	}

}
