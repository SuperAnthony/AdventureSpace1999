#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 13, 2015
#endregion 

#region using
using UnityEngine;
using System;
using WhatPumpkin.Actions.UI;
#endregion

namespace WhatPumpkin.Sgrid.Triggers {


	public class HotSpotController : MonoBehaviour, IHotSpotController {

		#region static fields

		/// <summary>
		/// The singleton instance.
		/// </summary>

		static HotSpotController _instance;

		#endregion

		#region fields

		/// <summary>
		/// The required number of sequences.
		/// </summary>

		[SerializeField] int _requiredSequences = 6;

		/// <summary>
		/// The default rollover messages when the mouse is not hovering over a hotspot
		/// </summary>
		
		[SerializeField] string _defaultRolloverMessage = "Sylladex :: Captchalogue Deck";

		/// <summary>
		/// The scene hot spots.
		/// </summary>
		
		IHotSpot [] _hotSpots;

		/// <summary>
		/// Gets a value indicating whether or not the user is hovering over a hotspot
		/// </summary>


		public bool IsHoveringOverHotSpot { get { return _lastHotspotEntered != null;} }

		/// <summary>
		/// Gets the hotspot that the user is hovering over.
		/// </summary>
		/// <value>The hovering hotspot.</value>

		public IHotSpot HoveringHotspot { get { return (IHotSpot)_lastHotspotEntered; } }

		// For raycasting with hotspots
		
		Ray ray;

		RaycastHit hit;
	
		#endregion

		#region properties

		/// <summary>
		/// Gets the required number sequences.
		/// </summary>
		/// <value>The required number of sequences.</value>

		public int RequiredSequences { get { return _requiredSequences; } }

		/// <summary>
		/// Gets the default rollover message.
		/// </summary>
		/// <value>The default rollover message.</value>

		public string DefaultRolloverMessage { get { return _defaultRolloverMessage; } }

		#endregion


		#region methods



		/// <summary>
		/// Occurs on start
		/// </summary>

		void Start () {
			// Set this to be the hotspot controller
			HotSpot.Controller = this;

		}

		LayerMask layermask = 1 << 0; // Focus on only the Default layer for raycasting hotspots

		HotSpot _lastHotspotEntered = null;
		//HotSpot _mousedDownHotspot = null;
		//HotSpot _mousedUpHotspot = null;

		void Update() {

			if (Camera.main == null) {return;}

			bool hoveringHotSpot = false;


				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layermask)) {
			
						if (hit.collider != null) {

							
				
								HotSpot hotSpot = hit.collider.GetComponent<HotSpot> ();
								
								// Did we enter a hotspot && and is it a valid hotspot
								if (hotSpot != null) {
					
									// If so, then we are definitely hovering over a hotspot
									hoveringHotSpot = true;	

									// Are we still hovering over the same hotspot?
									
									if(_lastHotspotEntered != hotSpot) {

										//Debug.Log ("Enter Hot Spot: " + hit.collider.name);

										// If not then change the last hot spot entered and let the new hot spot know that we entered it
										_lastHotspotEntered = hotSpot;
										hotSpot.MouseEnter();
									}
					
								}
				
						}
						else {
							// If the collider is null then we are not hovering over a hotspot
							hoveringHotSpot = false;
						}
				}
				

			// Handle hotspot hover and click
			if (hoveringHotSpot) {

				// On Mouse Down
				if (Input.GetMouseButtonDown (0)) {
					_lastHotspotEntered.MouseDown();
				}

				// On Mosue Up
				if (Input.GetMouseButtonUp(0)) {			
					_lastHotspotEntered.MouseUp();
				}
			}

			//Debug.Log ("Hovering Over Hotspot? " + hoveringHotSpot);

			// Did we exit a hotspot
			if(!hoveringHotSpot && _lastHotspotEntered != null) {
		
				_lastHotspotEntered.MouseExit();
				_lastHotspotEntered = null;

			}
		}
	

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="WhatPumpkin.Sgrid.Triggers.HotSpotController"/> are hot
		/// spots active.
		/// </summary>
		/// <value><c>true</c> if are hot spots active; otherwise, <c>false</c>.</value>

		public virtual bool AreHotSpotsActive { get 

			{
					return true;

					//!GameController.MessageManager.IsShowingNarratorMessage &&
					
					//!VerbCoinPanel.IsOpen;

			}
		}

		/// <summary>
		/// Are the hot spot conditions met?
		/// </summary>
		/// <returns><c>true</c>, if hot spot conditions are met, <c>false</c> otherwise.</returns>
		/// <param name="hotspot">Hotspot.</param>

		public virtual bool AreHotSpotConditionsMet(IHotSpot hotspot) {
			return 
				//HasRequiredItems((HotSpot)hotspot) &&
				MeetsCameraConditions((HotSpot)hotspot);
		}

		/// <summary>
		/// Determines whether this instance has required items of the specified hotSpot.
		/// </summary>
		/// <returns><c>true</c> if this instance has required items the specified hotSpot; otherwise, <c>false</c>.</returns>
		/// <param name="hotSpot">Hot spot.</param>
		/*
		bool HasRequiredItems(IHotSpot hotSpot) {

			return (GameController.PartyManager.ActivePC != null && GameController.PartyManager.ActivePC.Inventory.HasItems (hotSpot.RequiredItems));
		
		}*/

		/// <summary>
		/// Determines whether this instance meets the camera conditions of the specified hotSpot.
		/// </summary>
		/// <returns><c>true</c>, if camera conditions were met, <c>false</c> otherwise.</returns>
		/// <param name="hotSpot">Hot spot.</param>



		bool MeetsCameraConditions(IHotSpot hotSpot) {
		
			return  (!GameController.CamManager.ActiveCameraNode.IsCloseupCam && hotSpot.HasRequiredCam) || 
				    hotSpot.RequiredCamName == GameController.CamManager.ActiveCameraNode.gameObject.name ;

		}

		/// <summary>
		/// Disables the room hot spots.
		/// </summary>
		
		public void DisableRoomHotSpots() {
			
			_hotSpots = GameObject.FindObjectsOfType<HotSpot> ();
			
			foreach (IHotSpot hotSpot in _hotSpots) {
				if(hotSpot.HasRequiredCam) {hotSpot.Disable();}
			}
			
		}

		/// <summary>
		/// Enales the room hot spots.
		/// </summary>
		
		public void EnaleRoomHotSpots() {
			
			foreach (IHotSpot hotSpot in _hotSpots) {
				
				hotSpot.Enable();
				
			}
			
		}

		#endregion
	
	}
}
