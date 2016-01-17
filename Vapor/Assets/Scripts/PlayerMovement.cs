using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 3.0f;
	public float gravity = -5f;
	public float jumpSpeed = 10f;
	private bool _isGrounded = true;

	private float _vertSpeed;
	public float terminalVelocity = -10f;

	private CharacterController _charController;
	private ControllerColliderHit _contact;

	// Use this for initialization
	void Start () {
		_charController = GetComponent<CharacterController> ();
		_vertSpeed = gravity;
	}
	
	// Update is called once per frame
	void Update () {
		/*bool hitGround = false;
		RaycastHit hit;
		if(_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit){
			float checked = (_charController
		} */

		float deltaX = Input.GetAxis ("Horizontal") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, 0);
		movement = Vector3.ClampMagnitude (movement, speed);

		if (_charController.isGrounded) {
			if (Input.GetButtonDown ("Jump")) {
				_vertSpeed = jumpSpeed;
				_isGrounded = false;
			} else {
				_vertSpeed = -1.5f;
			}
		} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if(_vertSpeed < terminalVelocity){
				_vertSpeed = terminalVelocity;
			}		
		}


		movement.y = _vertSpeed;

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_charController.Move (movement);

	}
}
