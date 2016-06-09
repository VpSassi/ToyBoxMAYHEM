using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy1;
	public GameObject enemy2;
	public Transform areaUpperLeft;
	public Transform areaLowerRight;
	public float enemy1SpawnInterval;
	public float enemy2SpawnInterval;


	public LayerMask spawnMask;

	float enemy1SpawnTimer;
	float enemy2SpawnTimer;

	void Start () {
		enemy1SpawnTimer = enemy1SpawnInterval;
		enemy2SpawnTimer= enemy2SpawnInterval;
	}

	void Update () {

		enemy1SpawnTimer -= Time.deltaTime;
		if (enemy1SpawnTimer <= 0) {
			enemy1SpawnTimer = enemy1SpawnInterval;
			SpawnEnemy1();
		}

		enemy2SpawnTimer -= Time.deltaTime;
		if (enemy2SpawnTimer <= 0) {
			enemy2SpawnTimer = enemy2SpawnInterval;
			SpawnEnemy2();
		}
	}

	void SpawnEnemy1 () {
		float spawnX = Random.Range(areaUpperLeft.position.x, areaLowerRight.position.x);
		float spawnY = Random.Range(areaLowerRight.position.y, areaUpperLeft.position.y);

		Vector2 rayStart = new Vector2(spawnX, spawnY);

		RaycastHit2D spawnRay = Physics2D.Raycast(rayStart, Vector2.down, Mathf.Infinity, spawnMask);

		Vector2 spawnLocation = spawnRay.point + Vector2.up;
		GameObject enemIns = (GameObject)Instantiate(enemy1, spawnLocation, Quaternion.identity);
	}

	void SpawnEnemy2 () {
		int random = (Random.Range(0, 1));

		Vector2 spawnLocation = new Vector2();

		if (random > 0) {
			spawnLocation = areaUpperLeft.position;
		} else {
			spawnLocation = areaLowerRight.position;
		}

		GameObject enemyIns = (GameObject)Instantiate(enemy2, spawnLocation, Quaternion.identity);
	}
}
