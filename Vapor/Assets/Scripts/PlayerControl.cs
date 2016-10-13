using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public Transform jumpEnd_Point, interactEnd_Point; //points designating the end of the players jump, and end of players interact range
	public float moveSpeed = 5f;
	public LayerMask groundLayer, enemyLayer;
	public Transform spawnPoint;


	Animator animator;

	private bool _isGrounded, _canInteract, _isJumping;
	private float _jumpTime;
	private float _jumpDelay = 0.5f;
	private float _jumpForce = 330f;
	private float _playerHealth = 10f;
	
	RaycastHit2D interacted; //a variable type that stores a collider that was hit during linecast
	

	void Start(){
		DontDestroyOnLoad(transform.gameObject);
		animator = GetComponent<Animator> ();
	}

	void Update()
	{
		Movement(); 
		RaycastStuff(); 

	}

	void RaycastStuff()
	{
		Debug.DrawLine(transform.position, jumpEnd_Point.position, Color.magenta);
		Debug.DrawLine(transform.position, interactEnd_Point.position, Color.magenta);

		//_isGrounded returns true if jumpEnd point is interacting with "Ground"
		_isGrounded = Physics2D.Linecast (transform.position, jumpEnd_Point.position, LayerMask.GetMask("Ground"));

		//Checks if player is within interaction range with an enemy
		interacted = Physics2D.Linecast(transform.position, interactEnd_Point.position, LayerMask.GetMask("Enemy")); 

		if(interacted.collider != null)
		{
			//Debug.Log (interacted.collider.gameObject.layer);
			//Stores infromation about what player has interacted with
			interacted = Physics2D.Linecast(transform.position, interactEnd_Point.position, LayerMask.GetMask("Enemy")); 
			_canInteract = true; //since the linecase is touching the guard and we are in range, we can now interact
		}
		else
		{
			_canInteract = false; //if the linecast is not touching a guard, we cannot interact
		}

		//Press E to destroy interacted object
		if (Input.GetKeyDown (KeyCode.E) && _canInteract) {
			Destroy (interacted.collider.gameObject);
		}

		//Physics2D.IgnoreLayerCollision(10, 11); //Ignores collision between layer 10 (Player) and layer 11 (Guard)

		RaycastHit2D hitDoor = Physics2D.Linecast(transform.position, interactEnd_Point.position, LayerMask.GetMask("Exit")); 
		if (hitDoor.collider != null) {
			if (Input.GetKeyDown (KeyCode.E)) {
				Application.LoadLevel("Level2");
				transform.position = new Vector2(spawnPoint.position.x,spawnPoint.position.y);

			}	
		}

	}


	void Movement() //function that stores all the movement
	{
		//set speed variable in animator to transition to run animation 
		animator.SetFloat ("speed",Mathf.Abs(Input.GetAxis("Horizontal")));

		//move to the right
		if(Input.GetAxisRaw("Horizontal") > 0)
		{
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); 
			transform.eulerAngles = new Vector2(0, 0); //this sets the rotation of the gameobject (face right)
		}

		//move to the left
		if(Input.GetAxisRaw("Horizontal") < 0)
		{
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 180); //this sets the rotation of the gameobject (face left)
		}

		//jump
		if(Input.GetButtonDown ("Jump") && _isGrounded) // If the jump button is pressed and the player is grounded then the player jumps 
		{
			_jumpTime = _jumpDelay;
			animator.SetTrigger("jump");
			GetComponent<Rigidbody2D>().AddForce(transform.up * _jumpForce);
			_isJumping = true;
		}

		_jumpTime -= Time.deltaTime;
		if (_jumpTime <= 0 && _isGrounded && _isJumping) {
			animator.SetTrigger("land");
			_isJumping = false;
		}
	}

	public void detected(){
		_playerHealth--;
		//Debug.Log ("health: " + _playerHealth);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		int spikesLayer = 13;
		if (coll.gameObject.layer == spikesLayer) {
			Destroy (gameObject);
		}

		int enemyLayer = 11;
		if (coll.gameObject.layer == enemyLayer) {
			Destroy (gameObject);
		}
	}
}
