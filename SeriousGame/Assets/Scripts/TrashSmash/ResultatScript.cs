using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultatScript : MonoBehaviour {

    public Image MedalImage; 
    public int ScoreGold, ScoreSilver, ScoreBronze = 0;
    public Text ResultatScore;
    public Sprite[] Medal = new Sprite[3];
    



    void SeeScore(float score)
    {

        if (score > ScoreGold)
        {
            ResultatScore.text = "Incroyable Tu es vraimant tres fort !!!  \n\n Voila ton score : " + score + "!!! \n";
            MedalImage.sprite = Medal[0];
        }
        else if (score > ScoreSilver)
        {
            ResultatScore.text = "Quelle vitesse felicitation ! \n\n Voila ton score : " + score + "!! \n";
            MedalImage.sprite = Medal[1];
        }
        else if (score > ScoreBronze)
        {
            ResultatScore.text = "Pas mal du tout : " + score + " \n";
            MedalImage.sprite = Medal[2];
        }
        else
        {
            ResultatScore.text = "Tu peux mieux faire \n Voila ton score : " + score + " \n";
            MedalImage.gameObject.SetActive(false);
        }

    }
}
