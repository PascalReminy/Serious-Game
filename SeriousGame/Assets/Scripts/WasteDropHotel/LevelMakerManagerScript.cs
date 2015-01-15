using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LevelDifficulty : int {
	VeryEasy = 0,
	Easy = 1,
	Normal = 2,
	Hard = 3,
	VeryHard = 4
}

public class LevelMakerManagerScript : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject windowWasteRespawnerPrefab;
	public GameObject hotelFacadePrefab;
	public GameObject hotelDoorPrefab;
	//public LevelDifficulty difficulty;

	public int[,] hotel;

	private GameObject _hotelGameObject;

	private float _flatScale = 2.0f;
	public int _hotelWidth;
	public int _hotelHeight;

	private GameStateWasteDropHotel GS;

	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
		this._flatScale = GS.HotelScale;
		this._hotelHeight = GS.HotelHeight;
		this._hotelWidth = GS.HotelWidth;
	}

	// Use this for initialization
	void Start () {
		hotel = new int[_hotelHeight, _hotelWidth];
		_hotelGameObject = new GameObject("Hotel");
		GenerateRandomHotel();
		InstantiateHotel();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void GenerateRandomHotel()
	{
		for(int floor = 0; floor < _hotelHeight; floor++)
		{
			var str = "";
			for(int flat = 0; flat < _hotelWidth; flat ++ )
			{

				hotel[floor, flat] = 1;
				//the bottom of the hotel
				if(floor < 2)
				{
					//add the door in the middle of the hotel
					//manage pair and impair width
					if((_hotelWidth%2 == 1) && flat == _hotelWidth/2)
					{
						hotel[floor, flat] = 2;
					}
					else
					{
						if((_hotelWidth%2 == 0) && (flat == (_hotelWidth-1)/2 || flat == (_hotelWidth-1)/2+1))
						{
							hotel[floor, flat] = 2;
						}
					}
				}

				str += hotel[floor, flat]+", ";
			}
			//Debug.Log(str);
		}
	}

	public void InstantiateHotel()
	{
		for(int floor = 0; floor < _hotelHeight; floor++)
		{
			var floorGameObject = new GameObject("floor"+floor);
			floorGameObject.transform.parent = _hotelGameObject.transform;

			for(int flat = 0; flat < _hotelWidth; flat ++ )
			{
				var pos = new Vector3( flat - _hotelWidth/2 , floor) * _flatScale;
				if(hotel[floor, flat] == 0 || hotel[floor, flat] == 1)	//Brick Wall
				{
					var wall = Instantiate(wallPrefab, pos, wallPrefab.transform.rotation) as GameObject;
					var wallScript = wall.GetComponent<ChangeWallMaterialScript>();

					wall.name = "flat"+flat;
					wall.transform.localScale = new Vector3(_flatScale, _flatScale, _flatScale);
					wall.transform.parent = floorGameObject.transform;

					if(floor > 1)//BrickWall
					{
						if(flat == 0)			
							wallScript.ChangeWallMaterial(WallType.LeftBrickWall);		//LeftBrickWall
						else if(flat == _hotelWidth-1)	
							wallScript.ChangeWallMaterial(WallType.RightBrickWall);		//RightBrickWall
						else
							wallScript.ChangeWallMaterial(WallType.BrickWall);			//BrickWall
					}
					else
					{
						if(floor < 2)//Wall
						{
							if(flat == 0)			
								wallScript.ChangeWallMaterial(WallType.LeftWall);		//LeftWall
							else if(flat == _hotelWidth-1)	
								wallScript.ChangeWallMaterial(WallType.RightWall);		//LeftWall
							else
								wallScript.ChangeWallMaterial(WallType.Wall);			//Wall
						}
					}

					if(hotel[floor, flat] == 1)	//Window Waste Respawner
					{
						var flatGameObject = Instantiate(windowWasteRespawnerPrefab, pos + new Vector3(0, 0, -1), Quaternion.identity) as GameObject;
						flatGameObject.name = "flat"+flat;
						flatGameObject.transform.localScale = new Vector3(_flatScale, _flatScale, _flatScale)*0.75f;
						flatGameObject.transform.parent = floorGameObject.transform;
						GS.WastesRespawnerList.Add(flatGameObject);
					}
				}
				else
				{
					if(hotel[floor, flat] == 2)	//Door
					{
						if(floor == 0)
						{
							var flatGameObject = Instantiate(hotelDoorPrefab, pos, Quaternion.identity) as GameObject;
							flatGameObject.name = "flat"+flat;
							flatGameObject.transform.localScale = new Vector3(_flatScale, _flatScale, _flatScale);
							flatGameObject.transform.parent = floorGameObject.transform;
						}
						else
						{
							if(floor == 1)
							{
								var flatGameObject = Instantiate(hotelFacadePrefab, pos, Quaternion.identity) as GameObject;
								flatGameObject.name = "flat"+flat;
								flatGameObject.transform.localScale = new Vector3(_flatScale, _flatScale, _flatScale);
								flatGameObject.transform.parent = floorGameObject.transform;
							}
						}
					}
				}
			}
		}
	}
}
