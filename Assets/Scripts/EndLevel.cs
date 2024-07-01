using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public string leveltoload;
    // Update is called once per frame
    void Update()
    {
        // check if both cubes are on the tiles
        var cubes = Physics.OverlapBox(transform.position + new Vector3(.5f, 1, 0), (transform.localScale + new Vector3(transform.localScale.x, 0, 0)) * .25f, Quaternion.identity, LayerMask.GetMask("Player"));
        if (cubes.Length == 2) {
            if (leveltoload == "Win") {
                SceneManager.LoadScene(leveltoload);
                AudioManager.instance.StopMusic();
                AudioManager.instance.PlaySFX(AudioManager.instance.winsound);
            } 
            else {
                StartCoroutine(PlaySound());
                SceneManager.LoadScene(leveltoload);
            }
        }
    }
    IEnumerator PlaySound() {
        // play level clear sound.
        AudioManager.instance.PlaySFX(AudioManager.instance.levelclearsound);
        yield return new WaitForSeconds(AudioManager.instance.levelclearsound.length);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + new Vector3(.5f, 1, 0), (transform.localScale + new Vector3(transform.localScale.x, 0, 0))*.5f);
    }
}
