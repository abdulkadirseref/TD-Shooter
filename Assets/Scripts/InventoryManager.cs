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
