using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]private AudioSource audio,sfxaudio;
    public AudioClip BGM,flopsound,levelclearsound,doorunlocksound,locksound,teleportsound,deathsound,tileunlocksound,winsound;
    // Start is called before the first frame update
    private void Awake() {
        // create singleton.
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
    }
    public void StopMusic() {
        audio.Stop();
    }
    public void PlayMusic(AudioClip clip) {
        if (audio.isPlaying) return;
        audio.clip = clip;
        audio.Play();
    }
    public void PauseMusic() {
        audio.Pause();
    }
    public void ResumeMusic() {
        audio.UnPause();
    }
    public void PlaySFX(AudioClip clip) {
        sfxaudio.PlayOneShot(clip);
    }
}
