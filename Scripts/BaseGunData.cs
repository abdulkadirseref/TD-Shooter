using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGunData : ScriptableObject
{
    public string gunName;
    public string description;
    public int gunDamage;
    public int fireRate;
    public int range;
    public int piercing;
    public int bulletSpeed;
    public Sprite gunIcon;
    public GameObject gunPrefab;
    public GameObject bulletPrefab;
    public Rigidbody2D bulletRb;


    public abstract void AddToInventory(InventoryManager inventoryManager);

}
