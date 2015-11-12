#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 24, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using WhatPumpkin.Dialogue;
using WhatPumpkin;
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Actions;

using WhatPumpkin.ScriptingLanguage;
#endregion


namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command to Play an Action.
	/// </summary>

	public class SequencerCommandPlayAction : SequencerCommand {

		public void Start() {



			string actionType = GetParameter (0);
			string parameters = GetParameter (1);

			Debug.Log ("Sequencer Command - Action Type: " + actionType + " parameters: " + parameters);

			int index = 2;

			// TODO: Just use split dummy

			while (true) {
			
				// Break conditions
				if(GetParameter(index) == "" || GetParameter(index) == null){break;}
				if(index >= Scripting.MAX_ARGUMENTS){break;}

				parameters+= "," + GetParameter(index);

				// Increment the index
				index++;
			}

			Debug.Log("Action Type: " + actionType);
			Debug.Log("Parameters: " + parameters);

			// Add your initialization code here. You can use the GetParameter***() and GetSubject()
			// functions to get information from the command's parameters. You can also use the
			// Sequencer property to access the SequencerCamera, CameraAngle, Speaker, Listener,
			// and other properties on the sequencer. If IsAudioMuted() is true, the player has
			// muted audio.
			//
			// If your sequencer command only does something immediately and then finishes,
			// you can call Stop() here and remove the Update() method.

			try {

				// Perform the action of the action type at parameter 0 with the parameters 1
				GameController.ActionControl.PerformAction(ActionTypeCollection.Instance.GetActionType(actionType),parameters);

			}
			catch {
			
				Debug.LogError("Unable to perform ActionType '" + GetParameter(0) + "' with the parameters '" + GetParameter(1) + "'");

			}

			Stop ();
		}

		
	}

}
 

/**/