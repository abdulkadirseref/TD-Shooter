using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager Instance { get; private set; }
    public List<ItemStack> backpack = new List<ItemStack>();

    public event Action OnBackpackChanged;

    [System.Serializable]
    public class ItemStack
    {
        public BaseItemData item;
        public int quantity;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToBackPack(BaseItemData baseItemData)
    {
        ItemStack existingItemStack = backpack.Find(itemStack => itemStack.item == baseItemData);

        if (existingItemStack != null)
        {
            existingItemStack.quantity++;
        }
        else
        {
            ItemStack newItemStack = new ItemStack
            {
                item = baseItemData,
                quantity = 1
            };

            backpack.Add(newItemStack);
        }

        OnBackpackChanged?.Invoke();
    }
}