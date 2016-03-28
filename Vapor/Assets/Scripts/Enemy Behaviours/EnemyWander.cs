using UnityEngine;
using System.Collections;

public class EnemyWander : MonoBehaviour {

	public float speed = 3.0f;
	public float repeat;
	private bool movement;

	// Use this for initialization
	void Start () {
		startMovement ();
	}
	
	// Update is called once per frame
	void Update () {
		if (movement) {
			move ();
		}
	}

	public void seekPlayer(PlayerControl player){
		player.detected ();
		Transform playerTrans = player.GetComponent("Transform") as Transform;
		Debug.Log(playerTrans.transform.position.x);
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
		if (transform.eulerAngles.y == 0) {
			transform.eulerAngles = new Vector2 (0, 180);
		} else {
			transform.eulerAngles = new Vector2 (0, 0);
		}
	}


}
