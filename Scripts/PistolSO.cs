using UnityEngine;


[CreateAssetMenu(fileName = "Pistol", menuName = "Gun/Pistol")]
public class PistolSO : BaseGunData
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
