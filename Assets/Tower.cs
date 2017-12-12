using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	[Header("Missle tower stats")]
	public float range = 15f;

	public float rotationSpeed = 3f;
	public float fireRate = 3f;
	private float fireTimer = 0f;
	private Renderer rend;
	[Header("Unity files")]
	public string enemyTag = "Enemies";

	private Transform target;
	public GameObject rotationPart;
	public GameObject projectile;
	public Transform firePoint;

	public Vector3 onhover;
	private Vector3 normal;

	bool aimType = false;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("ClosestTarget", 0f, 0.333f);
		rend = GetComponent<Renderer>();
		normal = transform.localScale;
	}
	private void OnMouseEnter()
	{
		transform.localScale += onhover;
	}
	private void OnMouseExit()
	{
		transform.localScale = normal;
	}
	private void OnMouseDown(){
	
		if (aimType) {
			CancelInvoke ();
			Debug.Log ("mirando no mais perto");
			InvokeRepeating ("ClosestTarget", 0f, 0.333f);

		} else {
			CancelInvoke ();
			Debug.Log ("mirando no primeiro");
			InvokeRepeating ("FirstTarget", 0f, 0.333f);
		}
		aimType = !aimType;
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
// Waypoints.points[GetComponent<EnemyMoviment>().index].transform.position
	void FirstTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		int enemyIndex = 0;
		int aux = 0;
		float firstEnemyDistance = Mathf.Infinity;
		float distanceToWaypoint, distanceToTower;
		GameObject firstEnemy = null;
		foreach (GameObject enemy in enemies) {
			aux = enemy.GetComponent<EnemyMoviment>().index;
			if(aux >= enemyIndex){
				distanceToWaypoint = Vector3.Distance (enemy.GetComponent<EnemyMoviment>().corner.position, Waypoints.points[aux].position);
				distanceToTower = Vector3.Distance (transform.position, enemy.transform.position);
				if (firstEnemyDistance > distanceToWaypoint && distanceToTower <= range) {
					enemyIndex = aux;
					firstEnemy = enemy;
					firstEnemyDistance = distanceToWaypoint;
				}
			}
		}
		if (firstEnemy != null && firstEnemyDistance <= range) {
			target = firstEnemy.transform;
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
