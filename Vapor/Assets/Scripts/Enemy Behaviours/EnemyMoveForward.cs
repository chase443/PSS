using UnityEngine;
using System.Collections;

public class EnemyMoveForward : MonoBehaviour {
	
	public float speed = 3.0f;
	//private Vector2 speedVec = new Vector2 (speed,0);
	//private Vector2 accelVec = new Vector2 (0.1f, 0);

	private Vector2 seekTarget;


	void Start(){

		seekTarget = new Vector2(transform.position.x, transform.position.y);
		//Debug.Log (seekTarget);
		Debug.Log ("how am i here");
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (-speed * Time.deltaTime, 0, 0); //Enemy moves to the left at set speed
		//Debug.Log (seekTarget);
	}
}