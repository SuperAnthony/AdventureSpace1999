using UnityEngine;
using System.Collections;
using WhatPumpkin.ScriptingLanguage;

namespace WhatPumpkin {


	public class Shake : MonoBehaviour, ISwitchable {

		/// <summary>
		/// Are there conditions that affect this shake
		/// </summary>

		[SerializeField] string _conditions = ""; 

		/// <summary>
		// TODO: Rename
		/// </summary>
		
		[SerializeField] bool _activateWhenNodeAwakes = true; 

	
		/// <summary>
		/// The max strength on the local axis.
		/// </summary>

		[SerializeField] Vector3 _maxStrength = new Vector3(0F,0F,0F);

		/// <summary>
		/// The min strength on the local axis.
		/// </summary>

//		[SerializeField] Vector3 _minStrength = new Vector3(0F,0F,0F);

		/// <summary>
		/// The _duration.
		/// </summary>

		[SerializeField] float _duration = 20F;

		/// <summary>
		/// The time elapsed since the effect was activated.
		/// </summary>

		float _timeElapsed = 0F;

		/// <summary>
		/// The _original local position.
		/// </summary>

		Vector3 _originalLocalPosition;


		/// <summary>
		/// The attached camera node.
		/// </summary>

		ICameraNode _attachedCameraNode;


		#region ISwitchable implementation

		/// <summary>
		/// Is this effect active
		/// </summary>

		bool _isActive = false;

		/// <summary>
		/// Activate this instance.
		/// </summary>


		public void Activate() {
		
		
			Reset ();
			_isActive = true;
		
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public void Deactivate() {
		
			_isActive = false;
		}

		public bool IsActive { get { return _isActive; } }

		#endregion

		#region CameraMotion Implementation

		// TODO: Inheret from new type CameraMotionFX

		void Start() {
			
			GameController.CamManager.SwitchCameraNode += HandleSwitchCameraNode;
			_attachedCameraNode = this.GetComponent<ICameraNode> ();
			_originalLocalPosition = this.transform.localPosition;

			_isActive = _activateWhenNodeAwakes; // TODO: What the fuck kind of naming is this Anthony?
			Reset ();
		}
		
		void Update() {
			// Update the effect when active
			UpdateEffect ();
		}


		// TODO Abstract Method, override 

		void UpdateEffect() {

			// Reset to original position
			this.transform.localPosition = _originalLocalPosition;

			//Debug.Log ("Update Effect");

				// Update the time elapsed
				_timeElapsed += Time.deltaTime;

				// Check to see if the effect is complete
				if(_timeElapsed >= _duration) {
					Deactivate();
				}


			this.transform.localPosition = GenerateNewLocalPosition ();
			
		}

		/// <summary>
		/// Reset this instance.
		/// </summary>

		void Reset() {
			_timeElapsed = 0F;

			_originalLocalPosition = transform.localPosition;
		}

		/// <summary>
		/// Generates the new position.
		/// </summary>
		/// <returns>The new position.</returns>

		Vector3 GenerateNewLocalPosition() {
			/*

			float x = Random.Range (_maxStrength.x, _minStrength.x);
			float y = Random.Range (_maxStrength.y, _minStrength.y);
			float z = Random.Range (_maxStrength.z, _minStrength.z);
		*/
			return new Vector3(this.transform.localPosition.x + _maxStrength.x * Time.deltaTime,
			                   this.transform.localPosition.y + _maxStrength.y * Time.deltaTime,
			                   this.transform.localPosition.z + _maxStrength.z * Time.deltaTime);

		}

		/// <summary>
		/// Handles the switch camera node.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleSwitchCameraNode (object sender, WhatPumpkin.CameraManagement.CamSwitchArgs e)
		{

			if (_attachedCameraNode == e.NewActiveCamera && Scripting.AreConditionsMet(_conditions)) {
			
				// If the camera node attached was activated and conditions are met then activate this effect
				Activate();
			}
			else {
				// Otherwise, deactivate the effect if already active (Not that the user will notice the differene)
				if(_isActive){Deactivate();}
			}
		}

		#endregion


	}
}