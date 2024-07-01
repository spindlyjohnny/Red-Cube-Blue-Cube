using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUnlock : MonoBehaviour
{
    public GameObject tilestounlock;
    public GameObject tilestolock;
    CameraFollow cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraFollow>();
    }
    private IEnumerator OnTriggerEnter(Collider other) {
        if (other.GetComponent<CubeRoll>()) {
            if(!tilestounlock.activeSelf || tilestolock.activeSelf)other.GetComponent<CubeRoll>().canMove = false; // disable movement while tiles are getting locked/unlocked.
            if(tilestounlock != null && !tilestounlock.activeSelf) { // checks for activeself so that camera doesn't pan everytime player touches tile.
                tilestounlock.SetActive(true);
                AudioManager.instance.PlaySFX(AudioManager.instance.tileunlocksound);
                cam.target = tilestounlock; // pan camera to tiles
            }
            yield return new WaitForSeconds(1);
            if (tilestolock != null && tilestolock.activeSelf) {
                tilestolock.SetActive(false);
                AudioManager.instance.PlaySFX(AudioManager.instance.locksound);
                cam.target = tilestolock;
            }
            yield return new WaitForSeconds(.1f);
            other.GetComponent<CubeRoll>().canMove = true;
        }
    }
}
