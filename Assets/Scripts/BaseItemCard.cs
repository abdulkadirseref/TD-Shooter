using UnityEngine;



public class BaseItemCard : MonoBehaviour
{
    public BaseItemCardData baseItemCardData;
    public BaseItemData baseItemData;



    public void OnCardBought()
    {
        StatManager.Instance.baseStatData.damage += baseItemData.damage;
        StatManager.Instance.baseStatData.rangedDamage += baseItemData.rangedDamage;
        StatManager.Instance.baseStatData.armor += baseItemData.armor;
        StatManager.Instance.baseStatData.health += baseItemData.health;
        StatManager.Instance.baseStatData.attackSpeed += baseItemData.attackSpeed;
        StatManager.Instance.baseStatData.moveSpeed += baseItemData.moveSpeed;
        StatManager.Instance.baseStatData.range += baseItemData.range;
    }
}
