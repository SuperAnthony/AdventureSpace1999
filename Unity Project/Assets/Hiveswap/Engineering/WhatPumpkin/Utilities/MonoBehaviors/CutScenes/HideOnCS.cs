#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 29, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;

using WhatPumpkin.CutScenes;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Renderer if cutscene is playing
	/// </summary>

	[RequireComponent(typeof(Renderer))]

	public class HideOnCS : MonoBehaviour {

		Renderer _renderer;

		// TODO: Note - there are some outliers in which the hide on closeup could still conflict with this - let's see if that edge case actually comes up and I'll fix
		// TODO: Of course it will come up, keep a note to fix this

		void Start() {
		
			CutScene.StartCutscene += HandleCutsceneStart;
			//CutScene.EndCutscene += HandleCutsceneEnd;

			_renderer = this.GetComponent<Renderer> ();


		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>

		void OnDestroy() {
		
			CutScene.StartCutscene -= HandleCutsceneStart;
			CutScene.EndCutscene -= HandleCutsceneEnd;

		}


		/// <summary>
		/// Handles the cutscene start.
		/// </summary>

		void HandleCutsceneStart() {

			if (_renderer != null) {
								_renderer.enabled = false;
						}

			CutScene.EndCutscene += HandleCutsceneEnd;
		
		}

		/// <summary>
		/// Handles the cutscene end.
		/// </summary>

		void HandleCutsceneEnd() {


			if (_renderer != null) {
				_renderer.enabled = true;
			}
		
		}
	}
}