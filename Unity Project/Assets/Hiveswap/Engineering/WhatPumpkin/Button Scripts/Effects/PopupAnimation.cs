using UnityEngine;
using System.Collections;

public class PopupAnimation : MonoBehaviour {
	private Animator popup;

	void Awake (){
		popup = gameObject.GetComponent<Animator>();

	}

	void Start (){
		popup.StartPlayback();
		popup.playbackTime = 3f;
	}
}
