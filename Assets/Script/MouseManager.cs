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
			
			WorldMousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y ,0));
			posX = WorldMousePos.x;
			PosY = WorldMousePos.y;
			Tile theTile = GetTileAtWorldCoord (posX, PosY);
			if(theTile != null){
			Debug.Log ("Találtam egy négyzetet : "+theTile.TileX+"_"+theTile.TileY);

				if (theTile.TileGO != null) {
					//SpriteRenderer Spr = theTile.TileGO.GetComponentInChildren<SpriteRenderer> ();
					//Spr.color = Color.blue;

				} 
			}else {
				Debug.Log ("ott nincs is négyzet!");
				
		}
	}
	}
	public Tile GetTileAtWorldCoord (float x, float y){
		Tile theTile;
		int TileX;
		int TileY;

		TileX = Mathf.RoundToInt (x);
		TileY = Mathf.RoundToInt (y);
		if (TileX>0 && TileX<TilesScript.MapX && TileY>0 && TileY<TilesScript.MapY){

		theTile = TilesScript.ossztile [TileX, TileY];
		return theTile;

		}else{
			return null;
		}

}
}