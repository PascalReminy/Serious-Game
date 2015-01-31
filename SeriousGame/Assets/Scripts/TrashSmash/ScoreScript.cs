using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private Text Score;
    private Text HighScore;
    private float _HighScore = 0.0f;
   

    public bool HighScoreBool;

    void Start()
    {
        if (!HighScoreBool)
        {
            Score = this.gameObject.GetComponent<Text>();
            Score.text = "score : 0";
        }
        else
        {
            HighScore = this.gameObject.GetComponent<Text>();
            SeeHighScore(PlayerPrefs.GetFloat("HighScore"));
        }
    }

    void SeeScore(float score)
    {
        Score.text = "score : " + score;
    }

    void SeeHighScore(float score)
    {

        if (score > _HighScore)
        {
            HighScore.text = "HighScore : " + score;
            _HighScore = score; 
        }
        
        PlayerPrefs.SetFloat("HighScore", _HighScore);
        PlayerPrefs.Save();
    }
}
