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
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            DumpsterImage.sprite = Dumpsters[0];
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DumpsterImage.sprite = Dumpsters[1];
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            DumpsterImage.sprite = Dumpsters[2];
        }*/
	
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
