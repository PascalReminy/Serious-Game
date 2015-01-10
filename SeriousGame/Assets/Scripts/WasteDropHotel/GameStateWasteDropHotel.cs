using UnityEngine;
using System.Collections;

public class GameStateWasteDropHotel{

	protected GameStateWasteDropHotel(){}

	private static GameStateWasteDropHotel _instance = null;

	public static GameStateWasteDropHotel Instance {
		get
		{
			if(_instance == null)
			{
				_instance = new GameStateWasteDropHotel();
			}
			return _instance;
		}

	}

	private float _hotelScale = 2.0f;

	public float HotelScale {
		get {
			return _hotelScale;
		}
		set {
			_hotelScale = value;
		}
	}

}
