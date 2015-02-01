using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultatScript : MonoBehaviour {

    public int ScoreGold, ScoreSilver, ScoreBronze = 0;
    public Text ResultatScore;
    public Image MedalImage;
    public Image Tutoral;
    public Sprite[] Medal = new Sprite[3];


    void HowIPlay()
    {
        MedalImage.gameObject.SetActive(false);
        ResultatScore.gameObject.SetActive(false);
    }

    void SeeScore(float score)
    {

        if (score > ScoreGold)
        {
            Tutoral.gameObject.SetActive(false);
            MedalImage.gameObject.SetActive(true);
            ResultatScore.gameObject.SetActive(true);

            ResultatScore.text = "Incroyable Tu es vraimant tres fort !!!  \n\n Voila ton score : " + score + "!!! \n";
            MedalImage.sprite = Medal[0];
        }
        else if (score > ScoreSilver)
        {
            Tutoral.gameObject.SetActive(false);
            MedalImage.gameObject.SetActive(true);
            ResultatScore.gameObject.SetActive(true);

            ResultatScore.text = "Quelle vitesse felicitation ! \n\n Voila ton score : " + score + "!! \n";
            MedalImage.sprite = Medal[1];
        }
        else if (score > ScoreBronze)
        {
            Tutoral.gameObject.SetActive(false);
            MedalImage.gameObject.SetActive(true);
            ResultatScore.gameObject.SetActive(true);

            ResultatScore.text = "Pas mal du tout : " + score + " \n";
            MedalImage.sprite = Medal[2];
        }
        else
        {
            Tutoral.gameObject.SetActive(false);
            ResultatScore.gameObject.SetActive(true);

            ResultatScore.text = "Tu peux mieux faire \n Voila ton score : " + score + " \n";
        }

    }

}
