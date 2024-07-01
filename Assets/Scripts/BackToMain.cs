using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]

public class BackToMain : MonoBehaviour {
	public GUIStyle backToMain;

	void OnGUI() {
		// Buttons with custom style defined in GUI skin
		if(GUI.Button(new Rect((Screen.width - 182) * 0.5f + 350, (Screen.height - 64) * 0.5f + 250, 182, 64), "BACK TO MAIN MENU", backToMain))
			SceneManager.LoadScene("Title");
	}
}