using UnityEngine;
using System.Collections;

public class InfoScript : MonoBehaviour {

    public float timer = 120.0f;
    private float seconds;
    private float minutes;
    private bool flag = false;
    public Camera HUD;
    public float _originalRatio = 16f / 9f;

	// Use this for initialization
	void Start () {
        guiText.text = "Press Space";
        this.transform.localPosition =
            new Vector3(
                this.transform.localPosition.x * HUD.aspect / _originalRatio,
                this.transform.localPosition.y * HUD.aspect / _originalRatio,
                this.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || flag)
        {
            flag = true;
            timer -= Time.deltaTime;
            seconds = timer % 60;
            minutes = timer / 60;
            guiText.text = (int)minutes + " : " + (int)seconds;

            if ((int)timer <= 0)
            {
                flag = false;
            }
        }
	
	}
}
