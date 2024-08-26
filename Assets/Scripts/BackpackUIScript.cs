using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackpackUIScript : MonoBehaviour
{
    public List<Image> backpackSlotImages;



    void Start()
    {      
        BackPackManager.OnBackpackChanged += UpdateBackpackUI;
        UpdateBackpackUI();
    }


    void UpdateBackpackUI()
    {
        for (int i = 0; i < backpackSlotImages.Count; i++)
        {
            if (BackPackManager.Instance != null && i < BackPackManager.Instance.backpack.Count)
            {
                BaseItemData currentItem = BackPackManager.Instance.backpack[i].item;

                // Check if the Image component is not null and not destroyed
                if (backpackSlotImages[i] != null && backpackSlotImages[i].gameObject != null)
                {
                    // Ensure currentItem.icon is not null before using it
                    if (currentItem != null && currentItem.itemImage != null)
                    {
                        backpackSlotImages[i].sprite = currentItem.itemImage;
                        backpackSlotImages[i].GetComponentInChildren<Text>().text = BackPackManager.Instance.backpack[i].quantity.ToString();
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
