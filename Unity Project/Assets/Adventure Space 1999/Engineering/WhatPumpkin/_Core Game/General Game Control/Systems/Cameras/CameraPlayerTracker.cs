#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 7, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// TODO: Summary
	/// </summary>

	[RequireComponent(typeof(CameraPathAnimator))]
	[RequireComponent(typeof(CameraPath))]

	public class CameraPlayerTracker : MonoBehaviour {

		enum AXIS {X,Y,Z};

		/// <summary>
		/// The camera path animator component.
		/// </summary>

		CameraPathAnimator _cameraPathAnimator;

		/// <summary>
		/// The camera path component
		/// </summary>

		CameraPath _cameraPath;

		/// <summary>
		/// The relative axis that is used to determine the percentage used.
		/// </summary>

		[SerializeField] AXIS _relativeAxis = AXIS.X;

		/// <summary>
		/// The percent offset at any given moment.
		/// </summary>

		[SerializeField] float _percentOffset = 0F;

		/// <summary>
		/// The _invert.
		/// </summary>

		[SerializeField] bool _invert = false;

		/// <summary>
		/// The relative point.
		/// </summary>

		[SerializeField] Transform point1;

		[SerializeField] Transform point2;

		/// <summary>
		/// The _path distance.
		/// </summary>

		float _pathDistance;

	

		void Awake() {
		
			_cameraPathAnimator = this.GetComponent<CameraPathAnimator> ();
			_cameraPath = this.GetComponent<CameraPath>();
			_pathDistance = GetPathDistance();

			_cameraPathAnimator.Play ();
		}

		/// <summary>
		/// Gets the active player distance from start point of the camera rail.
		/// </summary>
		/// <returns>The player distance from start point.</returns>

		float GetPlayerDistanceFromStartPoint() {
		
			float playerDistance = 0F;

			// Get the distance between the first and last point

			try {

				if(_relativeAxis == AXIS.X) {
					if(_invert) {
						playerDistance  =   point1.position.x - GameController.PartyManager.ActivePC.transform.position.x;
					}
					else { 
						try {
							playerDistance  =  GameController.PartyManager.ActivePC.transform.position.x - point1.position.x;
						}
						catch {
							return 0F;
						}
					}

				}
				else if(_relativeAxis == AXIS.Y){

					if(_invert) {
						playerDistance  =   GameController.PartyManager.ActivePC.transform.position.y - point1.position.y;
					}
					else {
						try {
							playerDistance  =   point1.position.y - GameController.PartyManager.ActivePC.transform.position.y;
						}
						catch {
							return 0F;
						}
					}

				}
				else if(_relativeAxis == AXIS.Z) {
					if(_invert) {
						playerDistance  =   point1.position.z - GameController.PartyManager.ActivePC.transform.position.z;
					}
					else {
						try {
							playerDistance  =   GameController.PartyManager.ActivePC.transform.position.z - point1.position.z;
						}
						catch {
							return 0F;
						}
					}
				}
			}
			catch (System.Exception e) {
			
				Debug.LogException(e);

			}

			return playerDistance;
		
		}

		/// <summary>
		/// Gets the rail percentage.
		/// </summary>
		/// <returns>The rail percentage.</returns>

		float GetRailPercentage() {

			// Apply the offset
			float basePerentage = GetPlayerDistanceFromStartPoint () / _pathDistance;

//			Debug.Log ("Base Percentage: " + basePerentage);
	
			float offsetedPercent = basePerentage + _percentOffset;

			//Debug.Log ("Offeseted Percent: " + offsetedPercent);

			// Prevent a percentage that is less than 0 or greater than 1
			if(offsetedPercent < 0) {return 0F;}
			if(offsetedPercent > 1) {return 1F;}

			// Return the rail percent
			return offsetedPercent;
		
		}


		// Update is called once per frame
		void Update () {

			if (!_cameraPathAnimator.isPlaying) {
				_cameraPathAnimator.Play ();
			}

		//	Debug.Log ("Path Distance: " + _pathDistance);

			_cameraPathAnimator.Play ();
			_cameraPathAnimator.Percentage = GetRailPercentage ();
			//Debug.Log ("Get Rail Percentage: " + GetRailPercentage ());

			/*
			float percentage = GetRailPercentage ();
			if (percentage < 1F) {
					Debug.Log ("Get Rail Percentage: " + percentage);
			}*/



		}


		/// <summary>
		/// Gets the path distance one axis, the axis specified by the user (developer).
		/// </summary>
		/// <returns>The path distance.</returns>

		float GetPathDistance() {

			if (_cameraPath == null) {
				Debug.LogError("Camera path not attached");
				return 0F;

			}

			float p1 = 0F;
			float p2 = 0F;

			// Get the distance between the first and last point
			if(_relativeAxis == AXIS.X) {

				try {
					p1 = point1.position.x;
					p2 = point2.position.x;
				}
				catch (UnassignedReferenceException e){
					Debug.Log ("Reference to unassigned points");
				}
			}
			else if(_relativeAxis == AXIS.Y)
			{
				try {
					p1 = point1.position.y;
					p2 = point2.position.y;
				}
				catch (UnassignedReferenceException e){
					Debug.Log ("Reference to unassigned points");
				}
			
			}
			else if(_relativeAxis == AXIS.Z) {

				try {
					p1 = point1.position.z;
					p2 = point2.position.z;
				}
				catch (UnassignedReferenceException e){
				
					Debug.Log ("Reference to unassigned points");

				}
			
			}

			return Mathf.Abs (p1 - p2);
		}

	}
}
