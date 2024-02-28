using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class GunIconPanel : MonoBehaviour
{
    public Image[] gunIconImages;




    private void Update()
    {
        PopulateGunIcons();
    }


    public void PopulateGunIcons()
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
