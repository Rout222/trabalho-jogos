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
		GameObject[] mapList = { node, startingPoint, path, endingPoint };
		Transform[] parentList = { nodes.transform, transform.parent, paths.transform, transform.parent};
		for(int i = 0; i < Mathf.Sqrt(map.Length); i++){
			for(int j = 0; j < Mathf.Sqrt(map.Length); j++){
				createGO (i, j, mapList[map[i,j]-1], parentList[map[i,j]-1]	);
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

	public void updateNo(int i, int j){
		if (PlayerPrefs.GetInt ("edit") == 1) {
			GameObject[] mapList = { node, startingPoint, path, endingPoint };
			Transform[] parentList = { nodes.transform, transform.parent, paths.transform, transform.parent };
			map [i, j] = ((map [i, j]) % 4) + 1;
			createGO (i, j, mapList [map [i, j] - 1], parentList [map [i, j] - 1]);
		}
	}
}