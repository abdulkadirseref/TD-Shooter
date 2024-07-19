using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunIconPanel : MonoBehaviour
{
    public Image[] gunIconSlots;
    public Button[] upgradeButtons;


    private void Start()
    {
        ClearGunIcons();
        PopulateGunIcons();
    }


    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += PopulateGunIcons;
    }


    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= PopulateGunIcons;
    }


    public void ClearGunIcons()
    {
        foreach (var item in gunIconSlots)
        {
            item.sprite = null;
            GunIconInfo gunIconInfo = item.GetComponent<GunIconInfo>();
            if (gunIconInfo != null)
            {
                Destroy(gunIconInfo);
            }
        }
        foreach (var item in upgradeButtons)
        {
            item.gameObject.SetActive(false);

        }
        for (int i = 0; i < gunIconSlots.Length; i++)
        {
            if (i < upgradeButtons.Length)
            {
                upgradeButtons[i].onClick.RemoveAllListeners();
            }
        }
    }


    public void PopulateGunIcons()
    {
        ClearGunIcons();

        for (int i = 0; i < InventoryManager.Instance.inventory.Count; i++)
        {
            if (i < gunIconSlots.Length)
            {
                BaseGunData gunData = InventoryManager.Instance.inventory[i];
                if (gunData != null)
                {
                    gunIconSlots[i].sprite = gunData.gunIcon;
                    GunIconInfo gunIconInfo = gunIconSlots[i].gameObject.AddComponent<GunIconInfo>();
                    gunIconInfo.baseGunData = gunData;
                    Debug.Log("Populated gun icon: " + gunData.gunName);
                }
            }
        }

        // Check for duplicates
        Dictionary<Sprite, List<int>> spriteToSlotIndices = new Dictionary<Sprite, List<int>>();

        for (int i = 0; i < gunIconSlots.Length; i++)
        {
            if (gunIconSlots[i].sprite != null)
            {
                Sprite sprite = gunIconSlots[i].sprite;

                if (!spriteToSlotIndices.ContainsKey(sprite))
                {
                    spriteToSlotIndices[sprite] = new List<int>();
                }
                spriteToSlotIndices[sprite].Add(i);
            }
        }

        foreach (var pair in spriteToSlotIndices)
        {
            if (pair.Value.Count >= 2)
            {
                Debug.Log("Sprite " + pair.Key.name + " appears more than twice.");
                foreach (int index in pair.Value)
                {
                    GunIconInfo gunIconInfo = gunIconSlots[index].GetComponent<GunIconInfo>();
                    if (gunIconInfo != null && gunIconInfo.baseGunData.canUpgrade)
                    {
                        if (index < upgradeButtons.Length)
                        {
                            upgradeButtons[index].gameObject.SetActive(true);
                            int capturedIndex = index; // Capture the index for the lambda
                            upgradeButtons[index].onClick.AddListener(() => UpgradeGun(capturedIndex));
                        }
                    }
                }
            }
        }
    }


    public void UpgradeGun(int slotIndex)
    {
        if (slotIndex < gunIconSlots.Length)
        {
            Sprite sprite = gunIconSlots[slotIndex].sprite;
            if (sprite == null) return;

            // Find the two guns with the same icon in the inventory
            List<int> indicesToRemove = new List<int>();
            for (int i = 0; i < InventoryManager.Instance.inventory.Count; i++)
            {
                if (InventoryManager.Instance.inventory[i].gunIcon == sprite)
                {
                    indicesToRemove.Add(i);
                    if (indicesToRemove.Count == 2) break;
                }
            }

            if (indicesToRemove.Count == 2)
            {
                GunIconInfo gunIconInfo = gunIconSlots[slotIndex].GetComponent<GunIconInfo>();
                if (gunIconInfo == null || gunIconInfo.baseGunData == null) return;
                BaseGunData upgradedGun = gunIconInfo.baseGunData.upgradedGun;

                // Remove the two guns from the inventory
                InventoryManager.Instance.inventory.RemoveAt(indicesToRemove[1]);
                InventoryManager.Instance.inventory.RemoveAt(indicesToRemove[0]);
                InventoryManager.Instance.AddGunToInventory(upgradedGun);
                PopulateGunIcons();
            }
            else
            {
                Debug.LogWarning("Not enough guns to upgrade.");
            }
        }
    }
}
