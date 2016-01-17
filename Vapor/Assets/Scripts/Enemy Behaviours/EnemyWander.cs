using UnityEngine;
using System.Collections;

public class EnemyWander : MonoBehaviour {
	
	public float speed = 3.0f;
	
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-speed * Time.deltaTime, 0, 0); //Enemy moves to the left at set speed
	}
}