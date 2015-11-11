using System;
using UnityEngine;
using System.Collections;
using WhatPumpkin.FX;

using WhatPumpkin;

public class ProtoFadeScript : MonoBehaviour {

	// Public serializable fields (shows in inspector)

	float spriteAlpha = 1F;
	
	public float lerpSpeed = 0F;

	SpriteRenderer spriteRenderer;

	public bool fadeOut = false;

	bool isTransitioning = false;

	/// <summary>
	/// Occurs when _faded to black.
	/// </summary>

	static public event System.Action fadedToBlack;

	// Use this for initialization
	void Start () {
	
		EventManager.ApplicationClose += HandleApplicationClose;
		fadedToBlack += CameraTransitions.OnFadeToBlackComplete;

		if(fadeOut){
			spriteAlpha = 1F;
		}
		else{
			spriteAlpha = 0F;
		}

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color (0,0,0,spriteAlpha);

	
	}

	void HandleApplicationClose ()
	{
		// Unregister events
		fadedToBlack -= CameraTransitions.OnFadeToBlackComplete;
		EventManager.ApplicationClose -= HandleApplicationClose;
	}
	
	// Update is called once per frame
	void Update () {

		
		if (isTransitioning == true) {

				if (fadeOut == false) {
						FadeIn ();
				} else {
						FadeOut ();
				}
		}
	
	}

	public void StartTransition(bool _fadeOut) {

		fadeOut = _fadeOut;

		isTransitioning = true; 

	}

	public void StopTransition() {
	
		isTransitioning = false;
	}



	void OnFadedToBlackCompleted() {
	
		Debug.Log ("OnFadedToBlackCompleted");


		if (fadedToBlack != null) {
		
			Debug.Log ("Faded to black is not null");
			fadedToBlack.Invoke();

		}

		//StopTransition ();

	}

	//From scene to black
	//alpha needs to be 0
	void FadeIn(){

		//Debug.Log ("Fade In");

		if(spriteRenderer.color.a <= 1){
			spriteAlpha = spriteRenderer.color.a;
			//spriteRenderer.color.a -= Time.deltaTime * lerpSpeed;
			spriteAlpha += Time.deltaTime * lerpSpeed;
			spriteRenderer.color = new Color (0,0,0,spriteAlpha);

			if(spriteRenderer.color.a >= 1) {
			
				OnFadedToBlackCompleted();
			}

		}

	}

	void OnFadeFromBlackCompleted() {
		StopTransition ();
	}

	//From black to scene
	//alpha needs to be 0
	void FadeOut(){

//		Debug.Log ("Fade Out");

		// Lerping here:	
		if(spriteRenderer.color.a >= 0){
			spriteAlpha = spriteRenderer.color.a;
			spriteAlpha -= Time.deltaTime * lerpSpeed;
			spriteRenderer.color = new Color (0,0,0,spriteAlpha);

			if(spriteRenderer.color.a <= 0) {
				
				OnFadeFromBlackCompleted();
			}
		}


	}
}
