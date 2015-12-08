#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 23, 2014
#endregion 

#region using
using UnityEngine;
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.Sgrid.Triggers {
	
	/// <summary>
	/// Use action sequence.
	/// </summary>

	public class TriggerActionSequence : Entity  {

		#region fields

	    /// <summary>
	    /// The action sequence that is played when triggered.
	    /// </summary>

	    [SerializeField] ActionSequence _actionSequence;

		#endregion

        public ActionSequence ActionSequence { get { return _actionSequence; } set { _actionSequence = value; } }

		#region methods

		protected override void Start() {
			base.Start ();
		}

		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>

		public void OnTriggerEnter(Collider col) {

			ITriggerActions triggerer = col.GetComponent(typeof(ITriggerActions)) as ITriggerActions;

			if (triggerer != null && triggerer.TriggerAllActionsOnEntry) {_actionSequence.Play();}


		}

		#endregion

		#region editor methods
		[ExecuteInEditMode]
		void OnDrawGizmos() {

			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = new Color(0, 1, 0, 1F);
			Gizmos.DrawWireCube (Vector3.zero, Vector3.one);
			Gizmos.color = new Color(0, 1, 0, 0.2F);
			Gizmos.DrawCube (Vector3.zero, Vector3.one);
			
		}

		#endregion

#if UNITY_EDITOR

		// TODO: Use Trigger to remove this code redundancy

		public void AddAction(string actionType, string parameters, string conditions = "", string frequency = "") {
			_actionSequence.AddAction (actionType, parameters, conditions, frequency);
		}

		/// <summary>
		/// Sets the key. Meant to be used by the editor in the inspector view and no place else.
		/// </summary>
		/// <param name="key">Key.</param>
		public void SetKey(string key) {
			// Raise the rename key event if necessary and rename the key
			OnRenameKey (key);
		}
		
		/// <summary>
		/// Sets the properties. This is meant to only be used in the Editor Mode.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="name">Name.</param>
		
		public void SetProperties(string key, string name) {
			
			// Rename The Key
			SetKey(key);
			_name = name;
			
			
		}


#endif

	}
}
