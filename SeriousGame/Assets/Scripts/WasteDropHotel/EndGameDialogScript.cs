using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameDialogScript : MonoBehaviour {


	public Text glassCounterText;
	public Text plasticCounterText;
	public Text paperCounterText;

	private bool _once = true;
	private GameStateWasteDropHotel GM;

	// Use this for initialization
	void Start () {
		GM = GameStateWasteDropHotel.Instance;
		ShowOrHidePauseDialog();
	}
	
	// Update is called once per frame
	void Update () {
		if(GM.GameIsFinished && this._once)
		{
			ShowOrHidePauseDialog();
			this._once = false;
			glassCounterText.text = "   x"+GM.NumberOfRecycledGlassWaste;
			plasticCounterText.text = "   x"+GM.NumberOfRecycledPlasticWaste;;
			paperCounterText.text = "   x"+GM.NumberOfRecycledPaperWaste;

		}
	}

	public void ShowOrHidePauseDialog()
	{
		//Show/Hide Pause Dialog
		var children = this.gameObject.GetComponentInChildren<Transform>();
		foreach(Transform child in children)
		{
			child.gameObject.SetActive(GM.GameIsFinished);
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
}
