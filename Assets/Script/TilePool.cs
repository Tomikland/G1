using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePool : MonoBehaviour {
	public GameObject TilesGO;
	public Tiles TilesScript;
	public GameObject MmGO;
	MouseManager Mm;

	public List<GameObject> PendingPoolTiles = new List<GameObject> ();
	public List<Tile> CurrSeenTiles = new List<Tile> ();
	public List<Tile> AssignedTiles = new List<Tile> ();
	public List<GameObject> AllPoolTiles = new List<GameObject> ();
	public Tile TileBelowCam;
	public int TileBelowCamX;
	public int TileBelowCamY;
	public float cameraPoolSizeMultiplier = 2;
	public GameObject tilePrefab;
	float time;
	public float Interval = 5f;
	public int CameraPoolSize;
	public Sprite kőSprite;
	public Sprite földSprite;
	public Sprite fűSprite;
	//public Tile oldTile;
	// Use this for initialization
	void Start () {
		
		TilesScript = TilesGO.GetComponent<Tiles> ();
		Mm = MmGO.GetComponent<MouseManager> ();


		GeneratePoolTile (250);
	}
	
	// Update is called once per frame
	void Update () {

		Tile currLoopTile;
		if (time >= Interval){
			time = 0f;
			foreach (GameObject GO in AllPoolTiles) {
				PoolingTile GOScript = GO.GetComponent<PoolingTile> ();
				if(CurrSeenTiles.Contains(GOScript.myTile) == false && PendingPoolTiles.Contains(GO) == false){
					ReturnTileToPool (GO, GOScript);
				}
			}
		
		TileBelowCam = Mm.GetTileAtWorldCoord (Camera.main.transform.position.x, Camera.main.transform.position.y);
			if (TileBelowCam != null) {
				TileBelowCamX = TileBelowCam.TileX;
				TileBelowCamY = TileBelowCam.TileY;
				CurrSeenTiles.Clear (); 
				CameraPoolSize = Mathf.RoundToInt (Camera.main.orthographicSize * cameraPoolSizeMultiplier);
				for (int x = TileBelowCamX - CameraPoolSize; x < TileBelowCamX + CameraPoolSize; x++) {
					for (int y = TileBelowCamY - CameraPoolSize; y < TileBelowCamY + CameraPoolSize; y++) {
						if (x >= 0 && x < TilesScript.MapX && y >= 0 && y < TilesScript.MapY) {
							currLoopTile = TilesScript.ossztile [x, y];
					
							CurrSeenTiles.Add (currLoopTile);
							if(currLoopTile.TileGO != null && PendingPoolTiles.Contains(currLoopTile.TileGO)){
							PendingPoolTiles.Remove (currLoopTile.TileGO);
							}
							if (AssignedTiles.Contains (currLoopTile) == false) {
								//Debug.Log ("Assigning tile");
								if (AssignPoolTileToTile (x, y)) {
									AssignedTiles.Add (currLoopTile); 

								} else {
									GeneratePoolTile (5);
								}
							}
						}
					}
				}

			}
		

		}
		time += Time.deltaTime;
	}
	bool AssignPoolTileToTile(int x,int y){
		Tile oldTile;
		GameObject poolTile;
		PoolingTile PTScript;
		//Tile DataTile;
		Vector3 tilePos;
		if(PendingPoolTiles.Count == 0){
			
			return false;
		}
		poolTile = PendingPoolTiles [0];
		PTScript = poolTile.GetComponent<PoolingTile> ();
		oldTile = PTScript.myTile;
		PendingPoolTiles.Remove (poolTile);

		if (PTScript.myTile != null) {
			oldTile = PTScript.myTile;
			if (oldTile == TilesScript.ossztile [x, y]) {
				return true;



			} 
			else{
				AssignedTiles.Remove (oldTile);
			}
		}

			
		

		tilePos = new Vector3 (x, y, 0);
		PTScript.TileX = x;
		PTScript.TileY = y;
		poolTile.transform.position = tilePos;
		PTScript.myTile = TilesScript.ossztile[x,y];
		PTScript.myTile.TileGO = poolTile;
		PTScript.isAssigned = true;
		poolTile.GetComponentInChildren<SpriteRenderer> ().sprite = GetSpriteForTile (poolTile, PTScript);
		return true;

	}
	void GeneratePoolTile (int amt){
		GameObject GO;
		PoolingTile Script;
		Debug.Log("Generáltam "+amt+" további négyzetet.");
		for (int i = 0; i < amt; i++) {

			GO = (GameObject)Instantiate (tilePrefab, new Vector3 (0,0,0),Quaternion.identity);
			GO.transform.parent = gameObject.transform;
			Script = GO.GetComponent<PoolingTile> ();
			Script.TilePoolGO = this.gameObject;
			Script.PoolScript = this;
			PendingPoolTiles.Add (GO);
			AllPoolTiles.Add (GO);
		}
	}
	public void ReturnTileToPool(GameObject GO, PoolingTile tScript){

		PendingPoolTiles.Add (GO);
		tScript.isAssigned = false;


	
	}
	Sprite GetSpriteForTile(GameObject t, PoolingTile scr){
		if (scr.myTile.eztalajfajta == Tile.talajfajta.kő) {
			return kőSprite;
		} else if (scr.myTile.eztalajfajta == Tile.talajfajta.föld) {
			return földSprite;
		} else if (scr.myTile.eztalajfajta == Tile.talajfajta.fű) {
			return fűSprite;
		} else {
			Debug.LogError ("GetSpriteForTile");
			return kőSprite;
		}
	}



}
