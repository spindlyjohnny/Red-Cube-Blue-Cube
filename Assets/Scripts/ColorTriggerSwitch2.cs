using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTriggerSwitch2 : MonoBehaviour {
	public GameObject colorTile;
	public static bool colorStatus = false;

    private void Start() {
		colorStatus = false;
    }
    void OnTriggerEnter(Collider collider) {
		colorStatus = !colorStatus; // makes colourstatus the opposite of its current value in order to switch colours
	}

	
}