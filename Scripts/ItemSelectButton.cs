using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemSelectButton : MonoBehaviour
{
    public BaseGunData gunData;
    public BaseItemData itemData;

    public void OnSelectButtonClicked()
    {
        if (InventoryManager.Instance != null && gunData != null && InventoryManager.Instance.canEquipGun == true)
        {
            InventoryManager.Instance.AddGunToInventory(gunData);
            InventoryManager.Instance.UpdateEquipGun();
            gameObject.SetActive(false);
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

    public void OnItemBought()
    {
        BackPackManager.Instance.AddItemToBackPack(itemData);
        gameObject.SetActive(false);
    }


}
