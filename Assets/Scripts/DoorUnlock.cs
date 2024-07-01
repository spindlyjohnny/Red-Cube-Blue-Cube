using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public GameObject[] doors; // doors to unlock
    CameraFollow cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<CubeRoll>()) {
            foreach(GameObject i in doors) {
                i.SetActive(false); // sets doors to inactive when a player cube touches tile
                AudioManager.instance.PlaySFX(AudioManager.instance.doorunlocksound);
                cam.target = i.gameObject; // camera pans to open doors
            }
        }
    }
}
