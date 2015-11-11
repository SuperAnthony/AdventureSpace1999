using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayUIVideo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		RawImage image = GetComponent<RawImage> ();

		if (image != null) {
		
			MovieTexture movieTexture = (MovieTexture)image.mainTexture;

			if(movieTexture != null) {
				movieTexture.Play();
			}
		
		}

	}
	

}
