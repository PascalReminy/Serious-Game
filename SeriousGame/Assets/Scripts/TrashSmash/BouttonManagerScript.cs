using UnityEngine;
using System.Collections;

public class BouttonManagerScript : MonoBehaviour
{
    private bool StateOfCreditButton;

    public GameObject ButtonWasteDropHotel;
    public GameObject ButtonTrashSmash;
    public GameObject CreditText;

    void Start()
    {
        CreditText.SetActive(false);
    }

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
        if (tag == "Credit")
        {
            if (!StateOfCreditButton)
            {
                StateOfCreditButton = true;
                ButtonWasteDropHotel.SetActive(false);
                ButtonTrashSmash.SetActive(false);
                CreditText.SetActive(true);
            }
            else
            {
                StateOfCreditButton = false;
                ButtonWasteDropHotel.SetActive(true);
                ButtonTrashSmash.SetActive(true);
                CreditText.SetActive(false);
            }
        }
    }
}
