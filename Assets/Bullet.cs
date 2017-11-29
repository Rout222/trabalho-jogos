using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	public float bulletSpeed = 30f;
	public GameObject impactEffect;
	public void aimTarget(Transform t){
		target = t;
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			Destroy (gameObject);
			return;
		}
		Vector3 dir = target.position - transform.position;
		float distanceTravelled = bulletSpeed * Time.deltaTime;

		if (dir.magnitude <= distanceTravelled) {
			hitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceTravelled, Space.World);
		
	}

	void hitTarget(){
		Destroy(Instantiate (impactEffect, transform.position, transform.rotation) as GameObject, 2f);
		Destroy (gameObject);
	}
}
