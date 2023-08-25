using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectButton : MonoBehaviour
{
    public BaseGunData gunData;

    public GunIconPanel gunIconPanel;

    public void OnSelectButtonClicked()
    {
        if (InventoryManager.Instance != null && gunData != null && InventoryManager.Instance.canEquipGun == true)
        {
            InventoryManager.Instance.AddGunToInventory(gunData);
            InventoryManager.Instance.UpdateEquipGun();
            gameObject.SetActive(false);
            // GunSelectionEvent.InvokeGunSelection(gunData); // If needed
        }
        else if (InventoryManager.Instance != null && gunData != null && InventoryManager.Instance.canEquipGun != true)
        {
            Debug.Log("Inventory is full");
        }
        else
        {
            Debug.LogWarning("InventoryManager or GunData is missing!");
        }
    }
}
