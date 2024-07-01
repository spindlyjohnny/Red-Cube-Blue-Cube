using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	public bool cube;
	public GameObject othertile;
	void OnTriggerEnter(Collider collider) {
		if (collider.GetComponent<CubeRoll>() && !othertile.GetComponent<GameEndTriggerSwitchN>().cube) { 
			// checks if player cube is on tile and if the other end tile isnt touching a player cube so that one cube cannot make 'cube' true for both end tiles
			cube = true;
        }
	}
}