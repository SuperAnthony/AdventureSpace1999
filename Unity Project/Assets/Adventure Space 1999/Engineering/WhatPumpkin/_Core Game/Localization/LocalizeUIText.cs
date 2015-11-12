#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 19th, 2015
#endregion 

#region using
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace WhatPumpkin.Localization {

	[RequireComponent(typeof(Text))]

	/// <summary>
	/// Localize user interface text.
	/// </summary>

	public class LocalizeUIText : MonoBehaviour {

		/// <summary>
		/// The message key.
		/// </summary>

		[SerializeField] string _messageKey;


		/// <summary>
		/// Start this instance.
		/// </summary>

		void Start() {
		
			SetText ();

		}

		/// <summary>
		/// Sets the text based on the message key.
		/// </summary>

		void SetText() {
			Text text = this.GetComponent<Text> ();
			if (text != null) {text.text = GameController.MessageManager.GetMessage(_messageKey);}
		}

		/// <summary>
		/// Sets the message key.
		/// </summary>
		/// <param name="key">Key.</param>

		public void SetMessageKey(string key) {
			_messageKey = key;
			SetText ();
		}

	}
}
