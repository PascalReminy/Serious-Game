using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text TimerText;
	public int GameDurationInSeconds = 90;
	private float intervaleBetweenWastesRespawn = 0.5f;
	private float timeSpendUntilLastFall = 0.0f;

	private GameStateWasteDropHotel GS;
	private bool _gameIsStarted = false;
	private bool _gameIsPaused = false;

	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
		GS.NumberOfRecycledGlassWaste = 0;
		GS.NumberOfRecycledPaperWaste = 0;
		GS.NumberOfRecycledPlasticWaste = 0;
		GS.GameIsStarted = false;
		GS.GameIsPaused = false;
	}
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!GS.GameIsStarted && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Game is started");
			GS.GameIsStarted = true;
		}

		if(GS.GameIsStarted && Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("Game is paused");
			GS.GameIsPaused = !GS.GameIsPaused;
		}
	}



}
