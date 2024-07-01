using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeRoll : MonoBehaviour {
	public Transform cubeMesh;
	public bool rollForever = false; // does the cube keep moving in a direction until hitting a wall?
	private float rollSpeed = 400; // how fast cube rolls
	private bool isMoving = false;
	private RaycastHit hit;
	public Vector3 pivot;
	private float cubeSize = 1; // Block cube size
	public bool canMove;
	//public static int steps;
	[SerializeField] float teleportcooldown = 1.0f;
	private float lastteleporttime; // the last time player teleported
	public GameObject deatheffect;
	public enum CubeDirection {none, left, up, right, down}; //type of class
	public CubeDirection direction = CubeDirection.none;
	public Vector3 respawnPosition;
	Quaternion lastrotation;
	void Start() {
		//steps = 500;
		respawnPosition = transform.position;
		canMove = true;
		lastrotation = Quaternion.identity;
	}

	void Update() {
		var respawn = Physics.OverlapBox(transform.position, transform.localScale * .5f, Quaternion.identity, LayerMask.GetMask("Platform")); // checks if cube is on a platform to set as a respawn point
		if (respawn.Length > 0) respawnPosition = respawn[0].transform.position; // set respawn point
		// listen for input if player is not moving
		if (direction == CubeDirection.none) {
			CameraFollow cam = FindObjectOfType<CameraFollow>(); // get camera so can switch camera targets
			if (canMove && gameObject.tag == "RedPlayer") { // move red cube with WASD
				// set direction of movement to the corresponding directional buttons.
				if (Input.GetKeyDown(KeyCode.D)) {
					direction = CubeDirection.right;
					cam.target = gameObject; // camera follows current player
					//DeductStepCount();
				}
				else if (Input.GetKeyDown(KeyCode.A)) {
					direction = CubeDirection.left;
					cam.target = gameObject;
					//DeductStepCount();
				}
				else if (Input.GetKeyDown(KeyCode.W)) {
					direction = CubeDirection.up;
					cam.target = gameObject;
					//DeductStepCount();
				}
				else if (Input.GetKeyDown(KeyCode.S)) {
					direction = CubeDirection.down;
					cam.target = gameObject;
					//DeductStepCount();
				}
			}
			else if (canMove && gameObject.tag == "BluePlayer") { // move blue cube with arrow keys
				if (Input.GetKeyDown(KeyCode.RightArrow)) {
					direction = CubeDirection.right;
					cam.target = gameObject;
					//DeductStepCount();
				}
				else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
					direction = CubeDirection.left;
					cam.target = gameObject;
					//DeductStepCount();
				}
				else if (Input.GetKeyDown(KeyCode.UpArrow)) {
					direction = CubeDirection.up;
					cam.target = gameObject;
					//DeductStepCount();
				}
				else if (Input.GetKeyDown(KeyCode.DownArrow)) {
					direction = CubeDirection.down;
					cam.target = gameObject;
					//DeductStepCount();
				}
			}
		}
		else {
			// if this is our first frame moving
			if(!isMoving) {
				// check if anything is blocking the way
				if(CheckCollision(direction)) {
					// if we are hitting a push block move the block.
					if (hit.collider.gameObject.GetComponent<PushBlock>()) {
						hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - hit.collider.transform.position).normalized, 1);
					}
					isMoving = false;
					direction = CubeDirection.none;
					return;
				} 
				else {
					CalculatePivot();
					//DeductStepCount();
					isMoving = true;
				}
			}
			
			// rotates cube
			switch(direction) {
				case CubeDirection.right:
					cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastrotation,cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.left:
					cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastrotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.up:
					cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastrotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.down:
					cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastrotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
			}
		}
		if(transform.position.y <= -20) {
			transform.position = respawnPosition + new Vector3(0, respawnPosition.y + 20, 0); // sets the cube 20 units on the y-axis above its respawn position when it falls off the platform.
		}
	}

	void ResetPosition() {
		//sets position and rotation to 0, rounding the global position to a .5 value so it stays on the grid.
		//cubeMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
		lastrotation = cubeMesh.transform.rotation = Quaternion.Euler(Mathf.Round(cubeMesh.transform.rotation.eulerAngles.x / 90) * 90, Mathf.Round(cubeMesh.transform.rotation.eulerAngles.y / 90) * 90, Mathf.Round(cubeMesh.transform.rotation.eulerAngles.z / 90) * 90);
		transform.position = new Vector3(Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f, transform.position.y, Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f);
		cubeMesh.transform.localPosition = Vector3.zero;
		// sets moving to false
		isMoving = false;
		// check if there is anything in the direction we just moved in.
		if(CheckCollision(direction) && hit.collider != null) {
			// if the object in our way is a push block, we push it.
			if(hit.collider.gameObject.GetComponent<PushBlock>()) {
				hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - hit.collider.transform.position).normalized, 1);
			}
		}
		// terminate mvmt unless rollforever is on
		if (!rollForever)
			direction = CubeDirection.none;
	}

	void CalculatePivot() {
		switch(direction) {
			case CubeDirection.right:
				pivot = new Vector3(1, -1, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, -1, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, -1, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, -1, -1);
				break;
		}

		// Calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);
		AudioManager.instance.PlaySFX(AudioManager.instance.flopsound); // Play the flop sound 
	}

	bool CheckCollision(CubeDirection direction) {
		switch(direction) {
			case CubeDirection.right:
				Physics.Linecast(transform.position, transform.position + transform.right* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* 1, Color.black);
				break;
			case CubeDirection.left:
				Physics.Linecast(transform.position, transform.position + transform.right* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* -1, Color.black);
				break;
			case CubeDirection.up:
				Physics.Linecast(transform.position, transform.position + transform.forward* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* 1, Color.black);
				break;
			case CubeDirection.down:
				Physics.Linecast(transform.position, transform.position + transform.forward* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* -1, Color.black);
				break;
		}

		if(hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) {
			return false;
		} 
		else {
			return true;
		}
	}
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Teleporter>() && Time.time - lastteleporttime >= teleportcooldown) {
			transform.position = other.GetComponent<Teleporter>().Teleport() + new Vector3(0,1,0); // teleports cube to teleporter destination with slight offset to prevent cube from falling
			AudioManager.instance.PlaySFX(AudioManager.instance.teleportsound);
			lastteleporttime = Time.time;
		}
    }
}