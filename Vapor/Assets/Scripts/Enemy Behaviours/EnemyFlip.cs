using UnityEngine;
using System.Collections;

public class EnemyFlip : MonoBehaviour {

	public float repeat;
	private bool facingLeft = true;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("patrol",2f,repeat);	//call function 'patrol' 2 seconds after start, and every 2 seconds
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void patrol(){
		//flip direction facing (left and right)
		facingLeft = !facingLeft;
		if (facingLeft) {
			transform.eulerAngles = new Vector2 (0, 0);
		} else {
			transform.eulerAngles = new Vector2 (0,180);
		}
	}
}
