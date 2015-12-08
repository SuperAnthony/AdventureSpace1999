#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created -  January 29, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Actions.Sequences {

	/// <summary>
	/// Combine action sequence.
	/// </summary>

	[System.Serializable]

	public class CombineActionSequence : ActionSequence  {

		#region static members

		static List<CombineActionSequence> _instances = new List<CombineActionSequence>();

		static public List<CombineActionSequence> Instances { get { return _instances; } }

		#endregion

		#region fields

		ICombineItemController _combineItemController = null;

		/// <summary>
		/// The key of the first item to fullfill recipe 
		/// </summary>
		
		[SerializeField] string _item1;
		
		/// <summary>
		/// The key of the second item to fillfill recipe
		/// </summary>
		
		[SerializeField] string _item2;

		#endregion

		#region properties

		/// <summary>
		/// Get item 1.
		/// </summary>
		/// <value>The item1.</value>

		public string Item1 { 
			get {

				if(_item1 == null) {return "";}

				return _item1;
			} 
		
		}

		/// <summary>
		/// Get item 2.
		/// </summary>
		/// <value>The item2.</value>

		public string Item2 { 
			get {

				if(_item2 == null) {return "";}

				return _item2; 
			} 
		}

		#endregion

		#region methods


		public CombineActionSequence() {


			if (!_instances.Contains (this)) {
						// Subscribe to the CombineItemController
						_instances.Add (this);
				}

		
			//GameController.CombineItemController.Combine += HandleCombine; Unity says 'no'
	
		}

		public void SubscribeToController(ICombineItemController combineItemController) {


			if (_combineItemController == null) {

						_combineItemController = combineItemController;
						_combineItemController.Combine += HandleCombine;
				}
		
		}

		public bool HandleCombine (string itemKey1, string itemKey2)
		{
		

			// If these are the required items for a combination recipe to take effect then perform the necessary action sequence
			if (HasRequiredItems (itemKey1, itemKey2)) {
			//	Debug.Log ("Handling Combine: " + itemKey1 + " & " + itemKey2 + " & " + _item1 + " & " + _item2);
			//	Debug.Log ("Play Combine Sequence");
				Debug.Log ("Playing Combine Sequence: " + this.Key);
				this.Play();
				//GameController.InventoryManager.EndCombineMode(); // End Combine mode since combination was found | TODO: I made a mess - I am very unhappy with the way combine is working at the moment
				return true;

			}

			return false;
		}

		~CombineActionSequence() {
		
			_combineItemController.Combine -= HandleCombine;

		}

	
		#endregion

		public bool HasRequiredItems(string item1, string item2) {
		
			return  (_item1 != _item2) && 
					(_item1 == item1 || _item1 == item2) &&
					(_item2 == item1 || _item2 == item2);
		
		}

		public void SetProperties(string key, string conditions, string frequency, string item1, string item2) {
		
		
			_key = key;
			_conditions = conditions;
			_frequency = frequency;
			_item1 = item1;
			_item2 = item2;
		
		}
	}
}
