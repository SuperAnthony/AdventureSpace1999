// TODO: December 15th 2014

// TODO: This is messy, cleanup

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin;
using WhatPumpkin.Screens;
using WhatPumpkin.Actions;
using WhatPumpkin.Actions.Sequences;
using UnityEngine.UI;

namespace WhatPumpkin.Actions.UI {

	public class VerbCoinPanel : GameScreen {

		// TODO: There should only be one instance of this

		/// <summary>
		/// Gets a value indicating is open.
		/// </summary>
		/// <value><c>true</c> if is open; otherwise, <c>false</c>.</value>

		static public bool IsOpen { get; private set; }  

		/// <summary>
		/// The _verb coins.
		/// </summary>

		[SerializeField] VerbCoin[] _verbCoins = new VerbCoin[6];

		/// <summary>
		/// The number of verb coin rows
		/// </summary>

		[SerializeField] int _rows = 3;

		/// <summary>
		/// The number of verb coin columns
		/// </summary>
		
		[SerializeField] int _columns = 2;

		/// <summary>
		/// The is hovering verb coin.
		/// </summary>

		bool _isHoveringVerbCoin = false;

		bool _wasActivatedThisFrame = false;


		/// <summary>
		/// The _verb coin offset.
		/// </summary>

		[SerializeField] Vector3 _verbCoinOffset = new Vector3 (0,0,0);

		/// <summary>
		/// The _width.
		/// </summary>

		float _width = 475; 

		/// <summary>
		/// The _height.
		/// </summary>

		float _height = 100; 

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>The width.</value>

		float Width { get { return _width; } }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>The height.</value>

		float Height { get { return _height; } }

		/// <summary>
		/// Gets or sets the verb coin offset.
		/// </summary>
		/// <value>The verb coin offset.</value>

		public Vector3 VerbCoinOffset {
		
			get { return _verbCoinOffset;}

			set { _verbCoinOffset = value;}

		}

		public void OnCursorEnter() {

			_isHoveringVerbCoin = true;

		}

		public void OnCursorExit() {

			_isHoveringVerbCoin = false;
			
		}


		public override void Close() {

//			Debug.Log ("Close");

//			 Debug.Log ("Close Panel VC Panel");
			// Deactivate the panel
			// Get the switch component that's attached

			Switch swtch = this.GetComponent<Switch>();
			if(swtch != null) {
				// Activate the panel
				swtch.Deactivate ();
			}
			else {
				// Log error
				Debug.LogError("Cannot find the switch component attached to the Verb Coin Panel.");
			}

			IsOpen = false;

		}

		void Update() {

			if (Input.GetMouseButtonUp (0) 
			    && !_isHoveringVerbCoin && !_wasActivatedThisFrame
			    ) 
			{
				Close ();
			}

			_wasActivatedThisFrame = false;

		}

		/// <summary>
		/// Clears all verb coins.
		/// </summary>

		void ClearAllVerbCoins() {
		
			// Search through each verb coin in the panel and clear it
			foreach (VerbCoin verbCoin in _verbCoins) {
				verbCoin.Clear();
			}

		}


		void ShowVerbCoinImages() {
		
			foreach (VerbCoin verbcoin in _verbCoins) {
			
				verbcoin.Show();
			
			}

		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {

//			Debug.Log ("Activate");
			_wasActivatedThisFrame = true;
		
			// Activate the panel
			// Get the switch component that's attached
			Switch swtch = this.GetComponent<Switch>();
			if(swtch != null) {
				// Activate the panel
				swtch.Activate ();
			}
			else {
				// Log error
				Debug.LogError("Cannot find the switch component attached to the Verb Coin Panel.");
			}

		}

		/// <summary>
		/// Keeps the in bounds.
		/// </summary>

		void KeepInBounds() {

			_height =  _verbCoins[0].GetComponent<RectTransform> ().rect.height * _rows;
			_width  =  _verbCoins[0].GetComponent<RectTransform> ().rect.width * _columns;

			if ((this.transform.position.x - (Width * .5)) < 0) {
				this.transform.position = new Vector3((Width * .5F), this.transform.position.y); 			
			}

			if ((this.transform.position.x + (Width * .5)) > Screen.width) {
				this.transform.position = new Vector3(Screen.width - (Width * .5F), this.transform.position.y); 			
			}

			if ((this.transform.position.y - (Height * .5)) < 0) {
				this.transform.position = new Vector3(this.transform.position.x, (Height * .5F)); 			
			}
			
			if ((this.transform.position.y + (Height * .5)) > Screen.height) {
				this.transform.position = new Vector3(this.transform.position.y, Screen.height - (Height * .5F)); 			
			}

			


		}

		/// <summary>
		/// Assigns the action sequences.
		/// </summary>
		/// <param name="vActionSequences">V action sequences.</param>

		void AssignActionSequences(IList<VerbActionSequence> vActionSequences) {

			// Search through each sequence	
			for (int i = 0; i < vActionSequences.Count; i++) {
				
				// Set the verb coins
				if(i < _verbCoins.Length) {
					// Display the action sequence if it's required conditions are met
					
					VerbActionSequence verbAS = (VerbActionSequence)vActionSequences[i]; 
					
					if(verbAS != null && verbAS.ConditionsAreMet()){
						_verbCoins[i].SetActionSequence(verbAS);
					}
					else {
						_verbCoins[i].Hide();
					}
					
				}
				
			}

		}


		/// <summary>
		/// Activate the specified transform.
		/// </summary>
		/// <param name="transform">Transform.</param>

		public void OpenPanel(Transform transform, IList<VerbActionSequence> vActionSequences) {

			// TODO: Implement open interface
			// TODO: It appears that the transform doesn't get used at all

			// Clear the verb coins in the verb coin panel to
			// be certain that none of the verb coins maintain old/incorrect data
			ClearAllVerbCoins ();

			// Set all verbcoins to display
			ShowVerbCoinImages ();

			// Activate the verb coin
			Activate ();
	

			// Set the position
			this.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y) + _verbCoinOffset;

			// Make sure the verb coin stays within it's bounds
			KeepInBounds ();

			// Attach the verbs coins to the assocaited action in the action sequence
			AssignActionSequences (vActionSequences);

			// Open
			IsOpen = true;
			
		}

		
	}


}
