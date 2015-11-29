using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player;
	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate(){
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX,posY, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (player.);
	}
}

//hello 