using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]

public class TitleMenu : MonoBehaviour {
	public AudioClip titlemusic,levelmusic;
    private void Start() {
		if (!AudioManager.instance.GetComponent<AudioSource>().isPlaying) StartCoroutine(SwitchMusic(titlemusic)); // sets music if there is no music playing.
    }
    public void PlayGame() {
		SceneManager.LoadScene("Level1");
		StartCoroutine(SwitchMusic(levelmusic));
	}
	public void Instructions() {
		SceneManager.LoadScene("Instructions");
	}
	public void Credits() {
		SceneManager.LoadScene("Credits");
	}
    public void QuitGame() {
		Application.Quit();
	}
	public void Title() {
		SceneManager.LoadScene("Title");
	}
	public IEnumerator SwitchMusic(AudioClip music) {
		AudioManager.instance.StopMusic();
		yield return new WaitForEndOfFrame();
		AudioManager.instance.BGM = music;
		AudioManager.instance.PlayMusic(AudioManager.instance.BGM);
	}
}