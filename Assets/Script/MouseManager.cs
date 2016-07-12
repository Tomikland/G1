using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {
	public GameObject TilesGO;
	Tiles TilesScript;

	// Use this for initialization
	void Start () {
		TilesScript = TilesGO.GetComponent<Tiles> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 WorldMousePos;
		float posX;
		float PosY;
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Running");
			WorldMousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y ,0));
			posX = WorldMousePos.x;
			PosY = WorldMousePos.y;
			Debug.Log (GetTileAtWorldCoord (posX,PosY).TileGO.name);
		}
	}
	public Tile GetTileAtWorldCoord (float x, float y){
		Tile theTile;
		int TileX;
		int TileY;

		TileX = Mathf.RoundToInt (x);
		TileY = Mathf.RoundToInt (y);

		theTile = TilesScript.ossztile [TileX, TileY];
		return theTile;

	}
}
