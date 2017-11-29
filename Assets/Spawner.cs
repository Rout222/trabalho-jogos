using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
	public Text timerText;
	public Text waveText;
	public float timer;
	public float timerBetweenWaves;
	private int waveNumber = 0;
	public GameObject enemy;

	bool spawning = false;

	void Update(){
		if (!spawning) {
			timerText.text = Mathf.Round (timer).ToString ();
			timer -= Time.deltaTime;
		}
		if (timer < 0f && !spawning) {
			StartCoroutine (spawnEnemies ());
			timer = timerBetweenWaves;
		}

	}

	IEnumerator spawnEnemies(){
		waveNumber++;
		waveText.text = string.Concat("Wave number: " , waveNumber.ToString ());
		GameObject spawnPoint = GameObject.Find ("StartingPoint(Clone)");
		spawning = true;
		for (int i = 0; i < waveNumber; i++) {
			GameObject GO = Instantiate (enemy, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
			GO.transform.SetParent (GameObject.Find("Enemies").transform);
			yield return new WaitForSeconds (0.3f);
		}
		spawning = false;
	}

}