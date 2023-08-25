using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunSelectMenu : MonoBehaviour
{
    public GameObject gunCanvas; // Reference to the UI canvas that will hold the guns
    public List<GameObject> gunPrefabs;
    public Transform[] spawnPoints;
    public int maxGunPrefabs = 3;

    private List<GameObject> spawnedGuns = new List<GameObject>();



    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }


    public void OnEnable()
    {
        SpawnGunUI();
    }


    void SpawnGunUI()
    {
        // Destroy previously spawned guns
        foreach (GameObject spawnedGun in spawnedGuns)
        {
            Destroy(spawnedGun);
        }
        spawnedGuns.Clear();

        // Randomly spawn gun prefabs
        int numGunsToSpawn = Mathf.Min(maxGunPrefabs, gunPrefabs.Count);
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < numGunsToSpawn; i++)
        {
            if (availableSpawnPoints.Count == 0)
                break;

            int randomGunIndex = Random.Range(0, gunPrefabs.Count);
            GameObject selectedGunPrefab = gunPrefabs[randomGunIndex];

            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];

            GameObject spawnedGun = Instantiate(selectedGunPrefab, spawnPoint.position, Quaternion.identity);
            spawnedGun.transform.SetParent(gunCanvas.transform, false); // Set the canvas as parent

            spawnedGuns.Add(spawnedGun);

            availableSpawnPoints.RemoveAt(randomSpawnIndex);
            gunPrefabs.RemoveAt(randomGunIndex);
        }
    }




}
