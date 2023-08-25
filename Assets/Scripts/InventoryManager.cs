using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance { get; private set; }
    public List<BaseGunData> inventory = new List<BaseGunData>();
    public int maxItemCount = 6;
    public bool canEquipGun;




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
        Debug.Log("InventoryManager Start method called. Loading inventory...");
        LoadInventory();
    }


    private void OnApplicationQuit()
    {
        Debug.Log("Application quitting. Saving inventory...");
        SaveInventory();
    }


    private void SaveInventory()
    {
        // Convert the list of inventory items to a string
        string serializedInventory = string.Join(",", inventory.ConvertAll(item => item.name).ToArray());

        // Save the inventory data to PlayerPrefs
        PlayerPrefs.SetString("Inventory", serializedInventory);
        PlayerPrefs.Save();
    }


    private void LoadInventory()
    {
         if (PlayerPrefs.HasKey("Inventory"))
    {
        string serializedInventory = PlayerPrefs.GetString("Inventory");
        string[] itemNames = serializedInventory.Split(',');

        inventory.Clear();

        foreach (string itemName in itemNames)
        {
            BaseGunData gunData = Resources.Load<BaseGunData>("YourScriptableObjectFolderPath/" + itemName);
            if (gunData != null)
            {
                inventory.Add(gunData);
            }
        }
    }
    }


    public void AddGunToInventory(BaseGunData gunData)
    {
        if (CanEquipGun())
        {
            if (gunData != null)
            {
                inventory.Add(gunData);
                Debug.Log(gunData.gunName + " added to inventory!");
            }
            else
            {
                Debug.LogWarning("GunData is missing!");
            }
        }
        else
        {
            Debug.Log("Cannot add more guns. Inventory is full or canEquipGun is false.");
        }
    }


    public bool CanEquipGun()
    {
        return (inventory.Count < maxItemCount) && canEquipGun;
    }


    public void UpdateEquipGun()
    {
        canEquipGun = inventory.Count < maxItemCount;
    }

}
