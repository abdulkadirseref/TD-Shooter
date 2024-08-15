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
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;



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
            LoadGame();
            SceneManager.LoadScene(2);
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
        this.gameData = new GameData();

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }


    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }


    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
