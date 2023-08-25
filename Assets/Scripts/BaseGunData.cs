using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGunData : ScriptableObject
{
    public string gunName;
    public Sprite gunIcon;
    public GameObject gunPrefab;

    public abstract void AddToInventory(InventoryManager inventoryManager);
    
}
