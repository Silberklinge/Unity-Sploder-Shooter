using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {
	
	public GameObject projectile;
	public float velocityModifier;
	public float coolDown;
	
	private float timeOfLastProjectile;
	
	void Update() {
		if(Time.time - timeOfLastProjectile >= coolDown) {
			GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
			Projectile projScript = bullet.GetComponent<Projectile>();
			bullet.GetComponent<Rigidbody2D>().velocity = transform.right * projScript.baseVelocity * velocityModifier;
			projScript.owner = transform.root.gameObject;
			
			timeOfLastProjectile = Time.time;
		}
	}
}
