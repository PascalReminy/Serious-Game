using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text timerText;
	public int gameDurationInSeconds = 90;

	private GameStateWasteDropHotel GS;
	private bool _gameIsStarted = false;
	private bool _gameIsPaused = false;
	private float _intervaleBetweenTwoFallsWastes = 1.0f;
	private int _timeRemaininginSeconds;
	private float _timeSpendSinceLastFall = 0.0f;
	private Animator _timerAnimator;

	//
	private List<GameObject> _activeWastesRespawnersList = new List<GameObject>();
	private List<int> _floorIndexes = new List<int>();
	private List<int> _flatIndexes = new List<int>();

	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
		GS.PlayerScore = 0;
		GS.TimeScale = 1.0f;
		GS.WastesRespawnerList.Clear();
		GS.NumberOfRecycledGlassWaste = 0;
		GS.NumberOfRecycledPaperWaste = 0;
		GS.NumberOfRecycledPlasticWaste = 0;
		GS.GameIsStarted = false;
		GS.GameIsPaused = false;
		GS.GameIsFinished = false;

	}
	// Use this for initialization
	void Start ()
	{

		this._timeRemaininginSeconds = this.gameDurationInSeconds;
		this._timerAnimator = this.timerText.gameObject.GetComponent<Animator>();
		this._floorIndexes = GenerateIntegersSequence(2, GS.HotelHeight, false);
		this._flatIndexes = GenerateIntegersSequence(0, GS.HotelWidth, false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!GS.GameIsStarted && Input.GetKeyDown(KeyCode.Space) && !GS.GameIsFinished)
		{
			Debug.Log("Game is started");
			GS.GameIsStarted = true;
			this._timerAnimator.SetBool("TimerIsStarted", true);
			StartCoroutine("StartTimer");
			StartCoroutine("StartTheFallOfWaste");
		}

		if(GS.GameIsStarted && Input.GetKeyDown(KeyCode.Escape) && !GS.GameIsFinished)
		{
			Debug.Log("Game is paused");
			GS.GameIsPaused = !GS.GameIsPaused;
			if(GS.GameIsPaused)
			{
				//stop falling wastes
				GS.TimeScale = 0.0f;
				this.timerText.text = "Pause";
				this._timerAnimator.SetBool("TimerIsStarted", false);
				StopCoroutine("StartTimer");
			}
			else
			{
				GS.TimeScale = 1.0f;
				this._timerAnimator.SetBool("TimerIsStarted", true);
				StartCoroutine("StartTimer");
			}
		}

		if(GS.GameIsFinished)
		{
			this.timerText.text = "GameOver";
			GS.TimeScale = 0.0f;
		}
	}

	IEnumerator StartTimer()
	{
		while(this._timeRemaininginSeconds > 0)
		{
			this._timeRemaininginSeconds--;
			this.timerText.text = TimerToString();
			yield return new WaitForSeconds(1.0f);
		}
		GS.GameIsFinished = true;
	}

	IEnumerator StartTheFallOfWaste()
	{
		while(this._timeRemaininginSeconds > 0)
		{
			if(_timeSpendSinceLastFall == 0.0f)
			{
				UpdateActiveWastesRespawnersList();

				foreach(GameObject activeRespawner in this._activeWastesRespawnersList)
				{
					activeRespawner.renderer.material.color = Color.grey;
					activeRespawner.SendMessage("DropWaste");
				}
				_timeSpendSinceLastFall += Time.deltaTime * GS.TimeScale;
			}
			else
			{
				_timeSpendSinceLastFall += Time.deltaTime * GS.TimeScale;
				if(_timeSpendSinceLastFall >= _intervaleBetweenTwoFallsWastes)
				{
					foreach(GameObject activeRespawner in this._activeWastesRespawnersList)
					{
						activeRespawner.renderer.material.color = Color.white;
					}
					_timeSpendSinceLastFall = 0.0f;
				}
			}
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}

	public string TimerToString()
	{
		int m = this._timeRemaininginSeconds/60;
		int s = this._timeRemaininginSeconds%60;

		return ((m<10)?("0"+m):(""+m))+":"+((s<10)?("0"+s):(""+s));
	}

	public void UpdateActiveWastesRespawnersList()
	{
		this._activeWastesRespawnersList.Clear();

		if(this._floorIndexes.Count == 0 && this._floorIndexes.Count == 0)
		{
			this._floorIndexes = GenerateIntegersSequence(2, GS.HotelHeight, false);
			this._flatIndexes = GenerateIntegersSequence(0, GS.HotelWidth, false);
			if(this._flatIndexes.Count > this._floorIndexes.Count)
			{
				while(this._flatIndexes.Count != this._floorIndexes.Count)
				{
					this._floorIndexes.Add(this._floorIndexes[Random.Range(0,this._floorIndexes.Count)]);
				}
			}
			else
			{
				if(this._flatIndexes.Count < this._floorIndexes.Count)
				{
					while(this._flatIndexes.Count != this._floorIndexes.Count)
					{
						this._flatIndexes.Add(this._flatIndexes[Random.Range(0,this._flatIndexes.Count)]);
					}
				}
			}
		}
		//Debug.Log("Same size : "+(this._flatIndexes.Count == this._floorIndexes.Count).ToString());
		/*while((this._flatIndexes.Count > 0 && this._floorIndexes.Count > 0))
		{*/
			int randFlat = Random.Range(0,this._flatIndexes.Count);
			int randFloor = Random.Range(0,this._floorIndexes.Count);
			//Debug.Log("number of respawner: "+GS.WastesRespawnerList.Count);
			//Debug.Log ("Active waste respawn  : ("+(this._flatIndexes[randFlat]+this._floorIndexes[randFloor]*GS.HotelWidth-2)+")");
			this._activeWastesRespawnersList.Add(GS.WastesRespawnerList[this._flatIndexes[randFlat] + this._floorIndexes[randFloor]*GS.HotelWidth-2]);
			this._flatIndexes.RemoveAt(randFlat);
			this._floorIndexes.RemoveAt(randFloor);
		//}
		//regenarate index

	}

	public static List<int> GenerateIntegersSequence(int from, int to, bool inclusive = false)
	{
		List<int> seq = new List<int>();
		if(inclusive)
		{
			for(int i = from; i<= to; i++ )
			{
				seq.Add(i);
			}
		}
		else
		{
			for(int i = from; i < to; i++)
			{
				seq.Add(i);
			}
		}
		return seq;
	}

}
