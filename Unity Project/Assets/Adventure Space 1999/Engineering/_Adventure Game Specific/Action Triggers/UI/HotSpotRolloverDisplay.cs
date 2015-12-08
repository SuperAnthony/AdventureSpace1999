#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 24, 2015
#endregion 

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WhatPumpkin.Sgrid.Triggers;
using WhatPumpkin;

public class HotSpotRolloverDisplay : MonoBehaviour {

	/// <summary>
	/// The display panel.
	/// </summary>

	[SerializeField] GameObject _displayPanel;

	/// <summary>
	/// The text associated with this hot spot rollover display.
	/// </summary>

	[SerializeField] Text _text;

	/// <summary>
	/// Occurs on Awake
	/// </summary>

	void Awake() {
	
		// Register hot spot events

		HotSpot.HotSpotEvent += HandleHotSpotEvent;
	
	}

	/// <summary>
	/// Handles a hot spot event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>

	// TODO: HotSpotEventArgs Appear to exist elsewhere

	void HandleHotSpotEvent (object sender, WhatPumpkin.Sgrid.Triggers.HotSpotEventArgs e)
	{
		if (e.MouseEventType == WhatPumpkin.MouseEventType.MOUSE_ENTER) {
				
			if(e.ConditionsMet){Show (e.Name);}

		}
		else if(e.MouseEventType == WhatPumpkin.MouseEventType.MOUSE_EXIT) {
		
			Hide ();

		}

		// If conditions are not met then automatically hide it
		if (e != null && !e.ConditionsMet) {
			Hide ();
		}
	}

	/// <summary>
	/// Show the specified text.
	/// </summary>
	/// <param name="text">Text.</param>

	void Show(string text) {
	
		_displayPanel.SetActive (true);
		_text.text = GameController.MessageManager.GetMessage(text);

	}

	/// <summary>
	/// Hide this instance.
	/// </summary>

	void Hide() {
	
		_displayPanel.SetActive (false);
		_text.text = "";

	}

}
