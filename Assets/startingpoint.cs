using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingpoint : MonoBehaviour {
	Renderer rend;
	Component script;
	Vector3 pos;
	MapGeneration map;
	// Use this for initialization
	void Start()
	{
		rend = GetComponent<Renderer>();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<MapGeneration> ();
	}
	private void OnMouseDown()
	{
		if (PlayerPrefs.GetInt ("edit") == 1) {
			Destroy (gameObject);
			pos = transform.position;
			map.updateNo ((int)pos.x/5, (int)pos.z/5);

		}
	}
}
