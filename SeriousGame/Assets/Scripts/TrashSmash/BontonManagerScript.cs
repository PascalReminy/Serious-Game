using UnityEngine;
using System.Collections;

public class BontonManagerScript : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void OnButtonPress(string tag)
    {
        if (tag == "WasteDropHotel")
            Application.LoadLevel("WasteDropHotel");
        if (tag == "TrashSmash")
            Application.LoadLevel("TrashSmash");
        if (tag == "Menu")
            Application.LoadLevel("Menu");
        if (tag == "Quit")
            Application.Quit();
    }
}
