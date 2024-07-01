using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColouredTile : MonoBehaviour
{
    public Color mycolour;
    private IEnumerator OnTriggerEnter(Collider other) {
        if(other.GetComponentInChildren<Renderer>().material.color != mycolour) { // deactivates player cube if it doesn't match the tile's colour
            other.gameObject.SetActive(false);
            GameObject go = Instantiate(other.GetComponent<CubeRoll>().deatheffect, other.transform.position, other.transform.rotation);
            go.GetComponent<ParticleSystem>().startColor = other.GetComponentInChildren<Renderer>().material.color; // set colour of particle effect
            Destroy(go,1f);
            AudioManager.instance.PlaySFX(AudioManager.instance.deathsound); // play sound.
            yield return new WaitForSeconds(1);
            other.gameObject.SetActive(true);
            other.transform.position = other.GetComponent<CubeRoll>().respawnPosition + new Vector3(0, other.GetComponent<CubeRoll>().respawnPosition.y + 10, 0); // resets cube to 10 units above respawn position on the y-axis
        }
    }
}
