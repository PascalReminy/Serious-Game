using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour {

   
    private float seconds;
    private float minutes;
    private bool flag = false;
    private Text Info;

    public float timer = 120.0f;

	// Use this for initialization
	void Start () {
        Info = this.gameObject.GetComponent<Text>();
        Info.text = "Press Space";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || flag)
        {
            flag = true;
            timer -= Time.deltaTime;
            seconds = timer % 60;
            minutes = timer / 60;
            Info.text = (int)minutes + " : " + (int)seconds;

            
            if ((int)timer <= 0)
            {
                flag = false;
                Info.text = "GameOver";
            }
        }
            
	}
}
