using UnityEngine;
using System.Collections;

public class EnemySpotPlayer : MonoBehaviour {

	public Transform sightEnd_Point;
	public GameObject arrow; //visual for when player is spotted 
	//public float repeat;

	//private bool facingLeft = true;
	private RaycastHit2D spotted; //has spotted player

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("patrol",2f,repeat);	//call function 'patrol' 2 seconds after start, and every 2 seconds
	}

	void Update () {
		Raycasting();
		Behaviours();
	}

	//Cast ray for enemy guards line of sight
	void Raycasting(){
		Debug.DrawLine (transform.position, sightEnd_Point.position, Color.green);
		spotted = Physics2D.Linecast (transform.position, sightEnd_Point.position, LayerMask.GetMask("Player"));
	}

	void Behaviours(){
		//activate I see you arrow
		if (spotted.collider != null) {
			arrow.SetActive(true);
		} else {
			arrow.SetActive(false);
		}

		//affect player
		if (spotted.collider != null) {
			PlayerControl player = spotted.transform.gameObject.GetComponent<PlayerControl>();
			if(player != null){
				player.detected();
			}
		}

	}
/*
	void patrol(){
		//flip direction facing (left and right)
		facingLeft = !facingLeft;
		if (facingLeft) {
			transform.eulerAngles = new Vector2 (0, 0);
		} else {
			transform.eulerAngles = new Vector2 (0,180);
		}
	}*/
}
