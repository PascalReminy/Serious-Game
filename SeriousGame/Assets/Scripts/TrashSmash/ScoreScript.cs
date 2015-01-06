using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private Text Score;

    void Start()
    {
        Score = this.gameObject.GetComponent<Text>();
        Score.text = "score";

    }

    void SeeScore(float score)
    {
        Score.text = "score : " + score;
    }
}
