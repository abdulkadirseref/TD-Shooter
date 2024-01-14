using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackpackUIScript : MonoBehaviour
{
    public BackPackManager backpackManager;
    public List<Image> backpackSlotImages;

    void Start()
    {
        backpackManager = FindObjectOfType<BackPackManager>();

        backpackManager.OnBackpackChanged += UpdateBackpackUI;
        UpdateBackpackUI();
    }

    void UpdateBackpackUI()
    {
        for (int i = 0; i < backpackSlotImages.Count; i++)
        {
            if (backpackManager != null && i < backpackManager.backpack.Count)
            {
                BaseItemData currentItem = backpackManager.backpack[i].item;

                // Check if the Image component is not null and not destroyed
                if (backpackSlotImages[i] != null && backpackSlotImages[i].gameObject != null)
                {
                    // Ensure currentItem.icon is not null before using it
                    if (currentItem != null && currentItem.itemImage != null)
                    {
                        backpackSlotImages[i].sprite = currentItem.itemImage;
                        backpackSlotImages[i].GetComponentInChildren<Text>().text = backpackManager.backpack[i].quantity.ToString();
                    }
                    else
                    {
                        // Handle the case where the item or its icon is null
                        Debug.LogWarning("Item or its icon is null.");
                    }
                }
                else
                {
                    // Handle the case where the Image component is null or destroyed
                    Debug.LogWarning("Image component is null or destroyed.");
                }
            }
            else
            {
                // Clear the image and quantity for empty slots
                if (backpackSlotImages[i] != null && backpackSlotImages[i].gameObject != null)
                {
                    backpackSlotImages[i].sprite = null;
                    backpackSlotImages[i].GetComponentInChildren<Text>().text = "";
                }
                else
                {
                    // Handle the case where the Image component is null or destroyed
                    Debug.LogWarning("Image component is null or destroyed.");
                }
            }
        }
    }
}
