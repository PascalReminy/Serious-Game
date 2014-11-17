using UnityEngine;
using System.Collections;

public class DumpsterControllerScript : MonoBehaviour {

	public KeyCode moveLeftKeyCode = KeyCode.LeftArrow;
	public KeyCode moveRightKeyCode = KeyCode.RightArrow;

	private bool _wantToMoveLeft = false;
	private bool _wantToMoveRight = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(moveLeftKeyCode) && !_wantToMoveLeft)
		{
			_wantToMoveLeft = true;
		}

		if(Input.GetKeyDown(moveLeftKeyCode) && !_wantToMoveRight)
		{
			_wantToMoveRight = true;
		}

		if(Input.GetKeyUp(moveLeftKeyCode))
		{
			_wantToMoveLeft = false;
		}

		if(Input.GetKeyUp(moveLeftKeyCode))
		{
			_wantToMoveRight = false;
		}

	}

	void FixedUpdate()
	{
		if(_wantToMoveLeft)
		{
			var prevPos = this.transform.position;
			this.transform.position = prevPos + Vector3.left;
			_wantToMoveLeft = false;
		}

		if(_wantToMoveRight)
		{
			var prevPos = this.transform.position;
			this.transform.position = prevPos - Vector3.left;
			_wantToMoveRight = false;
		}
	}
}
