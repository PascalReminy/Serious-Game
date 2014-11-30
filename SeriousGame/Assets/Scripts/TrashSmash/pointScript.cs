using UnityEngine;
using System.Collections;

public class pointScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!animation.isPlaying)
            this.gameObject.SetActive(false);
	
	}
}
