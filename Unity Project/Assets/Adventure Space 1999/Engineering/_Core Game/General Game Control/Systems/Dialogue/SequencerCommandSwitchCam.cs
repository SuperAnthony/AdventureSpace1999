using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using WhatPumpkin.Dialogue;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandSwitchCam : SequencerCommand {

		public void Start() {
			// Add your initialization code here. You can use the GetParameter***() and GetSubject()
			// functions to get information from the command's parameters. You can also use the
			// Sequencer property to access the SequencerCamera, CameraAngle, Speaker, Listener,
			// and other properties on the sequencer. If IsAudioMuted() is true, the player has
			// muted audio.
			//
			// If your sequencer command only does something immediately and then finishes,
			// you can call Stop() here and remove the Update() method.

			// TODO: Do this correctly to handle strings as well
			try {
				WhatPumpkin.Dialogue.Conversation.ActiveConversation.SwitchCameraNode (GetParameterAsInt(0));
			}
			catch {
			
				Debug.LogError("No active conversation found");

			}

			Stop ();
		}

		
	}

}
 

/**/