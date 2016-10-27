using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour {
	float h;
	float v;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		h = Input.GetAxisRaw("Horizontal");
		v = Input.GetAxisRaw("Vertical");

		print (v + " || " + h);
		Vector2 pos = transform.position;
		Vector2 end = new Vector2 (transform.position.x + h, transform.position.y + v);
		transform.position = Vector2.Lerp (pos, end, 2f * Time.deltaTime);
	}
}
