using UnityEngine;



public class GunManager : MonoBehaviour
{
    public Transform playerTransform;

    public Transform[] gunSlots;

    private void OnEnable()
    {
        InstantiateGunsFromInventory();
    }


    
    public void InstantiateGunsFromInventory()
    {
        for (int i = 0; i < InventoryManager.Instance.inventory.Count; i++)
        {
            if (i < gunSlots.Length)
            {
                BaseGunData gunData = InventoryManager.Instance.inventory[i];
                GameObject gunPrefab = gunData.gunPrefab;

                if (gunPrefab != null)
                {
                    GameObject instantiatedGun = Instantiate(gunPrefab, gunSlots[i].position, gunSlots[i].rotation);
                    instantiatedGun.transform.SetParent(gunSlots[i]);
                }
            }
        }

    }
}
