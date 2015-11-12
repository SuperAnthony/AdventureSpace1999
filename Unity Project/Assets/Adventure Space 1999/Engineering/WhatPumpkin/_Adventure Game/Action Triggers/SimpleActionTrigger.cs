#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 3, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;

using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.Actions.Triggers {

	public class SimpleActionTrigger : MonoBehaviour {

		[SerializeField] ActionSequence _sequence;

		public void PerformSequence() {
			_sequence.Play ();
		}

	}
}
