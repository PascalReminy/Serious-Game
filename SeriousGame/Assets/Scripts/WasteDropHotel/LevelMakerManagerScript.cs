﻿using UnityEngine;
using System.Collections;

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
	public GameObject GlassDoorPrefab;
	public GameObject WallPrefab;
	public LevelDifficulty difficulty;
	public int hotelWidth = 3;
	public int hotelHeight = 3;
	public int[,] hotel;

	// Use this for initialization
	void Start () {
		//int[][] hotel = new int[3][3];
		hotel = new int[hotelHeight+4, hotelWidth];

		GenerateRandomHotel();
		InstantiateHotel();
		Debug.Log(hotel);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateRandomHotel()
	{
		for(int floor = 0; floor < hotel.GetLength(0); floor++)
		{
			for(int block = 0; block < hotel.GetLength(1); block ++ )
			{
				if(floor > 0 && floor <= hotelHeight)
				{
					hotel[floor, block] = Random.Range(0,2);
				}
				else
				{
					hotel[floor, block] = 0;
					if(floor > hotelHeight+1)
					{
						if((hotel.GetLength(1)%2 == 1) && block == hotel.GetLength(1)/2)
						{
							hotel[floor, block] = 2;
						}
						else
						{
							if((hotel.GetLength(1)%2 == 0) && (block == (hotel.GetLength(1)-1)/2 || block == (hotel.GetLength(1)-1)/2+1))
							{
								hotel[floor, block] = 2;
							}
						}
					}
				}
			}
		}
	}

	public void InstantiateHotel()
	{
		for(int floor = 0; floor < hotel.GetLength(0); floor++)
		{
			for(int block = 0; block < hotel.GetLength(1); block ++ )
			{

				if(hotel[floor, block] == 0)
				{
					Instantiate(wallPrefab, new Vector3(block,hotel.GetLength(0)-floor), Quaternion.identity);
				}
				else
				{
					if(hotel[floor, block] == 1)
					{
						Instantiate(windowWasteRespawnerPrefab, new Vector3(block,hotel.GetLength(0)-floor), Quaternion.identity);
					}
					else
					{
						if(hotel[floor, block] == 2)
						{
							Instantiate(GlassDoorPrefab, new Vector3(block,hotel.GetLength(0)-floor), Quaternion.identity);
						}
						else
						{
							if(hotel[floor, block] == 3)
							{
								Instantiate(WallPrefab, new Vector3(block,hotel.GetLength(0)-floor), Quaternion.identity);
							}
						}
					}
				}

			}
		}
	}
}
