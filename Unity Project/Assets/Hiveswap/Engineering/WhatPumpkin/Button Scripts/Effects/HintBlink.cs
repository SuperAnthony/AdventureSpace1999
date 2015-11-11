using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintBlink : MonoBehaviour {
	/*
	 * blinkColor parameters are RGBA value from 0 to 1 respectively. 
	 */
	public Vector4 baseColor = new Vector4(1f,1f,1f,1f);
	public Vector4 blinkColor = new Vector4(0.03f, 1f, 0.29f, 1f);
	private Button button;
	void Start() {
		button = gameObject.GetComponent<Button>();
	}
	/*
	 * TODO figure out why it is not lerping back to base color.
	 */
	void Update() {
		button.image.color = Color.Lerp(baseColor, blinkColor, 0.1f*Time.time);
	}
}