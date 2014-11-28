using UnityEngine;
using System.Collections;

public class ObjScript : MonoBehaviour {

    private int _rot;

    void Start() 
    {
       _rot = Random.Range(0, 360);
    }
 
	// Update is called once per frame
	void Update () 
    {

        transform.Translate(new Vector3(0.0f,-3f,0.0f) * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, _rot * Time.deltaTime);

	
	}
}
