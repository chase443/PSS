using UnityEngine;
using System.Collections;

public class WanderReaper : MonoBehaviour {

	public float speed = 3.0f;
	public float repeat;
	public string wanderReaperState = "idle";

	private bool movement = false;
	private bool doSeek = false;
	private Vector2 velocity;
	private float smoothTimeX = 30.0f;

	private float minX_Offset=-0.5f;
	private float maxX_Offset= 0.5f;
	private Transform playerTrans;

	// Use this for initialization
	void Start () {
		startMovement ();
	}
	
	// Update is called once per frame
	void Update () {
		if (movement) {
			move ();
		}
		if (doSeek) {
			startSeek ();
		}
	}
	
	public void seekPlayer(PlayerControl player){
		player.detected ();
		playerTrans = player.GetComponent("Transform") as Transform;
		doSeek = true;
	}

	public void startSeek(){
		float posX = Mathf.SmoothDamp (transform.position.x, playerTrans.position.x, ref velocity.x, smoothTimeX);
		
		float minX = playerTrans.position.x + minX_Offset;
		float maxX = playerTrans.position.x + maxX_Offset;
		
		transform.position = new Vector3 (Mathf.Clamp(posX,minX,maxX),transform.position.y, transform.position.z);
		
		Debug.Log(playerTrans.position.x);
	}
	
	public void stopMovement(){
		CancelInvoke ("patrol");
		movement = false;
	}
	
	public void startMovement(){
		movement = true;
		InvokeRepeating ("patrol",2f,repeat);	//call function 'patrol' 2 seconds after start, and every 'repeat' seconds
	}
	
	/*
	 * Function to move to the left at SPEED
	 */
	void move(){
		transform.Translate (-speed * Time.deltaTime, 0, 0); //Enemy moves to the left at set speed
	}
	
	/*
	 * Function to flip direction from left to right 
	 */
	void patrol(){
		Debug.Log ("patroled");
		if (transform.eulerAngles.y == 0) {
			transform.eulerAngles = new Vector2 (0, 180);
		} else {
			transform.eulerAngles = new Vector2 (0, 0);
		}
	}

	/*
	 * Function to update the state of the Wander Reaper 
	 */
	public void UpdateState(string state, PlayerControl player){
		wanderReaperState = state;
		
		switch (wanderReaperState) {
		case "idle":
			Debug.Log ("you are now idle");
			break;
		case "seek":
			stopMovement();
			seekPlayer(player);
			break;
		case "reset":
			Debug.Log ("you are now reset");
			break;
		default:
			break;
		}
	}
}
