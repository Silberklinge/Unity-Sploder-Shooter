using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {

	public float maximumVelocity;
	public float maximumAngularVelocity;
	public Transform target;
	public Rigidbody2D body;
	
	public void turnLeft() {
		body.AddTorque(-body.mass * body.angularDrag * maximumAngularVelocity);
	}
	
	public void turnRight() {
		body.AddTorque(body.mass * body.angularDrag * maximumAngularVelocity);
	}
	
	public void moveForward() {
		body.AddRelativeForce(new Vector2(0, body.mass * body.drag * maximumVelocity));
	}
	
	public void moveBackward() {
		body.AddRelativeForce(new Vector2(0, -body.mass * body.drag * maximumVelocity));
	}
	
	public abstract void fire();
}
