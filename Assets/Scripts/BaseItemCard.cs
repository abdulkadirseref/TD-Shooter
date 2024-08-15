using UnityEngine;



public class BaseItemCard : MonoBehaviour
{
    public BaseItemCardData baseItemCardData;
    public BaseStatData baseStatData;



    public void OnCardBought()
    {
        baseStatData.damage += baseItemCardData.damage;
        baseStatData.health += baseItemCardData.health;
        baseStatData.range += baseItemCardData.range;
        baseStatData.attackSpeed += baseItemCardData.attackSpeed;
        baseStatData.armor += baseItemCardData.armor;
        baseStatData.rangedDamage += baseItemCardData.rangedDamage;
    }
}
