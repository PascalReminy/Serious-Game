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
	public LevelDifficulty difficulty;
	public int hotelWidth = 5;
	public int hotelHeight = 8;
	public int[,] hotel;

	private GameObject _hotelGameObject;

	private float _blockScale = 2.0f;

	private GameStateWasteDropHotel GS;

	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
		this._blockScale = GS.HotelScale;

	}

	// Use this for initialization
	void Start () {
		hotel = new int[hotelHeight, hotelWidth];
		_hotelGameObject = new GameObject("Hotel");
		GenerateRandomHotel();
		InstantiateHotel();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void GenerateRandomHotel()
	{
		for(int floor = hotelHeight-1; floor >= 0; floor--)
		{
			var str = "";
			for(int block = 0; block < hotelWidth; block ++ )
			{
				if(floor > 1 && floor < hotelHeight-1)
				{
					//middle of hotel, playground area
					hotel[floor, block] = Random.Range(1,2);
				}
				else
				{
					hotel[floor, block] = 1;
					//the top and the bottom of the hotel
					if(floor < 2)
					{
						//the door
						if((hotelWidth%2 == 1) && block == hotelWidth/2)
						{
							hotel[floor, block] = 2;
						}
						else
						{
							if((hotelWidth%2 == 0) && (block == (hotelWidth-1)/2 || block == (hotelWidth-1)/2+1))
							{
								hotel[floor, block] = 2;
							}
						}
					}
				}
				str += hotel[floor, block]+", ";
			}
			//Debug.Log(str);
		}
	}

	public void InstantiateHotel()
	{
		for(int floor = 0; floor < hotelHeight; floor++)
		{
			var floorGameObject = new GameObject("floor"+floor);
			floorGameObject.transform.parent = _hotelGameObject.transform;

			for(int block = 0; block < hotelWidth; block ++ )
			{
				var pos = new Vector3( block - hotelWidth/2 , floor) * _blockScale;
				if(hotel[floor, block] == 0 || hotel[floor, block] == 1)	//Brick Wall
				{
					var wall = Instantiate(wallPrefab, pos, wallPrefab.transform.rotation) as GameObject;
					var wallScript = wall.GetComponent<ChangeWallMaterialScript>();

					wall.name = "block"+block;
					wall.transform.localScale = new Vector3(_blockScale, _blockScale, _blockScale);
					wall.transform.parent = floorGameObject.transform;

					if(floor > 1)//BrickWall
					{
						if(block == 0)			
							wallScript.ChangeWallMaterial(WallType.LeftBrickWall);		//LeftBrickWall
						else if(block == hotelWidth-1)	
							wallScript.ChangeWallMaterial(WallType.RightBrickWall);		//RightBrickWall
						else
							wallScript.ChangeWallMaterial(WallType.BrickWall);			//BrickWall
					}
					else
					{
						if(floor < 2)//Wall
						{
							if(block == 0)			
								wallScript.ChangeWallMaterial(WallType.LeftWall);		//LeftWall
							else if(block == hotelWidth-1)	
								wallScript.ChangeWallMaterial(WallType.RightWall);		//LeftWall
							else
								wallScript.ChangeWallMaterial(WallType.Wall);			//Wall
						}
					}

					if(hotel[floor, block] == 1)	//Window Waste Respawner
					{
						var blockGameObject = Instantiate(windowWasteRespawnerPrefab, pos + new Vector3(0, 0, -1), Quaternion.identity) as GameObject;
						blockGameObject.name = "block"+block;
						blockGameObject.transform.localScale = new Vector3(_blockScale, _blockScale, _blockScale)*0.75f;
						blockGameObject.transform.parent = floorGameObject.transform;
						GS.WastesRespawnerList.Add(blockGameObject);
					}
				}
				else
				{
					if(hotel[floor, block] == 2)	//Door
					{
						if(floor == 0)
						{
							var blockGameObject = Instantiate(hotelDoorPrefab, pos, Quaternion.identity) as GameObject;
							blockGameObject.name = "block"+block;
							blockGameObject.transform.localScale = new Vector3(_blockScale, _blockScale, _blockScale);
							blockGameObject.transform.parent = floorGameObject.transform;
						}
						else
						{
							if(floor == 1)
							{
								var blockGameObject = Instantiate(hotelFacadePrefab, pos, Quaternion.identity) as GameObject;
								blockGameObject.name = "block"+block;
								blockGameObject.transform.localScale = new Vector3(_blockScale, _blockScale, _blockScale);
								blockGameObject.transform.parent = floorGameObject.transform;
							}
						}
					}
				}
			}
		}
	}
}
