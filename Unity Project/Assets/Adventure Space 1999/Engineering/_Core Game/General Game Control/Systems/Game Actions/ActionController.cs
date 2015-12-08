#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December, 2014
#endregion 

#region using
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using WhatPumpkin.Sgrid;
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Actions.UI;


using WhatPumpkin.Sgrid.Triggers;
#endregion

namespace WhatPumpkin.Actions {

	public class ActionController : MonoBehaviour {
		

		#region static fields

		/// <summary>
		/// The singleton instance of the action controller.
		/// </summary>

		static ActionController _instance;

		#endregion

		#region member fields

		/// <summary>
		/// The verb coin panel.
		/// </summary>

		[SerializeField] VerbCoinPanel _verbCoinPanel; // TODO: Move this to hot spot controller




		#endregion

		#region static properties

		static public ActionController Instance { get { return _instance; } }

		/// <summary>
		/// Gets a value indicating whether an action sequence is playing.
		/// </summary>

		public bool IsSequencePlaying { get { return ActionSequence.NumSequencesPlaying > 0; } }


		/// <summary>
		/// Gets the verb coin panel.
		/// </summary>
		/// <value>The verb coin panel.</value>

		public VerbCoinPanel VerbCoinPanel {
		
			get {

				if (_verbCoinPanel == null) {
				
					_verbCoinPanel = GameObject.FindObjectOfType<VerbCoinPanel>();
				}

				return _verbCoinPanel;

			
			}
		
		}

		#endregion

		#region methods

		void Update() {
		
			// If a sequence is playing then use the wait cursor
			if (this.IsSequencePlaying) {
				// Set the cursor wait if a sequence is playing
				GameController.CursorControl.SetCursor("CURSOR_WAIT");
			}
		
		}

	

		/// <summary>
		/// Performs an action using an action type and it's parameters.
		/// </summary>
		/// <param name="actionType">Action type.</param>
		/// <param name="parameters">Parameters.</param>

		public void PerformAction(ActionType actionType, params object[] parameters) {
		
			if(actionType != null) {
				try {
					StartCoroutine(actionType.BeginAction(parameters));
				}
				catch {
				}
			}
		
		}

	

		/// <summary>
		/// Opens the verb coin panel.
		/// </summary>
		/// <param name="actionSequences">Action sequences.</param>
		/// <param name="transform">Transform.</param>

		public void OpenVerbCoinPanel(IList<VerbActionSequence> actionSequences, Transform transform) {


			// If the verb coin panel was not explicitly set up 
			// then locate one on the scene and set it
			if (_verbCoinPanel == null) {
				Debug.Log ("Verb coin panel is null");
				_verbCoinPanel = GameObject.FindObjectOfType<VerbCoinPanel>();
			}
		



			_verbCoinPanel.OpenPanel (transform, actionSequences);
		}


		/// <summary>
		/// On awake
		/// </summary>

		void Awake () {

			if (_instance == null) {
				// Set up the singleton instance	
				_instance = this;
				// Initialize the ActionType Colleciton
				new ActionTypeCollection ();

			} 

			// Make sure the verb coin panel is not null
			// This will not be found outside of the Awake method if the verbcoin panel is disabled
			// (which it usually is)
			if (_verbCoinPanel == null) {
								_verbCoinPanel = GameObject.FindObjectOfType<VerbCoinPanel> ();
						}

		
			// Check to see when a sequence has ended
			ActionSequence.ActionSequencePlayed += HandleActionSequencePlayed;

		}

		void HandleActionSequencePlayed (ActionSequence actionSequence)
		{

//				Debug.Log ("Use Default Cursor");
				GameController.CursorControl.UseDefaultCursor();

		}

	
		/// <summary>
		/// Occurs when this object is about to be destroyed;
		/// </summary>

		void OnDestroy() {
			ActionSequence.ActionSequencePlayed -= HandleActionSequencePlayed;
		}


		#endregion


	}
}
