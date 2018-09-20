using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Projectile {

	public float range;
	public float airTime;
	public float velocityBias;
	public AnimationCurve heightOverTime;
	public Transform sprite;
	public float radius;
	public float knockback;
	
	private Vector2 launchPoint;
	private Vector2 landPoint;
	private float creationTime;
	
	void Start() {
		creationTime = Time.time;
	
		launchPoint = transform.position;
		landPoint = launchPoint + (Vector2)(transform.right * range);
		
		if(owner) {
			Rigidbody2D ownerBody = owner.GetComponent<Rigidbody2D>();
			if(ownerBody) {
				Vector2 forwardVelocity = Vector3.Project(ownerBody.velocity, transform.right);
				landPoint += forwardVelocity * velocityBias;
			}
		}
		
		sprite.rotation = Quaternion.identity;
	}
	
	void Update() {
		float lifespanNormalized = Mathf.Clamp01((Time.time - creationTime) / airTime);
		transform.position = Vector2.Lerp(launchPoint, landPoint, lifespanNormalized);
		transform.localScale = Vector3.one * heightOverTime.Evaluate(lifespanNormalized);
		sprite.rotation = Quaternion.identity;
	}
	
	void FixedUpdate() {
		if(Time.fixedTime < creationTime + airTime)
			return;
		
		Collider2D[] victims = Physics2D.OverlapCircleAll(landPoint, radius);
		
		for(int i = 0; i < victims.Length; i++) {
			Rigidbody2D r = victims[i].attachedRigidbody;
		}
	}
}
