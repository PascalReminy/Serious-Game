using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WasteRespawnScript : MonoBehaviour {


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
		if(Random.Range(0,1) == 0)
		{
			int randomWasteIndex = Random.Range(0, this.wasteList.Count);
			Vector3 spawnPosition = this.transform.position + new Vector3(0,0,-4);
			GameObject waste = GameObject.Instantiate(this.wasteList[randomWasteIndex], spawnPosition, Quaternion.identity) as GameObject;
		}
	}
}
