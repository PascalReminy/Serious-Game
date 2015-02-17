using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateWasteDropHotel{

	protected GameStateWasteDropHotel(){}

	private static GameStateWasteDropHotel _instance = null;

	public static GameStateWasteDropHotel Instance {
		get
		{
			if(_instance == null)
			{
				_instance = new GameStateWasteDropHotel();
			}
			return _instance;
		}

	}

	private bool _gameIsStarted;

	public bool GameIsStarted {
		get {
			return _gameIsStarted;
		}
		set {
			_gameIsStarted = value;
		}
	}

	private bool _gameIsPaused;

	public bool GameIsPaused {
		get {
			if(!_gameIsStarted)
				_gameIsPaused = false;
			return _gameIsPaused;
		}
		set {
			_gameIsPaused = value;
		}
	}

	private bool _gameIsFinished = false;

	public bool GameIsFinished {
		get {
			return _gameIsFinished;
		}
		set {
			_gameIsFinished = value;
		}
	}

	private int _hotelWidth = 5;

	public int HotelWidth {
		get {
			return _hotelWidth;
		}
		set {
			_hotelWidth = value;
		}
	}

	private int _hotelHeight = 7;

	public int HotelHeight {
		get {
			return _hotelHeight;
		}
		set {
			_hotelHeight = value;
		}
	}

	private float _hotelScale = 2.0f;

	public float HotelScale {
		get {
			return _hotelScale;
		}
		set {
			_hotelScale = value;
		}
	}

	private float _timeScale = 1.0f;

	public float TimeScale {
		get {
			return _timeScale;
		}
		set {
			_timeScale = value;
		}
	}


	private float _intervaleBetweenWastesRespawn = 0.5f;

	public float IntervaleBetweenWastesRespawn {
		get {
			return _intervaleBetweenWastesRespawn;
		}
		set {
			_intervaleBetweenWastesRespawn = value;
		}
	}

	private int _playerScore = 0;

	public int PlayerScore {
		get {
			return _playerScore;
		}
		set {
			_playerScore = value;
		}
	}

	private int _goodRecyclingScore = 100;

	public int GoodRecyclingScore {
		get {
			return _goodRecyclingScore;
		}
		set {
			_goodRecyclingScore = value;
		}
	}

	private int _wrongRecyclingScore = -150;

	public int WrongRecyclingScore {
		get {
			return _wrongRecyclingScore;
		}
		set {
			_wrongRecyclingScore = value;
		}
	}

	private int _numberOfRecycledGlassWaste = 0;

	public int NumberOfRecycledGlassWaste {
		get {
			return _numberOfRecycledGlassWaste;
		}
		set {
			_numberOfRecycledGlassWaste = value;
		}
	}

	private int _numberOfRecycledPaperWaste = 0;

	public int NumberOfRecycledPaperWaste {
		get {
			return _numberOfRecycledPaperWaste;
		}
		set {
			_numberOfRecycledPaperWaste = value;
		}
	}

	private int _numberOfRecycledPlasticWaste = 0;

	public int NumberOfRecycledPlasticWaste {
		get {
			return _numberOfRecycledPlasticWaste;
		}
		set {
			_numberOfRecycledPlasticWaste = value;
		}
	}

	private List<GameObject> _wastesRespawnerList = new List<GameObject>();

	public List<GameObject> WastesRespawnerList {
		get {
			return _wastesRespawnerList;
		}
		set {
			_wastesRespawnerList = value;
		}
	}
}
