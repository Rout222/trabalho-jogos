using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

	public GameObject node;
	public GameObject path;

	public GameObject startingPoint;
	public GameObject endingPoint;

	public GameObject nodes;
	public GameObject paths;

	public int padding;

	bool inicio = false;
	bool fim = false;

	int[,] map = {
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},

	};
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("edit") == 0)
			loadMap ();

		GameObject[] mapList = { node, startingPoint, path, endingPoint };
		Transform[] parentList = { nodes.transform, transform.parent, paths.transform, transform.parent};
		for(int i = 0; i < Mathf.Sqrt(map.Length); i++){
			for(int j = 0; j < Mathf.Sqrt(map.Length); j++){
				if (PlayerPrefs.GetInt ("edit") == 1) {
					createGOEditMode (i, j, mapList [map [i, j] - 1], parentList [map [i, j] - 1], map [i, j]);
				} else {
					createGO (i, j, mapList [map [i, j] - 1], parentList [map [i, j] - 1]);
				}
			}	
		}
	}

	void createGO(int x, int z, GameObject aux, Transform parent){

	
		float height;
		height = 0f;
		if (map [x, z] == 2 || map [x, z] == 4) {
			height = 2.5f;
		}
		Vector3 GOScale = aux.transform.localScale;
		GameObject GO = Instantiate (aux, new Vector3(x*(GOScale.x + padding), height,z*(GOScale.z + padding)), Quaternion.identity) as GameObject;
		GO.transform.SetParent (parent);
	}

	void createGOEditMode(int x, int z, GameObject aux, Transform parent, int num){

		string stringauxiliar;
		float height;
		height = 0f;
		if (map [x, z] == 2 || map [x, z] == 4) {
			height = 2.5f;
		}
		stringauxiliar = "map" + x + "," + z;
		PlayerPrefs.SetInt (stringauxiliar, num);

		Vector3 GOScale = aux.transform.localScale;
		GameObject GO = Instantiate (aux, new Vector3(x*(GOScale.x + padding), height,z*(GOScale.z + padding)), Quaternion.identity) as GameObject;
		GO.transform.SetParent (parent);
	}

	void loadMap(){
		for (int i = 0; i < Mathf.Sqrt (map.Length); i++) {
			for (int j = 0; j < Mathf.Sqrt (map.Length); j++) {
				map [i, j] = PlayerPrefs.GetInt ("map" + i + "," + j);
			}
		}
	}

	public void updateNo(int i, int j){
		if (PlayerPrefs.GetInt ("edit") == 1) {
			GameObject[] mapList = { node, startingPoint, path, endingPoint };
			Transform[] parentList = { nodes.transform, transform.parent, paths.transform, transform.parent };
			if (map [i, j] == 2) {
				inicio = false;
			}
			if (map [i, j] == 4) {
				fim = false;
			}
			if ((((map [i, j]) % 4) + 1) == 2 ) {
				if (inicio) {
					map [i, j] = ((map [i, j]) % 4) + 1;
				} else {
					inicio = true;
				}

			}
			if ((((map [i, j]) % 4) + 1) == 4 ) {
				if (fim) {
					map [i, j] = ((map [i, j]) % 4) + 1;
				} else {
					fim = true;
				}

			}
			map [i, j] = ((map [i, j]) % 4) + 1;

			createGOEditMode (i, j, mapList [map [i, j] - 1], parentList [map [i, j] - 1], map [i, j]);
		}
	}
}