using UnityEngine;
using System.Collections;

public class Tiles : MonoBehaviour {
	public GameObject tilePrefab;
	public int MapX = 10;
	public int MapY = 10;
	// Use this for initialization
	void Start () {
		GameObject go;
		for (int x = 0; x < MapX; x++) {
			for (int y = 0; y < MapY; y++) {
				go = (GameObject)Instantiate (tilePrefab, new Vector3 (x, y,0),Quaternion.identity);
				go.transform.parent = gameObject.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
