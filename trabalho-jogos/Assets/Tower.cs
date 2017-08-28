using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[Header("Missle tower stats")]
	public float range = 15f;

	public float rotationSpeed = 3f;
	public float fireRate = 3f;
	private float fireTimer = 0f;
	[Header("Unity files")]
	public string enemyTag = "Enemies";

	private Transform target;
	public GameObject rotationPart;
	public GameObject projectile;
	public Transform firePoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("ClosestTarget", 0f, 0.333f);	
	}

	void ClosestTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float closestEnemyDistance = Mathf.Infinity;
		GameObject closestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distance = Vector3.Distance (transform.position, enemy.transform.position);
			if (closestEnemyDistance > distance) {
				closestEnemy = enemy;
				closestEnemyDistance = distance;
			}
		}

		if (closestEnemy != null && closestEnemyDistance <= range) {
			target = closestEnemy.transform;
		} else {
			target = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		Vector3 lookTarget = Quaternion.Lerp(rotationPart.transform.rotation,Quaternion.LookRotation (target.position - transform.position), Time.deltaTime * rotationSpeed).eulerAngles;
		rotationPart.transform.rotation = Quaternion.Euler (0f, lookTarget.y, 0f);
		if (fireTimer <= 0f) {
			Shoot ();
			fireTimer = 1f / fireRate;
		}
		fireTimer -= Time.deltaTime;
	}

	void Shoot(){
		GameObject projectileGO = Instantiate (projectile, firePoint.position, firePoint.rotation) as GameObject;
		Bullet bullet = projectileGO.GetComponent<Bullet> ();
		if (bullet != null)
			bullet.aimTarget (target);
	}

	void OnDrawGizmos () {
		Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
