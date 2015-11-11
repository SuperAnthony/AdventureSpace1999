#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 13, 2014
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Actions.Sequences;

#endregion

namespace WhatPumpkin.Hiveswap.Abilities {

	public class AbilityActivatedArgs : EventArgs {
	
		Ability _ability;

		public Ability ActivatedAbility { get { return _ability; } }

		public AbilityActivatedArgs(Ability ability) {
			_ability = ability;
		}

	
	}

	[System.Serializable]

	/// <summary>
	/// Ability/Abilitech that players gain over time.
	/// These abilities work much like inventory. The player acquires it and a verb coin can open, it is however displayed differently and does not have quantity.
	/// </summary>

	public class Ability : Keyed, ISwitchable {

		#region static events

		static public event EventHandler<AbilityActivatedArgs> AbilityActivated;

		#endregion

		#region static fields

		//static Dictionary<string, Ability> _abilities = new Dictionary<string, Ability>();

		#endregion

		#region fields

		/// <summary>
		/// The verb action sequences available when the player selects this ability.
		/// </summary>

		[SerializeField] protected VerbActionSequence [] _verbActionSequences;

		/// <summary>
		/// Is this keyed object active
		/// </summary>

		[SerializeField] bool _isActive = false;

		#endregion


		#region properties

		/// <summary>
		/// Gets the verb action sequences associated with this ability.
		/// </summary>
		/// <value>The verb action sequences.</value>

		public VerbActionSequence [] VerbActionSequences { get { return _verbActionSequences; } }

		/// <summary>
		/// Gets a value indicating whether this instance is available.
		/// </summary>
		/// <value><c>true</c> if this instance is available; otherwise, <c>false</c>.</value>

		public bool IsActive { get { return _isActive; } } // Some logic here is for a weapon


		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		#endregion

		#region methods

		public Ability() {

		
		}

		/// <summary>
		/// Open an ability's verb coin
		/// </summary>
		/// <param name="sender">The transform that is attempting this performance, persumably the UI element.</param>

		internal void OpenVerbCoin(Transform sender) {
			GameController.ActionControl.OpenVerbCoinPanel (this.VerbActionSequences, sender);
		}

		/// <summary>
		/// Makes this ability available to the player
		/// </summary>

		public void Activate() {

			_isActive = true;
		
			if (AbilityActivated != null) {
			
				AbilityActivated(this, new AbilityActivatedArgs(this)); 
			}


		}

		/// <summary>
		/// Makes this ability unavailable to the player.
		/// </summary>

		public void Deactivate() {
		
			_isActive = false;

		}

		#endregion

	}
}