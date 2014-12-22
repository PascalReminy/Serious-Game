using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WasteRespawnScript : MonoBehaviour {

	public GameObject dumpsterCaroussel;
	public GameObject paperDumpster;
	public GameObject plasticDumpster;
	public GameObject glassDumpster;
/*	public GameObject[] paperWaste = new GameObject[2];
	public GameObject[] plasticWaste = new GameObject[2];
	public GameObject[] glassWaste = new GameObject[2];*/
	public List<GameObject> wasteList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("Score", 12358);
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	public void DropWaste()
	{
		var randomWasteIndex = Random.Range(0, this.wasteList.Count);
		
	}
}
