using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ItemSelectMenu : MonoBehaviour
{
    public GameObject gunCanvas;
    public Transform[] spawnPoints;
    private List<GameObject> spawnedCards = new List<GameObject>();



    public void OnEnable()
    {
        SpawnGunUI();
    }


    public void PlayButton()
    {
        SceneManager.LoadScene(3);
        WaveManager.Instance.StartWave();
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



        foreach (var slot in spawnPoints)
        {
            // Try to spawn a gun until success
            while (true)
            {
                if (GameManager.Instance.concatenatedList.Count == 0 || availableSpawnPoints.Count == 0)
                    break;

                int randomGunIndex = Random.Range(0, GameManager.Instance.concatenatedList.Count);
                ScriptableObject selectedCardPrefab = GameManager.Instance.concatenatedList[randomGunIndex];

                GameObject gunPrefab = null;
                float spawnChance = 0f;

                if (selectedCardPrefab is BaseGunCardData gunCardData)
                {
                    gunPrefab = gunCardData.cardPrefab;
                    spawnChance = gunCardData.spawnProbability;
                }
                else if (selectedCardPrefab is BaseItemCardData itemCardData)
                {
                    gunPrefab = itemCardData.cardPrefab;
                    spawnChance = itemCardData.spawnProbability;
                }

                if (gunPrefab != null && Random.value < spawnChance)
                {
                    int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
                    Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];

                    GameObject spawnedGun = Instantiate(gunPrefab, spawnPoint.position, Quaternion.identity);
                    spawnedGun.transform.SetParent(gunCanvas.transform, false); // Set the canvas as parent

                    spawnedCards.Add(spawnedGun);
                    availableSpawnPoints.RemoveAt(randomSpawnIndex);
                    break; // Exit the while loop and move to the next slot
                }
            }
        }
    }
}
