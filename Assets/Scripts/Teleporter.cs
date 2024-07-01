using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField] Transform destination; // destination teleporter
    public Vector3 Teleport() { // allows CubeRoll to access destination variable's position
        return destination.position;
    }
}
