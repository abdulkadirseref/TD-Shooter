using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class WaveManager : MonoBehaviour, IDataPersistence
{
    public static WaveManager Instance { get; private set; }
    public int waveIndex;
    public float waveTimer;
    public bool isTimeOver = false;
    public bool canSpawnEnemy;
    public static event Action OnTimerReachesZero;

    [SerializeField] private WaveData[] waveData;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        canSpawnEnemy = true;
    }


    private void OnEnable()
    {
        OnTimerReachesZero += EndWave;
    }


    private void OnDisable()
    {
        OnTimerReachesZero -= EndWave;
    }


    private void FixedUpdate()
    {
        if (waveTimer < 0)
        {
            waveTimer = 0;
            OnTimerReachesZero?.Invoke();
            isTimeOver = true;
            canSpawnEnemy = false;
        }
        else if (waveTimer > 0)
        {
            waveTimer -= Time.fixedDeltaTime;
        }
    }


    public void StartWave()
    {
        if (waveIndex >= 0 && waveIndex <= waveData.Length)
        {
            Debug.Log("Wave " + waveIndex + " Started");
            isTimeOver = false;
            waveTimer = waveData[waveIndex - 1].duration;
            canSpawnEnemy = true;
        }
        else
        {
            Debug.LogError("Invalid waveIndex. Make sure it's within the bounds of wavesData array.");
        }
    }


    public void EndWave()
    {
        Debug.Log("Wave " + waveIndex + " Ended");
        waveIndex++;
        SceneManager.LoadScene(2);
    }


    public void LoadData(GameData data)
    {
        this.waveIndex = data.waveIndex;
    }


    public void SaveData(ref GameData data)
    {
        data.waveIndex = this.waveIndex;
    }


    [System.Serializable]
    public class WaveData
    {
        public float duration;
    }
}
