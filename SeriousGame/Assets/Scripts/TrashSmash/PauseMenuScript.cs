using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

    private bool isActive = false;

    public GameObject PauseDialog;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseDialog.SetActive(!isActive);
            isActive = !isActive;
        }

	}

 public void OnButtonPress(string tag)
    {
        if(tag == "button1")
           Application.LoadLevel(Application.loadedLevel);
    }
}
