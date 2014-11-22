using UnityEngine;
using System.Collections;

public class DumpsterControllerScript : MonoBehaviour {

	public KeyCode moveLeftKeyCode = KeyCode.LeftArrow;
	public KeyCode moveRightKeyCode = KeyCode.RightArrow;

	public  bool _wantToMoveLeft = false;
	public bool _wantToMoveRight = false;


	// Use this for initialization
	void Start () {
	
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
			this.transform.position = prevPos + Vector3.right;
			_wantToMoveRight = false;
		}
	}
}
