using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultatScript : MonoBehaviour {

    private Text Resultat;


    public int ScoreGold, ScoreSilver, ScoreBronze = 0;
    public Text ResultatScore;



    void Awake()
    {
        Resultat = this.gameObject.GetComponent<Text>();
    }

    void SeeScore(float score)
    {

        if (score > ScoreGold)
        {
            Resultat.text = "Tu a bien nettoyer \n Voila ton score :" + score + " \n";
        }
        else if (score > ScoreSilver)
        {
            Resultat.text = "Tu a bien nettoyer \n Voila ton score :" + score + " \n";
        }
        else if (score > ScoreBronze)
        {
            Resultat.text = "Tu a bien nettoyer \n Voila ton score :" + score + " \n";
        }
        else
        {
            Resultat.text = "Tu a bien nettoyer \n Voila ton score :" + score + " \n";
        }

    }
}
