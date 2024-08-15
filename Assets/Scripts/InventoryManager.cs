using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{

    public static InventoryManager Instance { get; private set; }
    public List<BaseGunData> inventory = new List<BaseGunData>();
    public int maxItemCount = 6;
    public bool canEquipGun;
    public static event Action OnInventoryChanged;



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


    public void AddGunToInventory(BaseGunData gunData)
    {
        if (CanEquipGun())
        {
            if (gunData != null)
            {
                inventory.Add(gunData);
                Debug.Log(gunData.gunName + " added to inventory!");
                OnInventoryChanged?.Invoke();
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
        return (inventory.Count < maxItemCount);
    }


    public void UpdateEquipGun()
    {
        canEquipGun = inventory.Count < maxItemCount;
    }


    public void LoadData(GameData data)
    {
        this.inventory = data.gunData;
    }


    public void SaveData(ref GameData data)
    {
        data.gunData = this.inventory;
    }
}
