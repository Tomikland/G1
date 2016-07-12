using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	Vector3 menniri;
	float sebi = 2f;
	float gsebi = 2f;
	public GameObject tilesGO;
	public Tiles tScript;
	// Use this for initialization

	void Start () {
		tScript = tilesGO.GetComponent<Tiles> ();
		transform.position = new Vector3 (tScript.MapX / 2 - 0.5f, tScript.MapY / 2 - 0.5f, -5);

	}
	
	// Update is called once per frame
	void Update () {
		menniri = transform.position;

		menniri.x = Mathf.Clamp(menniri.x + Input.GetAxis ("Horizontal") * sebi * Time.deltaTime*Camera.main.orthographicSize,0,tScript.MapX-1);
		menniri.y = Mathf.Clamp(menniri.y + Input.GetAxis ("Vertical") * sebi * Time.deltaTime*Camera.main.orthographicSize,0,tScript.MapY-1);


		transform.position = menniri; 

		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize + -Input.GetAxis ("Mouse ScrollWheel") * gsebi * Time.deltaTime*Mathf.Pow(Camera.main.orthographicSize,2),2,6);
	}

}
