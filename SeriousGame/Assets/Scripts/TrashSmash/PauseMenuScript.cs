using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {


    public GameObject PauseDialog;



    void PressEscape(bool isActive)
    {
        PauseDialog.SetActive(!isActive);
    }

    public void OnButtonPress(string tag)
    {
        if (tag == "TrashSmash")
            Application.LoadLevel("TrashSmash");
        if (tag == "Menu")
            Application.LoadLevel("Menu");
    }
}
