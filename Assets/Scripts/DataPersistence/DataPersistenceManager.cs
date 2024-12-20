using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DataPersistenceManager : MonoBehaviour
{
    [Header("File storage config")]
    [SerializeField] private string fileName;

    public static DataPersistenceManager Instance { get; private set; }
    public GameData GameData { get; private set; }
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static event Action OnContinueButtonPressed;
    public bool isEventInvoked;



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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }


    public void ContinueButton()
    {
        if (dataHandler.SaveFileExists())
        {
            SceneManager.LoadScene(2);
            OnContinueButtonPressed?.Invoke();
            isEventInvoked = true;
            LoadGame();
        }
        else
        {
            Debug.Log("No save file exists.");
        }
    }


    public void NewGameButton()
    {
        if (dataHandler.SaveFileExists())
        {
            DeleteSaveFile();
        }
        NewGame();
        SaveGame();
        SceneManager.LoadScene(1);
    }


    private void DeleteSaveFile()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
                Debug.Log("Old save file deleted successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to delete save file: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("No save file found to delete.");
        }
    }


    public void NewGame()
    {
        this.GameData = new GameData();

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
    }


    public void LoadGame()
    {
        this.GameData = dataHandler.Load();

        if (this.GameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
    }


    public void SaveGame()
    {
        // Create a local copy of gameData
        GameData localGameData = this.GameData;

        // Pass the local variable by reference
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref localGameData);
        }

        // Assign the modified local variable back to the property
        this.GameData = localGameData;

        // Save the data
        dataHandler.Save(this.GameData);
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
        Debug.Log("Data persistence objects found: " + dataPersistenceObjects.Count());
        return new List<IDataPersistence>(dataPersistenceObjects);
    }


    public void RegisterDataPersistenceObject(IDataPersistence dataPersistenceObject)
    {
        if (!dataPersistenceObjects.Contains(dataPersistenceObject))
        {
            dataPersistenceObjects.Add(dataPersistenceObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
