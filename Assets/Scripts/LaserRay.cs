using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour {

	public GameObject impact;
	public SpriteMask beamMask;
	public float maxBeamRange;
	
	void FixedUpdate() {
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxBeamRange);
		bool hit = hitInfo.collider != null;
		
		impact.SetActive(hit);
		if(hit) {
			Vector2 impactLocalPos = impact.transform.localPosition;
			impactLocalPos.y = hitInfo.distance / transform.localScale.y;
			impact.transform.localPosition = impactLocalPos;
		}
		
		beamMask.alphaCutoff = hit ? 1 - (hitInfo.distance / maxBeamRange) : 0;
	}
}
