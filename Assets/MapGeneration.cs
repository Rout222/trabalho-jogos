using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

	public GameObject node;
	public GameObject path;

	public GameObject startingPoint;
	public GameObject endingPoint;

	public GameObject nodes;
	public GameObject paths;

	public GameObject way;
	public GameObject waypointprefab;

	public int padding;

	int aresta;
    int origem;
    int destino;
    List<int> l1 = new List<int>();     
    List<int> l2 = new List<int>();     
    List<int[]> agm = new List<int[]>();
	bool inicio = false;
	bool fim = false;


	int[,] MA;
	int originr;
	int originc;
	int destinyr;
	int destinyc;
	List<int> caminho;


	int [,] map = {
		{1,1,1},
		{1,1,1},
		{1,1,1},
	};
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("edit") == 0) {
			loadMap ();

			MatrizAdjacencia ();
			for(int i = 0; i < Mathf.Sqrt(MA.Length); i++){
				for (int j = 0; j < Mathf.Sqrt (MA.Length); j++){
					Debug.Log(i + "," + j + " = " + MA[i,j]);
				}
			}
			origem = (int) (originc * Mathf.Sqrt(map.Length) + originr);
			destino = (int)  (destinyc * Mathf.Sqrt (map.Length) + destinyr);
			caminho = findPath();
			for (int i = 0; i < caminho.Count; i++) {
				Debug.Log(caminho[i] + " = " + (int) ((caminho[i] % 3)));
				GameObject GO = Instantiate (waypointprefab, new Vector3(4*((int) ((caminho[i] - ((caminho[i] % 3) - 1))/3) + padding),4f, 4*(caminho[i] % 3)), Quaternion.identity) as GameObject;
				GO.transform.SetParent (way.transform);
			}
		}
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

	void MatrizAdjacencia(){
		MA = new int[map.Length, map.Length];
		for(int i = 0; i < map.Length; i++){
			for(int j = 0; j < map.Length; j++){
				MA[i,j] = -1;
			}
		}
		originr = -1;
		originc = -1;
		destinyr = -1;
		destinyc = -1;
		int colaux, rowaux;
		int[] values = { -1, 1, 1,1};
		for(int i = 0; i < Mathf.Sqrt(map.Length); i++){
			for(int j = 0; j < Mathf.Sqrt(map.Length); j++){
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
						if(i+rowaux >= 0 && i+rowaux < Mathf.Sqrt(map.Length) && j+colaux >= 0 && j+colaux < Mathf.Sqrt(map.Length)){
							if(k==4){
								MA [(int) (i * Mathf.Sqrt(map.Length) + j), (int) ((i + rowaux) * Mathf.Sqrt(map.Length) + (j + colaux))] = 0;
							} else {
								MA [(int) (i * Mathf.Sqrt(map.Length) + j), (int) ((i + rowaux) * Mathf.Sqrt(map.Length) + (j + colaux))] = values[map[i+rowaux,j+colaux] -1];	
								MA [(int) ((i + rowaux) * Mathf.Sqrt(map.Length) + (j + colaux)), (int) (i * Mathf.Sqrt(map.Length) + j)] = values[map[i+rowaux,j+colaux] -1];
							}

						}
					}
				}
			}
		}
	}

	public List<int> findPath(){
		int auxPai = origem;

		List<int[]> agmFP = executaPrim();
		List<int> FP = new List<int>();
		List<int> Usados = new List<int>();
		List<int> Blacklist = new List<int>();
		bool flag = false;
		while(auxPai != destino){
			flag = false;
			for(int i = 0; i < agmFP.Count; i++){
				int[] aux = agmFP[i];
				if(aux[0] == auxPai && !flag && Usados.IndexOf(aux[1]) == -1 && Blacklist.IndexOf(aux[1]) == -1){
					Usados.Add(auxPai);
					auxPai = aux[1];
					FP.Add(auxPai);
					flag = true;
				}
				if(aux[1] == auxPai && !flag && Usados.IndexOf(aux[0]) == -1 && Blacklist.IndexOf(aux[1]) == -1) {
					Usados.Add(auxPai);
					auxPai = aux[0];
					FP.Add(auxPai);
					flag = true;
				}
			}
			if(!flag && auxPai != destino){
				Blacklist = Usados.GetRange(0, Usados.Count);
				Usados = new List<int>();
				auxPai = origem;
			}
		}
		return Usados;
	}

	public List<int[]> executaPrim(){

		int[] resultado;
		l1.Add(origem);
		for (int i = 0; i < Mathf.Sqrt(MA.Length); i++) 
		{ 
			if(i!= origem){
				l2.Add(i);
			}
		}
		int vert = -1;
		int max = 0;
		List<int> clonarLista2 = l2.GetRange(0, l2.Count);
		for (int i = 0; i < clonarLista2.Count; i++){
			vert = clonarLista2[i];
			max = 0;
			for (int j = 0; j < Mathf.Sqrt(MA.Length); j++){

				if(max < MA[vert,j]){
					max = MA[vert,j];
				}
			}

			if(max <= 0) {
				l2.Remove(vert);
			}
		}
		int aux = 1;
		while(l2.Count != 0){
			resultado = new int[] {1};
			aresta = 99999;
			for (int i = 0; i < l1.Count; i++) 
			{
				for (int j = 0; j < Mathf.Sqrt(MA.Length); j++) 
				{
					if(
						(MA[j,l1.ElementAt(i)] < aresta)
						&& (MA[j,l1.ElementAt(i)] != 0)
						&& (MA[j,l1.ElementAt(i)] != -1) 
						&& (l1.IndexOf(j) == -1)
						){
						aresta = MA[j,l1.ElementAt(i)];
						resultado = new int[] {l1.ElementAt(i),j};
					}
				}
			}

			l1.Add(resultado[1]);
			agm.Add(resultado);
			l2.Remove(resultado[1]); 
		}
		return agm;

	}  
}


