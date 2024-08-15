using UnityEngine;



public abstract class BaseItemData : ScriptableObject
{
    public Sprite itemImage;
    public int itemID;
    public int price;
    public int qualityLevel;
    public int damage;
    public int health;
    public int armor;
    public int rangedDamage;
    public int range;
    public int attackSpeed;
    public int moveSpeed;


    public abstract void AddToBackpack(BackPackManager backPackManager);
}
