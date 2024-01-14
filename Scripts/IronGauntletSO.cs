using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Items", menuName = "Item/IronGauntlet")]
public class IronGauntletSO : BaseItemData
{
    public override void AddToBackpack(BackPackManager backPackManager)
    {
        backPackManager.AddItemToBackPack(this);
    }
}
