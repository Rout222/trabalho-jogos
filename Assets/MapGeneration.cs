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
		{1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 2, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3, 1, 3, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3, 1, 3, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1},
		{1, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},

	};
	// Use this for initialization
	void Start () {
		GameObject[] mapList = { node, startingPoint, path, endingPoint };
		Transform[] parentList = { nodes.transform, transform.parent, paths.transform, transform.parent};
		float height;
		for(int i = 0; i < Mathf.Sqrt(map.Length); i++){
			for(int j = 0; j < Mathf.Sqrt(map.Length); j++){
				height = 0f;
				if (map [i, j] == 2 || map [i, j] == 4) {
					height = 2.5f;
				}
				createGO (i, j, height, mapList[map[i,j]-1], parentList[map[i,j]-1]	);
			}	
		}
	}

	void createGO(int x, int z, float y, GameObject aux, Transform parent){
		Vector3 GOScale = aux.transform.localScale;
		GameObject GO = Instantiate (aux, new Vector3(x*(GOScale.x + padding),y,z*(GOScale.z + padding)), Quaternion.identity) as GameObject;
		GO.transform.SetParent (parent);
	}

}