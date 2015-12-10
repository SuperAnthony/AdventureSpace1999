using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using WhatPumpkin;
using WhatPumpkin.Screens;
using WhatPumpkin.Sgrid;

namespace WhatPumpkin.Actions.UI {

	public class VerbCoin : MonoBehaviour {

		/// <summary>
		/// The verb text.
		/// </summary>

		[SerializeField] Text _verbText;

		/// <summary>
		/// The action.
		/// </summary>

		[SerializeField] VerbActionSequence _actionSequence;


		/// <summary>
		/// Sets the verb text.
		/// </summary>
		/// <value>The verb text.</value>

		public string VerbText { set { _verbText.text = value; } }

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		/// <summary>
		/// Clear this instance by removing the display text and the action sequence
		/// </summary>

		internal void Clear() {
			// Clear the text
			_verbText.text = "";
			// Clear the action sequence
			_actionSequence = null;
		}

		public void SetActionSequence(VerbActionSequence actionSequence) {
			_actionSequence = actionSequence;


			if(GameController.MessageManager != null) {
				_verbText.text = GameController.MessageManager.GetMessage(_actionSequence.VerbCoinText);
			}
			else {
				_verbText.text = _actionSequence.VerbCoinText;
			}

			// If the text is blank then hide
			if(_verbText.text == null || _verbText.text == ""){Hide();}

		}

		/// <summary>
		/// Plays the action sqeuence.
		/// </summary>
		
		public void PlayActionSequence() {

			// Close verb coin panel
			GameObject.FindObjectOfType<VerbCoinPanel> ().Close ();
			// Play the action sequence
			_actionSequence.Play ();

		}

		public void Show() {
		
			Image image = this.GetComponent<Image> ();
			if(image != null){image.enabled = true;}

		}

		public void Hide() {
			//print ("Hide");
			Image image = this.GetComponent<Image> ();
			if(image != null){image.enabled = false;}
		
		}

	}

}
