using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChooseDumpsterScript : MonoBehaviour {

    public Sprite[] Dumpsters = new Sprite [3];

    private Image DumpsterImage; 

	// Use this for initialization
	void Start () {
        DumpsterImage = this.GetComponent<Image>();
	}
	

    void changeDumpsterSprite (string type)
    {
        if(type == "papier")
            DumpsterImage.sprite = Dumpsters[0];
        else if(type == "plastique")
            DumpsterImage.sprite = Dumpsters[1];
        else if(type == "verre")
            DumpsterImage.sprite = Dumpsters[2];

    }
}
