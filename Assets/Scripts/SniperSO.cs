using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sniper", menuName = "Gun/Sniper")]
public class SniperSO : BaseGunData
{
    public override void AddToInventory(InventoryManager inventoryManager)
    {
        if (gunPrefab != null)
        {
            inventoryManager.AddGunToInventory(this);
        }
        else
        {
            Debug.LogWarning("PistolPrefab is missing!");
        }
    }
}
