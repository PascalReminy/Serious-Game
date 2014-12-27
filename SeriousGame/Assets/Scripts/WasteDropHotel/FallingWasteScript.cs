using UnityEngine;
using System.Collections;

public enum WasteType {Plastic, Glass, Paper}

public class FallingWasteScript : MonoBehaviour {

	public WasteType type = WasteType.Paper;
	public float minFallingSpeed = 1.0f;
	public float maxFallingSpeed = 4.0f;

	private float _fallingSpeed;
	private float _rotationAngle;
	// Use this for initialization
	void Start ()
	{
		this._rotationAngle = Random.Range(90, 180);
		this._fallingSpeed = Random.Range(minFallingSpeed, maxFallingSpeed);
	}
	
	// Update is called once per frame
	void Update ()
	{
		var currentPostion = this.transform.position;
		this.transform.position = currentPostion - Vector3.up * this._fallingSpeed * Time.deltaTime;
		this.transform.Rotate(0f, 0f, _rotationAngle * Time.deltaTime);

	}


}
