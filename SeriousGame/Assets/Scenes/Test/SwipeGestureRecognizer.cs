using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SwipeGestureType{ Horizontal, Vertical, Both};
public enum SwipeDirection{ Up, Down, Left, Right, None};
public class SwipeState
{

	public Vector3 startPosition;
	public Vector3 currentPosition;
	public SwipeDirection swipeDirection;

	public SwipeState ()
	{
		this.currentPosition = Vector3.zero;
		this.startPosition = Vector3.zero;
		this.swipeDirection = SwipeDirection.None;
	}

	public SwipeState (Vector3 start, Vector3 current, SwipeDirection dir)
	{
		this.currentPosition = current;
		this.startPosition = start;
		this.swipeDirection = dir;
	}
}

public class SwipeGestureRecognizer : MonoBehaviour {
	
	public GameObject swipeGestureUser;
	public SwipeGestureType swipeType = SwipeGestureType.Both;
	public float minScreenPercentHorizontalSwipe = 20; //
	public float minScreenPercentVerticalSwipe = 20;
	public bool moveAlongSingleAxisAtSameTime = true;
	public bool followSwipe = true;
	public float goBackToInitialPositionSpeed = 1.0f;

	private float _minPixelWidthSwipe;
	private float _minPixelHeightSwipe;
	private float _moveAmount = 0.0f;

	private Vector2 _swipingFingerStartPosition;
	private Vector2 _swipingFingerCurrentPosition;

	private Vector3 _initialSwipeObjectPosition;

	private bool _goBackToInitialPositionOnRelease = false;
	private bool _isSwiping = false;

	void Start ()
	{
		if(swipeGestureUser == null)
		{
			swipeGestureUser = this.gameObject;
		}
		_initialSwipeObjectPosition = swipeGestureUser.transform.position;
		_minPixelWidthSwipe = ((Screen.width * minScreenPercentHorizontalSwipe)/100.0f);
		_minPixelHeightSwipe = ((Screen.height * minScreenPercentVerticalSwipe)/100.0f);



	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR 
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
		{

			_swipingFingerCurrentPosition = getDirectionRegardingSwipeType(Input.mousePosition);

			if(Input.GetMouseButtonDown(0))
			{
				_swipingFingerStartPosition = _swipingFingerCurrentPosition;
			}
			else
			{
				if(Input.GetMouseButton(0))
				{
					_isSwiping = true;
					if(_swipingFingerStartPosition != _swipingFingerCurrentPosition)
					{
						BroadcastSwipeEvent();
					}
				}
				else
				{
					if(Input.GetMouseButtonUp(0))
					{
						_isSwiping = false;
						BroadcastSwipeEvent();
					}
				}
			}
		}
#else
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			_swipingFingerCurrentPosition = getDirectionRegardingSwipeType(touch.position);
			
			if(touch.phase == TouchPhase.Began)
			{
				_swipingFingerStartPosition = _swipingFingerCurrentPosition;
			}
			else
			{
				if(touch.phase == TouchPhase.Moved)
				{
					_isSwiping = true;
					if(_swipingFingerStartPosition != _swipingFingerCurrentPosition)
					{
						BroadcastSwipeEvent();
					}
				}
				else
				{
					if(touch.phase == TouchPhase.Ended)
					{

						_isSwiping = false;
						BroadcastSwipeEvent();
					}
				}
			}
		}
#endif	
		
	}

	void FixedUpdate()
	{
		if( _goBackToInitialPositionOnRelease )
		{
			Debug.Log("Go Back to initial position");
			if(_moveAmount <= 1f)
			{
				Debug.Log("Going back" + _moveAmount);
				Vector3 newPosition = Vector3.Lerp(swipeGestureUser.transform.position, _initialSwipeObjectPosition, goBackToInitialPositionSpeed * Time.fixedDeltaTime);
				swipeGestureUser.transform.position = newPosition;
				_moveAmount += (goBackToInitialPositionSpeed * Time.fixedDeltaTime);
			}
			else
			{
				swipeGestureUser.transform.position = _initialSwipeObjectPosition;
				_moveAmount = 0;
				_goBackToInitialPositionOnRelease = false;
			}
			
		}
	}

	IEnumerator GoBackToInitialPosition()
	{
		Vector3 currentPosition = swipeGestureUser.transform.position;
		while(_moveAmount <= 1f)
		{
			_moveAmount += (goBackToInitialPositionSpeed * Time.deltaTime);
			Vector3 newPosition = Vector3.Lerp(currentPosition, _initialSwipeObjectPosition, _moveAmount);
			swipeGestureUser.transform.position = newPosition;

			yield return new WaitForSeconds(Time.deltaTime);

		}
		_moveAmount = 0;
		_goBackToInitialPositionOnRelease = false;

	}
	
	public void BroadcastSwipeEvent ()
	{
		SwipeDirection swipeDir = SwipeDirection.None;
		float swipeVerticalDistance = 0 ;
		if(swipeType == SwipeGestureType.Both || swipeType == SwipeGestureType.Vertical)
		{
			swipeVerticalDistance = (new Vector3(0, _swipingFingerCurrentPosition.y, 0) - new Vector3(0, _swipingFingerStartPosition.y, 0)).magnitude;

			if (swipeVerticalDistance > _minPixelHeightSwipe) 	
			{
				float swipeVerticaleValue = Mathf.Sign(_swipingFingerCurrentPosition.y - _swipingFingerStartPosition.y);
				
				if (swipeVerticaleValue > 0)//up swipe
				{
					swipeDir = SwipeDirection.Up;
				}
				else
				{
					if (swipeVerticaleValue < 0)//down swipe
					{
						swipeDir = SwipeDirection.Down;
					}
				}		
			}
		
		}

		float swipeHorizontalDistance = 0;

		if(swipeType == SwipeGestureType.Both || swipeType == SwipeGestureType.Horizontal)
		{
			swipeHorizontalDistance = (new Vector3(_swipingFingerCurrentPosition.x, 0, 0) - new Vector3(_swipingFingerStartPosition.x, 0, 0)).magnitude;
			if (swipeHorizontalDistance > _minPixelWidthSwipe) 
			{
				float swipeHorizontalValue = Mathf.Sign(_swipingFingerCurrentPosition.x - _swipingFingerStartPosition.x);
				
				if (swipeHorizontalValue > 0)//right swipe
				{	
					swipeDir = SwipeDirection.Right;
				}	
				else
				{
					if (swipeHorizontalValue < 0)//left swipe
					{
						swipeDir = SwipeDirection.Left;
					}	
				}	
			}
		}




		if(!_isSwiping)
		{
			swipeGestureUser.SendMessage("OnSwipePerformed",
			                             new SwipeState(_swipingFingerStartPosition, _swipingFingerCurrentPosition, swipeDir),
			                             SendMessageOptions.DontRequireReceiver);
			StartCoroutine("GoBackToInitialPosition");
		}
		else
		{
			//the object follow user swiping finger
			if(followSwipe)
			{
				var currentPos = swipeGestureUser.transform.position;
				var tmp = Camera.main.ScreenToWorldPoint(_swipingFingerCurrentPosition ) - Camera.main.ScreenToWorldPoint(_swipingFingerStartPosition);
				if(moveAlongSingleAxisAtSameTime)
				{
					if(swipeHorizontalDistance/Screen.width > swipeVerticalDistance/Screen.height)
					{
						currentPos = new Vector3( tmp.x - _initialSwipeObjectPosition.x, _initialSwipeObjectPosition.y, _initialSwipeObjectPosition.z);
					}
					else
					{
						if(swipeHorizontalDistance/Screen.width < swipeVerticalDistance/Screen.height)
						{
							currentPos = new Vector3( _initialSwipeObjectPosition.x , tmp.y - _initialSwipeObjectPosition.y, _initialSwipeObjectPosition.z);
						}
					}
				}
				else
				{
					currentPos += new Vector3( tmp.x  - currentPos.x, tmp.y - currentPos.y, currentPos.z);
				}
				swipeGestureUser.transform.position = currentPos;
			}
			swipeGestureUser.SendMessage("OnSwipePerforming", new SwipeState(_swipingFingerStartPosition, _swipingFingerCurrentPosition, SwipeDirection.None), SendMessageOptions.DontRequireReceiver);

		}
	}


	public Vector3 getDirectionRegardingSwipeType( Vector3 dir)
	{
		if(this.swipeType == SwipeGestureType.Both)
		{
			return dir;
		}
		else
		{
			if(this.swipeType == SwipeGestureType.Horizontal)
			{
				var objectScreenPos = Camera.main.WorldToScreenPoint(swipeGestureUser.transform.position);
				return new Vector3(dir.x, objectScreenPos.y, dir.z);
			}
			else
			{
				if(this.swipeType == SwipeGestureType.Vertical)
				{
					var objectScreenPos = Camera.main.WorldToScreenPoint(swipeGestureUser.transform.position);
					return new Vector3(objectScreenPos.x, dir.y, dir.z);
				}
				else
				{
					var objectScreenPos = Camera.main.WorldToScreenPoint(swipeGestureUser.transform.position);

					return objectScreenPos;
				}
			}
		}
	}
}
