using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePool : MonoBehaviour {
	public GameObject TilesGO;
	Tiles TilesScript;
	public List<GameObject> PendingPoolTiles = new List<GameObject>();
	// Use this for initialization
	void Start () {
		TilesScript = TilesGO.GetComponent<Tiles> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void AssignPoolTileToTile(int x,int y){
		
		GameObject poolTile;
		PoolingTile PTScript;
		//Tile DataTile;
		Vector3 tilePos;

		poolTile = PendingPoolTiles [0];
		PendingPoolTiles.Remove(poolTile);
		PTScript = poolTile.GetComponent<PoolingTile> ();

		tilePos = new Vector3 (x, y, 0);
		PTScript.TileX = x;
		PTScript.TileY = y;
		poolTile.transform.position = tilePos;
		PTScript.myTile = TilesScript.ossztile[x,y];
	}



}
