using UnityEngine;



[CreateAssetMenu(fileName = "Rifle", menuName = "Gun/Rifle")]
public class RifleSO : BaseGunData
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
