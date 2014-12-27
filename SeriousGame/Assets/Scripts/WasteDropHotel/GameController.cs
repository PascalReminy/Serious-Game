using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public LevelMakerManagerScript levelMakerManager;
	public List<GameObject> _wasteRespawnPointList;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(this._wasteRespawnPointList.Count < 1)
			this._wasteRespawnPointList = new List<GameObject>(levelMakerManager.GetRaspawnerList());
	}
}
