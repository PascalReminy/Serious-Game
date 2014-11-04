using UnityEngine;
using System.Collections;

public class DesactiveObj : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
