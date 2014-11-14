using UnityEngine;
using System.Collections;

public class DumpsterCarouselScript : MonoBehaviour {

	public float carouselRotationSpeed = 0.1f;
	public GameObject[] dumpsters = new GameObject[3];
	public int _currentDumpsterIndex = 0;
	private GameObject Current;
	public float _currentYRotation = 0.0f;
	private int rotateDirection = 0;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < dumpsters.GetLength(0); i++)
		{
			GameObject d = GameObject.Instantiate(dumpsters[i],new Vector3(1,0,0),dumpsters[i].transform.rotation) as GameObject;
			d.transform.parent = this.transform;
			d.transform.RotateAround(Vector3.zero,Vector3.up,120 * i + 90);
		}
		_currentYRotation = this.transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.Q))
		{
			rotateDirection = 1;
			//this.transform.Rotate(new Vector3(0,-120,0));
			if(_currentDumpsterIndex == 0)
			{
				_currentDumpsterIndex = dumpsters.GetLength(0) - 1;
			}
			else
			{
				_currentDumpsterIndex--;
			}
			StartCoroutine("RotateCarousel");

		}
		
		if(Input.GetKeyDown(KeyCode.S))
		{
			rotateDirection = -1;
			if(_currentDumpsterIndex == dumpsters.GetLength(0) - 1)
			{
				_currentDumpsterIndex = 0;
			}
			else
			{
				_currentDumpsterIndex ++;
			}

			StartCoroutine("RotateCarousel");
		}
	}

	IEnumerator RotateCarousel()
	{
		/*this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
		                                          Quaternion.AngleAxis(rotateDirection * 120.0f * _currentDumpsterIndex,Vector3.up),
		                                          carouselRotationSpeed * Time.deltaTime);*/
		if(rotateDirection < 0)
		{
			while(transform.rotation.y >= _currentYRotation - 120 )
			{
				this.transform.Rotate(new Vector3(0,rotateDirection * 120 * (carouselRotationSpeed * Time.deltaTime)));
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			if(rotateDirection > 0)
			{
				while(transform.rotation.y <= _currentYRotation + 120 )
				{
					this.transform.Rotate(new Vector3(0,rotateDirection * 120 * (carouselRotationSpeed * Time.deltaTime)));
					yield return new WaitForEndOfFrame();
				}
			}
		}

		_currentYRotation = transform.rotation.y;
	}
}
