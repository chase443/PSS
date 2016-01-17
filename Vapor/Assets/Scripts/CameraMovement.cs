using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private GameObject player;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;


	private Vector2 velocity;
	private float smoothTimeY = 0.05f;
	private float smoothTimeX = 0.05f;

	private float cameraPosOffset = 8.66f;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		player = GameObject.FindGameObjectWithTag ("Player");
	}


	void FixedUpdate(){
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (Mathf.Clamp(posX,minX,maxX),Mathf.Clamp(posY,minY,maxY), transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (player.);
	}
}