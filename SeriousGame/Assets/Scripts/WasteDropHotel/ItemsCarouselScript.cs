using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemsCarouselScript : MonoBehaviour {

	public float carouselRotationSpeed = 0.1f;	//carousel rotation speed
	public float carouselRadius = 1.0f;	//carousel radius
	public GameObject[] carouselItemsPrefabs = new GameObject[3]; //objects to put inside the carousel
	private List<GameObject> _carouselItems = new List<GameObject>();	

	public KeyCode rotateLeftKeyCode = KeyCode.LeftArrow;	//the key to left-rotate the carousel
	public KeyCode rotateRightKeyCode = KeyCode.RightArrow;	//the key to right-rotate the carousel

	public int _selectedItemIndex = 0;	//index of selected object, the nearest object
	public int _wantedItemIndex = 0;
	public float _rotationAmount = 0.0f; //from 0 to 1
	private float _angleBetweenCarouselItems;	//
	public float _currentRotationY = 0.0f;
	private int _rotationDirection = 0;
	private bool _isRotating = false;

	// Use this for initialization
	void Start () {
		_currentRotationY = transform.rotation.eulerAngles.y;
		_wantedItemIndex = _selectedItemIndex ;
		_angleBetweenCarouselItems = 360 / carouselItemsPrefabs.GetLength(0);	//compute the angle between items in degree
		for(int i = 0; i < carouselItemsPrefabs.GetLength(0); i++)
		{
			GameObject itm = Instantiate(carouselItemsPrefabs[i],
			                             new Vector3(carouselRadius,0,0),
			                             carouselItemsPrefabs[i].transform.rotation) as GameObject;
			itm.transform.parent = this.transform;	// make items parent of the carousel
			itm.transform.RotateAround(Vector3.zero,Vector3.up,_angleBetweenCarouselItems * i + 90);
			_carouselItems.Add(itm);
		}
	}


	void FixedUpdate ()
	{

		if(_selectedItemIndex != _wantedItemIndex && !_isRotating)
		{
			_isRotating = true;
			StartCoroutine("RotateCarousel");
		}
		
	}
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(rotateLeftKeyCode) && _wantedItemIndex == _selectedItemIndex)
		{

			if(_wantedItemIndex == 0)
			{
				_wantedItemIndex = _carouselItems.Count - 1;
			}
			else
			{
				_wantedItemIndex--;
			}
			_rotationDirection = -1;
		}
		
		if(Input.GetKeyDown(rotateRightKeyCode) && _wantedItemIndex == _selectedItemIndex)
		{
			if(_wantedItemIndex ==  _carouselItems.Count - 1)
			{
				_wantedItemIndex = 0;
			}
			else
			{
				_wantedItemIndex ++;
			}
			_rotationDirection = 1;
		}
	}

	IEnumerator RotateCarousel()
	{
		var toto = 0.0f;
		while (_rotationAmount <= _angleBetweenCarouselItems)
		{
			toto = toto + 1 * Time.deltaTime; 
			this.transform.RotateAround(Vector3.zero,Vector3.up,_rotationDirection * _angleBetweenCarouselItems * carouselRotationSpeed * Time.deltaTime);
			_rotationAmount = _rotationAmount + _angleBetweenCarouselItems * carouselRotationSpeed * Time.deltaTime;

			yield return new WaitForSeconds(Time.deltaTime);
			
		}
		this.transform.RotateAround(Vector3.zero,
		                            Vector3.up,
		                            _rotationDirection * (_angleBetweenCarouselItems - _rotationAmount));
		_rotationAmount = 0.0f;
		_selectedItemIndex = _wantedItemIndex;
		_currentRotationY = transform.rotation.eulerAngles.y;
		_isRotating = false;


	}

	public float GaussianFunction(float x, float sigma, float mu)
	{
		return ( 1 / (sigma * Mathf.Sqrt(2 * Mathf.PI) ) ) * Mathf.Exp( -1 / 2 * Mathf.Pow(( x - mu) / sigma, 2));
	}
}
