using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : Controller {

	public GameObject projectile;
	public GameObject missile;
	public Transform spawnLocation;
	
	public float missileProbability;
	public float coolDown;

	private float lastFire;

	private List<Transform> targetCandidates;

	void Start() {
		targetCandidates = new List<Transform>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(!targetCandidates.Contains(collider.transform))
			targetCandidates.Add(collider.transform);
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		targetCandidates.Remove(collider.transform);
	}	

	void UpdatePrey() {
		targetCandidates.RemoveAll(t => t == null);
		target = targetCandidates.Count == 0 ? null : targetCandidates[0];
	}

	void FixedUpdate() {
		UpdatePrey();

	}

	public override void fire() {
		if(Time.time - lastFire < coolDown)
			return;
		
		Instantiate(Random.value < missileProbability ? missile : projectile, spawnLocation.position, spawnLocation.rotation);
		lastFire = Time.time;
	}
}
