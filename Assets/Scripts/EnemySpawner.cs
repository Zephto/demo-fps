using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[Header("General")]
	[SerializeField] private GameObject[] zombiePrefabs;
	[SerializeField] private Transform[] spawnPoints;

	[Header("Rounds")]
	[SerializeField] private float timeBetweenWaves = 80f;
	[SerializeField] private int initialZombiesPerWave = 5;
	[SerializeField] private float difficultyMultiplier = 1.2f;

	[Header("Spawning")]
	[SerializeField] private float spawnInterval = 1f;
	[SerializeField] private float spawnRadius = 2f;

	private int currentWave;

	#region Public Methods
	public void StartSpawn()
	{	
		StartCoroutine(SpawnLoop());
	}
	#endregion

	private IEnumerator SpawnLoop()
	{
		while (true)
		{
			currentWave++;

			int zombiesThisWave = Mathf.RoundToInt(initialZombiesPerWave * Mathf.Pow(difficultyMultiplier, currentWave - 1));
			timeBetweenWaves += 5;

			Debug.Log("Ronda " + currentWave + " -- Total zombies: " + zombiesThisWave);

			//spawn zombies
			for(int i=0; i<zombiesThisWave; i++)
			{
				SpawnZombie();
				yield return new WaitForSeconds(spawnInterval);
			}

			Debug.Log("Ronda finalizada");
			yield return new WaitForSeconds(timeBetweenWaves);
		}
	}

	private void SpawnZombie()
	{
		if (zombiePrefabs.Length == 0 || spawnPoints.Length == 0)
		{
			return;
		}

		//Select a random prefab and spawn
		GameObject selection = zombiePrefabs[UnityEngine.Random.Range(0, zombiePrefabs.Length)];
		Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

		//Offset from spawnpoint
		Vector3 offset = UnityEngine.Random.insideUnitCircle * spawnRadius;
		Vector3 spawnPos = spawnPoint.position + new Vector3(offset.x, offset.y);

		GameObject newZombie = Instantiate(selection, spawnPos, spawnPoint.rotation);

		//Aqui se spawnean los valores de los zombies, si hay mas veloces o lentos
	}
}
