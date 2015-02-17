using UnityEngine;
using System.Collections;

public class WasteDestroyerScript : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		//destroy uncaught the waste
		Destroy(col.gameObject);
	}
}
