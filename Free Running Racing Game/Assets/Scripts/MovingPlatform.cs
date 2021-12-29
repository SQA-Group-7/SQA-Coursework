using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.transform.parent = transform;
    }

    private void onTriggerExit(Collider other) {
        other.transform.parent = null;
    }
}
