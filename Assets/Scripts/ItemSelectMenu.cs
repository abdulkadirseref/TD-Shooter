using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ItemSelectMenu : MonoBehaviour, IDataPersistence
{
    public GameObject gunCanvas;
    public Transform[] spawnPoints;
    [SerializeField] private List<GameObject> spawnedCards = new List<GameObject>();
    [SerializeField] private List<BaseGunCardData> lastGunCardList;
    [SerializeField] private List<BaseItemCardData> lastItemCardList;



    private void Start()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
        this.lastGunCardList = DataPersistenceManager.Instance.GameData.lastGunCards;
        this.lastItemCardList = DataPersistenceManager.Instance.GameData.lastItemCards;
        SpawnLastCards();
    }

    public void OnEnable()
    {
        SpawnGunUI();
        InventoryManager.OnInventoryChanged += StartUpdateLastCardList;
        BackPackManager.OnBackpackChanged += StartUpdateLastCardList;
    }


    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= StartUpdateLastCardList;
        BackPackManager.OnBackpackChanged -= StartUpdateLastCardList;
    }


    public void PlayButton()
    {
        SceneManager.LoadScene(3);
        WaveManager.Instance.StartWave();
    }


    public void ReRollButton()
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

    public void StartUpdateLastCardList()
    {
        StartCoroutine(UpdateLastCardList());
    }


    private IEnumerator UpdateLastCardList()
    {
        yield return new WaitForSeconds(0.01f);

        GameObject[] gunCardList = GameObject.FindGameObjectsWithTag("GunCard");
        GameObject[] itemCardList = GameObject.FindGameObjectsWithTag("ItemCard");

        lastGunCardList.Clear();
        lastItemCardList.Clear();

        foreach (GameObject card in gunCardList)
        {
            BaseGunCard baseGunCard = card.GetComponent<BaseGunCard>();

            if (baseGunCard != null)
            {
                lastGunCardList.Add(baseGunCard.baseGunCardData);
            }
        }

        foreach (GameObject itemCard in itemCardList)
        {
            BaseItemCard baseItemCard = itemCard.GetComponent<BaseItemCard>();

            if (baseItemCard != null)
            {
                lastItemCardList.Add(baseItemCard.baseItemCardData);
            }
        }
        InventoryManager.Instance.isEventInvoked = false;
        BackPackManager.Instance.isEventInvoked = false;
    }

    private void SpawnLastCards()
    {
        if (DataPersistenceManager.Instance.isEventInvoked)
        {
            // Destroy any previously spawned cards to avoid duplication
            foreach (GameObject spawnedCard in spawnedCards)
            {
                Destroy(spawnedCard);
            }
            spawnedCards.Clear();

            List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

            // Spawn gun cards
            foreach (BaseGunCardData gunCardData in lastGunCardList)
            {
                if (availableSpawnPoints.Count == 0)
                    break;

                GameObject gunPrefab = gunCardData.cardPrefab;

                if (gunPrefab != null)
                {
                    int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
                    Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];

                    GameObject spawnedGun = Instantiate(gunPrefab, spawnPoint.position, Quaternion.identity);
                    spawnedGun.transform.SetParent(gunCanvas.transform, false); // Set the canvas as parent

                    spawnedCards.Add(spawnedGun);
                    availableSpawnPoints.RemoveAt(randomSpawnIndex);
                }
            }

            // Spawn item cards
            foreach (BaseItemCardData itemCardData in lastItemCardList)
            {
                if (availableSpawnPoints.Count == 0)
                    break;

                GameObject itemPrefab = itemCardData.cardPrefab;

                if (itemPrefab != null)
                {
                    int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
                    Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];

                    GameObject spawnedItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
                    spawnedItem.transform.SetParent(gunCanvas.transform, false); // Set the canvas as parent

                    spawnedCards.Add(spawnedItem);
                    availableSpawnPoints.RemoveAt(randomSpawnIndex);
                }
            }
        }
        DataPersistenceManager.Instance.isEventInvoked = false;
    }

    public void SaveData(ref GameData data)
    {
        data.lastGunCards = this.lastGunCardList;
        data.lastItemCards = this.lastItemCardList;
    }

    public void LoadData(GameData data)
    {

    }
}
