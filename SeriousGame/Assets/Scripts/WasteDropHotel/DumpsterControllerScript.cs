using UnityEngine;
using System.Collections;

//public enum WasteType { Plastic, Paper, Glass};
using UnityEngine.UI;

public class DumpsterControllerScript : MonoBehaviour {

	public KeyCode moveLeftKeyCode = KeyCode.LeftArrow;
	public KeyCode moveRightKeyCode = KeyCode.RightArrow;
	public KeyCode paperDumpsterKeyCode = KeyCode.W;
	public KeyCode plasticDumpsterKeyCode = KeyCode.X;
	public KeyCode glassDumpsterKeyCode = KeyCode.C;
	public RectTransform recyclingScore;
	public Color goodRecyclingColor = Color.green;
	public Color wrongRecyclingColor = Color.red;

	private  bool _wantToMoveLeft = false;
	private bool _wantToMoveRight = false;
	private GameObject _paperDumpster;
	private GameObject _plasticDumpster;
	private GameObject _glassDumpster;

	private float _moveAmount;
	private GameStateWasteDropHotel GS;
	private WasteType targetWaste = WasteType.Paper;

	private Text _recyclingScoreText;
	private Animator _recyclingScoreAnimator;
	private int _goodRecyclingScore;
	private int _wrongRecyclingScore;



	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
		_moveAmount = GS.HotelScale;
		_goodRecyclingScore = GS.GoodRecyclingScore;
		_wrongRecyclingScore = GS.WrongRecyclingScore;


	}

	// Use this for initialization
	void Start () {

		this._recyclingScoreText = recyclingScore.GetComponentInChildren<Text>();
		this._recyclingScoreAnimator = recyclingScore.GetComponentInChildren<Animator>();

		Transform[] tranDumps = this.GetComponentsInChildren<Transform>();
		foreach(Transform t in tranDumps)
		{
			//Debug.Log("*"+t.tag+"*");
			if(t.tag == "Paper")
			{
				_paperDumpster = t.gameObject;
			}
			else
			{
				if(t.tag == "Plastic")
				{
					_plasticDumpster = t.gameObject;
				}
				else
				{
					if( t.tag == "Glass")
					{
						_glassDumpster = t.gameObject;
					}
				}
			}
		}
		SetActiveDumpster(targetWaste);
	}
	
	// Update is called once per frame
	void Update () {
		if(!GS.GameIsPaused && GS.GameIsStarted)
		{
			if(Input.GetKeyDown(moveLeftKeyCode) && !_wantToMoveLeft)
			{
				_wantToMoveLeft = true;
			}

			if(Input.GetKeyDown(moveRightKeyCode) && !_wantToMoveRight)
			{
				_wantToMoveRight = true;
			}

			if(Input.GetKeyUp(moveLeftKeyCode))
			{
				_wantToMoveLeft = false;
			}

			if(Input.GetKeyUp(moveRightKeyCode))
			{
				_wantToMoveRight = false;
			}

			if(Input.GetKeyDown(paperDumpsterKeyCode))
			{
				SetActiveDumpster(WasteType.Paper);
			}

			if(Input.GetKeyDown(plasticDumpsterKeyCode))
			{
				SetActiveDumpster(WasteType.Plastic);
			}

			if(Input.GetKeyDown(glassDumpsterKeyCode))
			{
				SetActiveDumpster(WasteType.Glass);
			}
		}
	}

	void FixedUpdate()
	{
		if(_wantToMoveLeft)
		{
			var prevPos = this.transform.position;
			this.transform.position = prevPos + Vector3.left * _moveAmount;
			_wantToMoveLeft = false;
		}

		if(_wantToMoveRight)
		{
			var prevPos = this.transform.position;
			this.transform.position = prevPos + Vector3.right * _moveAmount;
			_wantToMoveRight = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log(col.gameObject.tag);
	}

	public void SetActiveDumpster(WasteType type)
	{
		targetWaste = type;
		_paperDumpster.SetActive((type == WasteType.Paper));
		_plasticDumpster.SetActive((type == WasteType.Plastic));
		_glassDumpster.SetActive((type == WasteType.Glass));
	}

	void OnTriggerEnter(Collider col)
	{
		//check if the waste match the dumpster
		if(col.tag == targetWaste.ToString())
		{
			Debug.Log("good");
			this._recyclingScoreText.text = "+"+(_goodRecyclingScore);
			this._recyclingScoreText.color = goodRecyclingColor;
			this._recyclingScoreAnimator.SetBool("isHidden", false);
			StartCoroutine("WaitEndOfScoreAnimation");


			if(targetWaste == WasteType.Glass)
			{
				GS.NumberOfRecycledGlassWaste++;
			}
			else if(targetWaste == WasteType.Paper)
			{
				GS.NumberOfRecycledPaperWaste++;
			}
			else if(targetWaste == WasteType.Plastic)
			{
				GS.NumberOfRecycledPlasticWaste++;
			}
		}
		else
		{
			this._recyclingScoreText.text = ""+(_wrongRecyclingScore);
			this._recyclingScoreText.color = wrongRecyclingColor;
			this._recyclingScoreAnimator.SetBool("isHidden", false);
			StartCoroutine("WaitEndOfScoreAnimation");
		}

		//destroy the waste
		Destroy(col.gameObject);
	}

	IEnumerator WaitEndOfScoreAnimation()
	{
		yield return new WaitForEndOfFrame();
		this._recyclingScoreAnimator.SetBool("isHidden", true);

	}
}
