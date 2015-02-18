using UnityEngine;
using System.Collections;

public class PauseDialogScript : MonoBehaviour {

	private GameStateWasteDropHotel GS;
	private bool _isPaused ;
	// Use this for initialization
	void Awake () {
		GS = GameStateWasteDropHotel.Instance;
		this._isPaused = GS.GameIsPaused;


	}

	void Start()
	{
		ShowOrHidePauseDialog();
	}

	// Update is called once per frame
	void Update ()
	{
		if(!GS.GameIsFinished)
		{
			if(this._isPaused != GS.GameIsPaused)
			{
				Debug.Log("Pause Dialog State has changed");
				this._isPaused = GS.GameIsPaused;
				ShowOrHidePauseDialog();
			}
		}
	}

	public void ShowOrHidePauseDialog()
	{
		//Show/Hide Pause Dialog
		var children = this.gameObject.GetComponentInChildren<Transform>();
		foreach(Transform child in children)
		{
			child.gameObject.SetActive(this._isPaused);
		}
	}
	
	public void GoToMainMenu()
	{
		Application.LoadLevel("Menu");
	}

	public void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
