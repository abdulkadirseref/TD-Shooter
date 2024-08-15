using UnityEngine;
using UnityEngine.UI;



public abstract class BaseGunCardData : ScriptableObject
{
    public GameObject cardPrefab;
    public string cardName;
    public Image cardImage;
    public string damage;
    public string totalGunDamage;
    public string coolDown;
    public string range;
    public string piercing;
    public string description;
    public int price;
    public float spawnProbability;
}
