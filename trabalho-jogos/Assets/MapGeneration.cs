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

	int[,] MA;
	int originr;
	int originc;
	int destinyr;
	int destinyc;
	int size;
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
		size = 15;
		MatrizAdjacencia();
		Debug.Log(originc);
		Debug.Log(originr);
		Debug.Log(destinyc);
		Debug.Log(destinyr);
	}

	void createGO(int x, int z, float y, GameObject aux, Transform parent){
		Vector3 GOScale = aux.transform.localScale;
		GameObject GO = Instantiate (aux, new Vector3(x*(GOScale.x + padding),y,z*(GOScale.z + padding)), Quaternion.identity) as GameObject;
		GO.transform.SetParent (parent);
	}

	void MatrizAdjacencia(){
		MA = new int[size*size, size*size];
		for(int i = 0; i < size*size; i++){
			for(int j = 0; j < size*size; j++){
				MA[i,j] = int.MaxValue;
			}
		}
		originr = -1;
		originc = -1;
		destinyr = -1;
		destinyc = -1;
		int colaux, rowaux;
		int[] values = { int.MaxValue, 1, 1,1};
		for(int i = 0; i < size; i++){
			for(int j = 0; j < size; j++){
				if(map[i,j] != 1){
					if(map[i,j] == 2){
						originr = i;
						originc =  j;
					}
					if(map[i,j] == 4){
						destinyr = i;
						destinyc = j;
					}
					for(int k = 0; k < 9; k++){
						rowaux = (k/3) -1;
						colaux = k - ((rowaux+1) * 3) - 1;
						if(i+rowaux >= 0 && i+rowaux < size && j+colaux >= 0 && j+colaux < size){
							if(k==4){
								MA [(i * size + j), ((i + rowaux) * size + (j + colaux))] = 0;
							} else {
								MA [(i * size + j), ((i + rowaux) * size + (j + colaux))] = values[map[i+rowaux,j+colaux] -1];							
							}

						}
					}
				}
			}
		}
	}

	void Prim(){
		bool offset = false;
		int aux = originr*size+originc;
		int[] l1 = {aux};
		quantVertices = size*size;
		int[] l2 = new int[size*size - 1];
		for (int i = 0; i < l2.Length; i++){
			if (i  == aux){
				offset = true;
			} else {
				if (offset){
					l2[i] = i + 1;
				} else {
					l2[i] = i;
				}
			}
		}
	}

	void Relax(){

	}

}