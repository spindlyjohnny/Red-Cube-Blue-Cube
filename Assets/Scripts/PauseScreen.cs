using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pausescreen;
    CubeRoll[] cubes;
    public AudioClip levelmusic;
    // Start is called before the first frame update
    void Start()
    {
        cubes = FindObjectsOfType<CubeRoll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (Time.timeScale == 0) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }
    public void PauseGame() {
        Time.timeScale = 0; // freeze game
        pausescreen.SetActive(true);
        foreach(CubeRoll cube in cubes) {
            cube.canMove = false; // disable cubes
        }
        AudioManager.instance.PauseMusic();
    }
    public void ResumeGame() {
        Time.timeScale = 1;
        pausescreen.SetActive(false);
        foreach (CubeRoll cube in cubes) {
            cube.canMove = true;
        }
        AudioManager.instance.ResumeMusic();
    }
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(SwitchMusic(levelmusic)); // resets music
        Time.timeScale = 1;
    }
    public void BackToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    public IEnumerator SwitchMusic(AudioClip music) {
        AudioManager.instance.StopMusic();
        yield return new WaitForEndOfFrame();
        AudioManager.instance.BGM = music;
        AudioManager.instance.PlayMusic(AudioManager.instance.BGM);
    }
}
