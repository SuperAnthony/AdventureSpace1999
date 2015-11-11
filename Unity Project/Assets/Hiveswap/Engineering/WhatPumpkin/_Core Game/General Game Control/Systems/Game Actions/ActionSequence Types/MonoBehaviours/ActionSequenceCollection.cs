#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 10, 2015
#endregion 

using UnityEngine;

namespace WhatPumpkin.Actions.Sequences {

	/// <summary>
	/// Doesn't do anything special other than hold action sequences
	/// </summary>

	public class ActionSequenceCollection : DataCollection {

		[SerializeField] ActionSequence [] _actionSequences;


		/// <summary>
		/// Gets the collection.
		/// </summary>
		/// <value>The collection.</value>
		
		public override IKeyed [] Collection { get { return (IKeyed[])_actionSequences as IKeyed[]; } } 
	}
}