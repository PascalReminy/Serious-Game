using UnityEngine;
using System.Collections;

public class ObjectBehaviourOnSwipe : MonoBehaviour {

	public Color onSwipeUpColor;
	public Color onSwipeDownColor;
	public Color onSwipeLeftColor;
	public Color onSwipeRightColor;


	private Material _mat;
	private Color _defaultColor;

	private SwipeState swipe = new SwipeState();
	private Rect _windowRect;


	private int screenWidth;
	private int screenHeight;

	// Use this for initialization
	void Start () {
		_mat = this.renderer.material;
		_defaultColor = _mat.color;

		screenWidth = Screen.width;
		screenHeight = Screen.height;
		_windowRect = new Rect (0, screenHeight-200, screenWidth, 200);

;
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void OnSwipePerforming(SwipeState s)
	{
		//do something during the swipe

	}

	public void OnSwipePerformed(SwipeState s)
	{
		swipe = s;

		switch (s.swipeDirection)
		{
		case SwipeDirection.Up:
			_mat.color = onSwipeUpColor;
			break;
		case SwipeDirection.Down:
			_mat.color = onSwipeDownColor;
			break;
		case SwipeDirection.Left:
			_mat.color = onSwipeLeftColor;
			break;
		case SwipeDirection.Right:
			_mat.color = onSwipeRightColor;
			break;
		case SwipeDirection.None:
			_mat.color = _defaultColor;
			break;

		}
	}


	
	void OnGUI () {
		_windowRect = GUI.Window (0, _windowRect, WindowFunction, "Swipe INFO");
	}
	
	void WindowFunction (int windowID) {
		// Draw any Controls inside the window here
		GUI.Label(new Rect(10, 20, screenWidth, 20), "Start Swipe: " + swipe.startPosition.ToString());
		GUI.Label(new Rect(10, 40, screenWidth, 20), "End Swipe: " + swipe.currentPosition.ToString());
		GUI.Label(new Rect(10, 60, screenWidth, 20), "Min Dist X: " + ((screenWidth * 20)/100));
		GUI.Label(new Rect(10, 80, screenWidth, 20), "Dist X: " + (new Vector3(swipe.currentPosition.x, 0, 0) - new Vector3(swipe.startPosition.x, 0, 0)).magnitude);
		GUI.Label(new Rect(10, 100, screenWidth, 20), "Min Dist Y: " + ((20 * screenHeight)/100));
		GUI.Label(new Rect(10, 120,screenWidth, 20), "Dist Y: " + (new Vector3(0, swipe.currentPosition.y, 0) - new Vector3(0, swipe.startPosition.y, 0)).magnitude);
		GUI.Label(new Rect(10, 140,screenWidth, 20), "Swipe Direction: " + swipe.swipeDirection.ToString());
		GUI.Label(new Rect(10, 160,screenWidth, 20), "Screen Resolution: " + screenWidth + "x" + screenHeight);
	}

}
