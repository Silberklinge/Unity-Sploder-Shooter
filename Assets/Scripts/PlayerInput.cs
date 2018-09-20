using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public Rigidbody2D rb;
	public float movementForce;

	public GameObject projectileSpawner;
	public GameObject raySpawner;
	public GameObject missileSpawner;
	public GameObject botSpawner;
	public GameObject mortarSpawner;
	public GameObject mineSpawner;

	private Vector2 lookDirection;

	public List<KeyCode> moveForwardKeys;
	public List<KeyCode> moveBackwardKeys;
	public List<KeyCode> turnLeftKeys;
	public List<KeyCode> turnRightKeys;
	
	public List<KeyCode> fireProjectileKeys;
	public List<KeyCode> fireRayKeys;
	public List<KeyCode> fireMissileKeys;
	public List<KeyCode> fireBotKeys;
	public List<KeyCode> fireMortarKeys;
	public List<KeyCode> fireMineKeys;
	
	void Update() {
		Vector2 playerWorldPos = transform.position;
		Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		lookDirection = (mouseWorldPos - playerWorldPos).normalized;
		
		bool fireProjectiles = fireProjectileKeys.Exists(Input.GetKey);
		bool fireRay = fireRayKeys.Exists(Input.GetKey) && !fireProjectiles;
		bool fireMissiles = fireMissileKeys.Exists(Input.GetKey);
		bool fireBots = fireBotKeys.Exists(Input.GetKey) && !fireMissiles;
		
		if(projectileSpawner != null) projectileSpawner.SetActive(fireProjectiles);
		if(raySpawner != null) raySpawner.SetActive(fireRay);
		if(missileSpawner != null) missileSpawner.SetActive(fireMissiles);
		if(botSpawner != null) botSpawner.SetActive(fireBots);
	}

	void FixedUpdate() {		
		if(Input.GetMouseButton(0))
			rb.AddForce(lookDirection * movementForce);
		
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
