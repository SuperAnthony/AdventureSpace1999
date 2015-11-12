#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 1, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Xml;
using WhatPumpkin.Data.XML;
using System.Collections.Generic;
using WhatPumpkin.CameraManagement;
#endregion

namespace WhatPumpkin.CutScenes {

	/// <summary>
	/// Cutscene - contains cutscene data
	/// </summary>

	[System.Serializable]

	public class RequiredCutscene : MonoBehaviour {

		[SerializeField] string _requiredCutScene = "";

		[SerializeField] string _disabledOnCutscene = "";

		[SerializeField] bool _inactiveDuringAllCutscenes = false;



		void Start() {
		
			// Subscribe this to the cutscene start
			CutScene.StartCutscene += OnCutsceneStart;

			if (_inactiveDuringAllCutscenes) {
				CutScene.EndCutscene += HandleEndCutscene;
			}

			// Handle as though the cutscene has started
			OnCutsceneStart ();

		}

		/// <summary>
		/// Handles the end of a cutscene.
		/// </summary>

		void HandleEndCutscene ()
		{
			if (_inactiveDuringAllCutscenes) {
				Activate();
			}


		}

		void OnDestroy() {

			CutScene.StartCutscene -= OnCutsceneStart;
			if(_inactiveDuringAllCutscenes){CutScene.EndCutscene -= HandleEndCutscene;}
		}

		/// <summary>
		/// Determines whether this instance can be enabled.
		/// </summary>
		/// <returns><c>true</c> if this instance can be enabled; otherwise, <c>false</c>.</returns>

		bool CanEnable() {
			return (_requiredCutScene == CutScene.CurrentlyPlaying || !HasRequiredCS()) ;
				//&& ( (_disabledOnCutscene != CutScene.CurrentlyPlaying) /*|| !HasDisableCS()*/ );
		}


		/// <summary>
		/// Determines whether this instance has a required Cutscene.
		/// </summary>
		/// <returns><c>true</c> if this instance has required C; otherwise, <c>false</c>.</returns>

		bool HasRequiredCS() {

			if (_requiredCutScene == "") {
								return false;
						}

			return _requiredCutScene != null /*|| _requiredCutScene != ""*/;
		}

		/// <summary>
		/// Determines whether this instance has a disable CS.
		/// </summary>
		/// <returns><c>true</c> if this instance has disable C; otherwise, <c>false</c>.</returns>

		bool HasDisableCS() {
			return _disabledOnCutscene != null || _disabledOnCutscene != "";
		}


		/// <summary>
		/// Raises the cutscene start event.
		/// </summary>

		void OnCutsceneStart() {
		
			/// Should this be rendered
			if(CanEnable()) {
				Activate();

					
			}
			else {
				Deactivate();
			}

			// Check for disable
			if (HasDisableCS () && CutScene.CurrentlyPlaying == _disabledOnCutscene) {
				Deactivate();
			}

		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		void Deactivate() {

				this.gameObject.SetActive(false);
			

		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		void Activate() {

			this.gameObject.SetActive(true);
		}

	}
}
