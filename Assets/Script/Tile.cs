using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tile  {

	public int TileX;
	public int TileY;
	[System.NonSerialized]
	public Tile[] szomik = new Tile[8];

	public GameObject TileGO;
	public bool lathato = false;
	public enum talajfajta {kő, föld, üres, vas, fű};
	public talajfajta eztalajfajta;

	public Tile(int x, int y){
		TileX = x;
		TileY = y;


	}

}
