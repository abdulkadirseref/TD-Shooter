using System;
using System.Collections.Generic;
using UnityEngine;



public class BackPackManager : MonoBehaviour, IDataPersistence
{
    public static BackPackManager Instance { get; private set; }
    public List<ItemStack> backPack = new List<ItemStack>();

    public static event Action OnBackpackChanged;
    public bool isEventInvoked;



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

    private void Start()
    {
        
    }


    public void AddItemToBackPack(BaseItemData baseItemData)
    {
        ItemStack existingItemStack = backPack.Find(itemStack => itemStack.item == baseItemData);

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
            backPack.Add(newItemStack);
        }
        OnBackpackChanged?.Invoke();
    }

    public void LoadData(GameData data)
    {
        this.backPack = data.itemData;
        Debug.Log("Loaded Backpack Items:");
        foreach (var itemStack in backPack)
        {
            Debug.Log($"Item: {itemStack.item.name}, Quantity: {itemStack.quantity}");
        }
    }

    public void SaveData(ref GameData data)
    {
        data.itemData = this.backPack;
    }

    [System.Serializable]
    public class ItemStack
    {
        public BaseItemData item;
        public int quantity;
    }
}