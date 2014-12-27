using UnityEngine;
using System.Collections;
public enum WallType {Wall, LeftWall, RightWall, BrickWall, LeftBrickWall, RightBrickWall};


public class ChangeWallMaterialScript : MonoBehaviour {

	public Material wallMaterial;
	public Material leftWallMaterial;
	public Material rightWallMaterial;
	public Material brickWallMaterial;
	public Material leftBrickWallMaterial;
	public Material rightBrickWallMaterial;

	private WallType _wallType = WallType.Wall;
	// Use this for initialization
	void Start ()
	{
		//this.renderer.material = wallMaterial;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ChangeWallMaterial(WallType type)
	{
		switch (type)
		{
		case WallType.Wall:
			this.renderer.material = wallMaterial;
			break;
		case WallType.LeftWall:
			this.renderer.material = leftWallMaterial;
			break;
		case WallType.RightWall:
			this.renderer.material = rightWallMaterial;
			break;
		case WallType.BrickWall:
			this.renderer.material = brickWallMaterial;
			break;
		case WallType.LeftBrickWall:
			this.renderer.material = leftBrickWallMaterial;
			break;
		case WallType.RightBrickWall:
			this.renderer.material = rightBrickWallMaterial;
			break;
		default:
			this.renderer.material = wallMaterial;
			break;
		}
	}
}
