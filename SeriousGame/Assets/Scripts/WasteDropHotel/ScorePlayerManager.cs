using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScorePlayerManager : MonoBehaviour {

	public GameObject score;
	public GameObject highscore;

	private int _playerScore = 0;
	private Text _scoreText;
	private Text _highscoreText;

	private GameStateWasteDropHotel GS;

	void Awake()
	{
		GS = GameStateWasteDropHotel.Instance;
	}

	// Use this for initialization
	void Start () {
		this._scoreText = score.gameObject.GetComponent<Text>();
		this._highscoreText = highscore.gameObject.GetComponent<Text>();

		if(!PlayerPrefs.HasKey("HighscoreWasteDropHotel"))
		{
			PlayerPrefs.SetInt("HighscoreWasteDropHotel",0);
		}
		this._highscoreText.text = "Highscore\n"+intToStringDigit(PlayerPrefs.GetInt("HighscoreWasteDropHotel"),6);
		//Debug.Log(intToStringDigit(5648,8));
	}
	
	// Update is called once per frame
	void Update () {
		if(this._playerScore != GS.PlayerScore)
		{
			GS.PlayerScore = (GS.PlayerScore < 0)?0:GS.PlayerScore;
			this._playerScore = GS.PlayerScore;
			this._scoreText.text = "Score\n"+this._playerScore.ToString("000000");
		}

		if(GS.GameIsFinished)
		{
			var previousHightscore = 0;
			if(PlayerPrefs.HasKey("HighscoreWasteDropHotel"))
			{
				previousHightscore = PlayerPrefs.GetInt("HighscoreWasteDropHotel");
			}
			if(GS.PlayerScore > previousHightscore)
				PlayerPrefs.SetInt("HighscoreWasteDropHotel",GS.PlayerScore);
			this._highscoreText.text = "Highscore\n"+intToStringDigit(PlayerPrefs.GetInt("HighscoreWasteDropHotel"),6);
		}
	}

	public static string intToStringDigit (int n, int numberOfDigit)
	{
		string strDigit = "";
		for(int i = numberOfDigit-1; i > 0; i --)
		{
			var divider = (int)Mathf.Pow(10, i); 
			if(n/divider == 0)
			{
				strDigit += "0";
			}
		}
		return strDigit+""+n;
	}
}
