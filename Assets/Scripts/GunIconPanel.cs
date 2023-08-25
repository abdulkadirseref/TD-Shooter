using UnityEngine;
using UnityEngine.UI;

public class GunIconPanel : MonoBehaviour
{
    public Image[] gunIconImages; // UI elements to display gun icons

    private void Update()
    {
        PopulateGunIcons();
    }

    private void PopulateGunIcons()
    {
        for (int i = 0; i < InventoryManager.Instance.inventory.Count; i++)
        {
            if (i < gunIconImages.Length)
            {
                BaseGunData gunData = InventoryManager.Instance.inventory[i];

                if (gunData != null)
                {
                    gunIconImages[i].sprite = gunData.gunIcon;
                }
            }
        }
    }
}
