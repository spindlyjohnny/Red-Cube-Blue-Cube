using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour {
	public Color[] color;
	// Update is called once per frame
    void Start() {
		GetComponent<Renderer>().material.color = color[1]; // sets colour of cube to base colour at start of level.
	}
    void Update() {
		// check if cube is touching the colour switcher
		var hit = Physics.OverlapBox(transform.position + new Vector3(.1f, 0, 0), transform.localScale * .5f, Quaternion.identity, LayerMask.GetMask("Colour Switch"));
		if (ColorTriggerSwitch2.colorStatus) {
			if (hit.Length > 0)
				GetComponent<Renderer>().material.color = color[0]; // switches colour if colour switcher is detected.
		} 
		else if (!ColorTriggerSwitch2.colorStatus) {
			if (hit.Length > 0)
				GetComponent<Renderer>().material.color = color[1]; // base colour.
		}
	}
	private void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position + new Vector3(.1f, 0, 0), transform.localScale);
	}
}
