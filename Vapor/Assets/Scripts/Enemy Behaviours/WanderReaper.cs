using UnityEngine;
using System.Collections;

public class WanderReaper : MonoBehaviour {
	
	public Transform sightEnd_Point;
	public GameObject arrow; //visual for when player is spotted 

	private string wanderReaperState = "wanderState";
	private bool doPatrol = true;
	private float speed = 3.0f;
	private RaycastHit2D spotted; //has spotted player
	private PlayerControl player;
	
	private Vector2 wanderTarget;

	// Use this for initialization
	void Start () {
		//wanderTarget = new Vector2 (transform.position.x - 5f, transform.position.y);
		togglePatrol ();
	}
	
	// Update is called once per frame
	void Update () {
		Raycasting();
		Invoke (wanderReaperState, 0f);
	}

	private void seekState(){
		seekTarget (player.transform.position);

		if (!spotted) {
			Debug.Log ("leaving seek");
			UpdateState ("wanderState");
		}
	}

	private void seekTarget(Vector3 target){
		float moveDirX = target.x - transform.position.x;
		float moveDirY = target.y - transform.position.y;

		Vector2 direction = new Vector2 (moveDirX, moveDirY);
		direction.Normalize ();
		float velX = direction.x * speed * Time.deltaTime;
		
		transform.Translate (velX, 0, 0);
	}

	/**************************************
	 *Functions dealing with WANDER state *
	 **************************************/
	private void wanderState(){

		transform.Translate (-speed * Time.deltaTime, 0, 0); //Enemy moves to the left at set speed
		//seekTarget (wanderTarget);
		//float currX = Mathf.Abs (wanderTarget.x - transform.position.x);
		//Debug.Log (transform.eulerAngles);
		//if (currX < 1) {
			//transform.eulerAngles
//			transform.eulerAngles = new Vector2 (0, 180);
//		}

		//Debug.Log (currX);
		//activate I see you arrow
		if (spotted.collider != null) {
			arrow.SetActive (true);
			player = spotted.transform.gameObject.GetComponent<PlayerControl> ();
			if (player != null) {
				UpdateState ("seekState");
				player.detected ();
			}
		} else {
			arrow.SetActive (false);
		}
	}
	public void togglePatrol(){
		Debug.Log ("we togglin");
		if (doPatrol) {
			InvokeRepeating ("patrol",2f,2f);	//call function 'patrol' 2 seconds after start, and every 'repeat' seconds
		} else {
			CancelInvoke ("patrol");
		}
		doPatrol = !doPatrol;
	}

	void patrol(){
		Debug.Log ("patroled");
		if (transform.eulerAngles.y == 0) {
			transform.eulerAngles = new Vector2 (0, 180);
		} else {
			transform.eulerAngles = new Vector2 (0, 0);
		}
	}

	/**************************************
	 **************************************
	 **************************************/

	public void UpdateState(string newState){
		wanderReaperState = newState;
		
		switch (wanderReaperState) {
			case "wanderState":
				transform.eulerAngles = new Vector2 (0, 180);
				togglePatrol(); //turn on patrol
				Debug.Log ("you are now wander");
				break;
			case "seekState":
				togglePatrol(); //turn off patrol
				Debug.Log ("you are now seek");
				break;
			case "reset":
				Debug.Log ("you are now reset");
				break;
			default:
				break;
		}
	}
	void Raycasting(){
		Debug.DrawLine (transform.position, sightEnd_Point.position, Color.green);
		spotted = Physics2D.Linecast (transform.position, sightEnd_Point.position, LayerMask.GetMask("Player"));
	}

}
