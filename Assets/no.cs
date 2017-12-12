using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no : MonoBehaviour {
	Renderer rend;
	Component script;
	Vector3 pos;
	MapGeneration map;
	public GameObject tower;
	bool used;

	public Color onhover;
	private Color normal;

	// Use this for initialization
	void Start()
	{

		used = false;
		rend = GetComponent<Renderer>();
		normal = rend.material.color;
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<MapGeneration> ();
	}

	private void OnMouseEnter()
	{
		if(!used)
			rend.material.color = onhover;
	}
	private void OnMouseExit()
	{
			rend.material.color = normal;
	}
	private void OnMouseDown()
	{
		if (PlayerPrefs.GetInt ("edit") == 1) {
			Destroy (gameObject);
			pos = transform.position;
			map.updateNo ((int)pos.x / 5, (int)pos.z / 5);

		} else {
			pos = transform.position;
			if (!used) {
				Vector3 GOScale = transform.position;
				GameObject GO = Instantiate (tower, new Vector3(GOScale.x, 0.5f,GOScale.z), Quaternion.identity) as GameObject;
				used = true;

			}

		}
	}
}
