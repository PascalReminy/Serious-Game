using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitScript : MonoBehaviour {

    private Text Hit;
    public bool verre;
    public bool papier;
    public bool plastique;
    int hit_verre, hit_plastique, hit_papier = 0;

    void Start()
    {
        Hit = this.gameObject.GetComponent<Text>();
    }

    void SeeHit(string Obj)
    {
        if ("verre" == Obj && verre)
        {
            hit_verre += 1;
            Hit.text = "x" + hit_verre;
        }
        if ("plastique" == Obj && plastique)
        {
            hit_plastique += 1;
            Hit.text = "x" + hit_plastique;
        }
        if ("papier" == Obj && papier)
        {
            hit_papier += 1;
            Hit.text = "x" + hit_papier;
        }
    }
}
