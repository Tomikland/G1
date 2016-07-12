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
	//public Tile oldTile;
	// Use this for initialization
	void Start () {
		
		TilesScript = TilesGO.GetComponent<Tiles> ();
		Mm = MmGO.GetComponent<MouseManager> ();


		GeneratePoolTile (200);
	}
	
	// Update is called once per frame
	void Update () {

		if (time > Interval){
			time = 0f;

		CurrSeenTiles.Clear (); 
		TileBelowCam = Mm.GetTileAtWorldCoord (Camera.main.transform.position.x, Camera.main.transform.position.y);
	    TileBelowCamX = TileBelowCam.TileX;
		TileBelowCamY = TileBelowCam.TileY;

		CameraPoolSize = Mathf.RoundToInt (Camera.main.orthographicSize* cameraPoolSizeMultiplier);
		for (int x = TileBelowCamX - CameraPoolSize; x < TileBelowCamX + CameraPoolSize; x++) {
			for (int y = TileBelowCamY - CameraPoolSize; y < TileBelowCamY + CameraPoolSize; y++) {
				if(x >= 0 && x < TilesScript.MapX  &&  y >= 0 && y < TilesScript.MapY){
					
					CurrSeenTiles.Add (TilesScript.ossztile [x, y]);

					if(AssignedTiles.Contains(TilesScript.ossztile [x, y]) == false){
						//Debug.Log ("Assigning tile");
							if (AssignPoolTileToTile (x, y)) {
								AssignedTiles.Add (TilesScript.ossztile [x, y]); 

							} else {
								GeneratePoolTile (5);
							}
					}
				}
			}
		  }
		foreach (GameObject GO in AllPoolTiles) {
			PoolingTile GOScript = GO.GetComponent<PoolingTile> ();
				if(CurrSeenTiles.Contains(GOScript.myTile) == false && PendingPoolTiles.Contains(GO) == false){
				ReturnTileToPool (GO, GOScript);
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
		bool WasSuccesful = true;
		if(PendingPoolTiles.Count == 0){
			WasSuccesful = false;
			return false;
		}
		poolTile = PendingPoolTiles [0];
		PTScript = poolTile.GetComponent<PoolingTile> ();


		if (PTScript.myTile != null && PTScript.myTile != TilesScript.ossztile[x,y]) {
			oldTile = PTScript.myTile;
			AssignedTiles.Remove (oldTile);
		}
		PendingPoolTiles.Remove(poolTile);


		tilePos = new Vector3 (x, y, 0);
		PTScript.TileX = x;
		PTScript.TileY = y;
		poolTile.transform.position = tilePos;
		PTScript.myTile = TilesScript.ossztile[x,y];
		PTScript.myTile.TileGO = poolTile;
		PTScript.isAssigned = true;
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



}
