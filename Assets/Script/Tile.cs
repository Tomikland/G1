using UnityEngine;
using System.Collections;

public class Tile  {

	public int TileX;
	public int TileY;
	public Tile[] szomik = new Tile[8];
	public GameObject TileGO;
	public bool lathato = false;
	public enum talajfajta {föld, üres, vas};
	talajfajta eztalajfajta;

	public Tile(int x, int y, GameObject go){
		TileX = x;
		TileY = y;
		TileGO = go;
	}

}
