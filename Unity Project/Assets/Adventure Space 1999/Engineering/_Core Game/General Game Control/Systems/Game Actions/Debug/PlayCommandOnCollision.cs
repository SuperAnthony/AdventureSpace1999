#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 20, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Debuging {

	public class PlayCommandOnCollision : MonoBehaviour {

		/// <summary>
		/// The command.
		/// </summary>

		[SerializeField] string command;


		void OnTriggerEnter(Collider col) {

			// Play 
			DebugConsole.ReceiveCommand (command);
		
		}

	}
}