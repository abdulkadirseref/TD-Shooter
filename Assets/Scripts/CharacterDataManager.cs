using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{
    public List<BaseStatData> baseStatData;



    public void SetValuesToDefault()
    {
        foreach (BaseStatData item in baseStatData)
        {
            item.health = item.defaultHealth;
            item.armor = item.defaultArmor;
            item.attackSpeed = item.defaultAttackSpeed;
            item.damage = item.defaultDamage;
            item.range = item.defaultRange;
            item.rangedDamage = item.defaultRangedDamage;
            item.moveSpeed = item.defaultMoveSpeed;
        }
    }
}
