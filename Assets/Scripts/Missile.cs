using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Missile : Projectile {

	public Rigidbody2D rb;
	public Transform target;
	public float steering;

	void FixedUpdate() {
		if(target) {
			var currentHeading = transform.right;
			var desiredHeading = (target.position - transform.position).normalized;
			var torque = Vector3.Cross(currentHeading, desiredHeading).z;
			rb.AddTorque(steering * torque);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Transform colliderTransform = collider.transform.root;
		if(colliderTransform == owner.transform || colliderTransform == transform || target != null)
			return;
		target = collider.transform;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		Destroy(gameObject);
	}
}
