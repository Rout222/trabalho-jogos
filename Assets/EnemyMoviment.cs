using UnityEngine;

public class EnemyMoviment : MonoBehaviour {

	public float speed = 5f;

	public Transform corner;
	public int index = 0;

	void Start(){
		corner = Waypoints.points[0];
	}

	void FixedUpdate(){
		Vector3 dir = corner.position - transform.position;
		transform.Translate (dir.normalized * speed);

		if (Vector3.Distance (corner.position, transform.position) <= 0.4f) {
			getNextCorner ();
		}
	}

	void getNextCorner(){
		if (index == Waypoints.points.Length - 1) {
			Destroy (gameObject);
			return;
		}
		corner = Waypoints.points [++index];
	}

}
