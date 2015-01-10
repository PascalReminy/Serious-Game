using UnityEngine;
using System.Collections;

//public enum WasteType { Plastic, Paper, Glass};

public class DumpsterControllerScript : MonoBehaviour {

	public KeyCode moveLeftKeyCode = KeyCode.LeftArrow;
	public KeyCode moveRightKeyCode = KeyCode.RightArrow;
	public KeyCode paperDumpsterKeyCode = KeyCode.W;
	public KeyCode plasticDumpsterKeyCode = KeyCode.X;
	public KeyCode glassDumpsterKeyCode = KeyCode.C;


	public  bool _wantToMoveLeft = false;
	public bool _wantToMoveRight = false;
	public GameObject _paperDumpster;
	public GameObject _plasticDumpster;
	public GameObject _glassDumpster;

	private float _moveAmount;
	private GameStateWasteDropHotel GS;
	private WasteType targetWaste = WasteType.Paper;


	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
		_moveAmount = GS.HotelScale;
	}

	// Use this for initialization
	void Start () {
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
	
		if(Input.GetKeyDown(moveLeftKeyCode) && !_wantToMoveLeft)
		{
			Debug.Log ("Left Arrow");
			_wantToMoveLeft = true;
		}

		if(Input.GetKeyDown(moveRightKeyCode) && !_wantToMoveRight)
		{
			Debug.Log ("Right Arrow");
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
		Debug.Log("Collid");
		Debug.Log(col.tag);
		Destroy(col.gameObject);
	}
}
