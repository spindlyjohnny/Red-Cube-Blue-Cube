using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoColorSwitcher1 : MonoBehaviour {
	private bool state = false;
	public Color red, blue;
	void Start() {
		StartCoroutine(ChangeColor());
	}

	IEnumerator ChangeColor() {
		while(true) {
			yield return new WaitForSeconds(1f); // Delay for 1 second
			if(state) {
				RedColor();
				state = false;
			}
			else {
				BlueColor();
				state = true;
			}
		}
	}

	void RedColor() {
		GetComponent<Renderer>().material.color = red;
	}

	void BlueColor() {
		GetComponent<Renderer>().material.color = blue;
	}
}
