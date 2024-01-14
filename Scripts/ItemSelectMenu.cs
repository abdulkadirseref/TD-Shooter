using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSelectMenu : MonoBehaviour
{
    public GameObject gunCanvas; // Reference to the UI canvas that will hold the guns
    public List<GameObject> cardPrefabs;
    public Transform[] spawnPoints;
    public int maxCardPrefabs = 5;

    float spawnSameGunProbability = 0.1f;

    private List<GameObject> spawnedCards = new List<GameObject>();



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
        foreach (GameObject spawnedCard in spawnedCards)
        {
            Destroy(spawnedCard);
        }
        spawnedCards.Clear();

        // Randomly spawn gun prefabs
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < maxCardPrefabs; i++)
        {
            if (availableSpawnPoints.Count == 0)
                break;

            GameObject selectedCardPrefab;

            // Determine whether to spawn a different gun or the same one

            if (i > 0 && Random.value < spawnSameGunProbability)
            {
                // Spawn the same gun as the previous one
                selectedCardPrefab = spawnedCards[i - 1];
            }
            else
            {
                int randomGunIndex = Random.Range(0, cardPrefabs.Count);
                selectedCardPrefab = cardPrefabs[randomGunIndex];
                cardPrefabs.RemoveAt(randomGunIndex);
            }

            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];

            GameObject spawnedGun = Instantiate(selectedCardPrefab, spawnPoint.position, Quaternion.identity);
            spawnedGun.transform.SetParent(gunCanvas.transform, false); // Set the canvas as parent

            spawnedCards.Add(spawnedGun);

            availableSpawnPoints.RemoveAt(randomSpawnIndex);
        }
    }




}
