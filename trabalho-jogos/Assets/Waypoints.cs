using UnityEngine;

public class Waypoints : MonoBehaviour {

	public static Transform[] points;

	void Awake(){
		GameObject MapG = GetComponent<MapGeneration>();
		points = new Transform[MapG.waypoints];
		for (int i = 0; i < points.Length; i++) {
			points [i] = transform.GetChild(i);
		}
	}
}
