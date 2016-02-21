using UnityEngine;
using System.Collections;

public class EnemyFlip : MonoBehaviour {

	public float repeat;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("patrol",2f,repeat);	//call function 'patrol' 2 seconds after start, and every 'repeat' seconds
}
	
	// Update is called once per frame
	void Update () {
	}

	void patrol(){
		if (transform.eulerAngles.y == 0) {
			transform.eulerAngles = new Vector2 (0, 180);
		} else {
			transform.eulerAngles = new Vector2 (0, 0);
		}
	}
}


