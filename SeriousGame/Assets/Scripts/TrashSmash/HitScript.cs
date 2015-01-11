using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitScript : MonoBehaviour {

    private Text Hit;
    public Text verre;
    public Text plastique;
    public Text papier;

    int hit_verre, hit_plastique, hit_papier = 0;

    void SeeHit(string Obj)
    {
        if ("verre" == Obj)
        {
            hit_verre += 1;
            verre.text = "x" + hit_verre;
        }
        if ("plastique" == Obj)
        {
            hit_plastique += 1;
            plastique.text = "x" + hit_plastique;
        }
        if ("papier" == Obj)
        {
            hit_papier += 1;
            papier.text = "x" + hit_papier;
        }
    }

    void SetNumberOfRecycledWastes()
    {
        if (PlayerPrefs.HasKey("GlassScore") || PlayerPrefs.HasKey("PlasticScore") || PlayerPrefs.HasKey("PaperScore"))
        {
            PlayerPrefs.SetInt("GlassScore", hit_verre + PlayerPrefs.GetInt("GlassScore"));
            PlayerPrefs.SetInt("PlasticScore", hit_plastique + PlayerPrefs.GetInt("PlasticScore"));
            PlayerPrefs.SetInt("PaperScore", hit_papier + PlayerPrefs.GetInt("PaperScore"));
        }
        else
        {
            PlayerPrefs.SetInt("GlassScore", hit_verre);
            PlayerPrefs.SetInt("PlasticScore", hit_plastique);
            PlayerPrefs.SetInt("PaperScore", hit_papier);
        }
        PlayerPrefs.Save();
    }
}
