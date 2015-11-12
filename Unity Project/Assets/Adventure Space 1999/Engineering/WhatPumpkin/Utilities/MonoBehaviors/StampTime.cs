using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StampTime : MonoBehaviour {

	[SerializeField] Text _text;

	public void Activate() {	
		if (_text != null) {
						_text.text = DateTime.Now.ToString ();
				}
	}


}
