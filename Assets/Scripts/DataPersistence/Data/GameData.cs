using System.Collections.Generic;
using UnityEngine;
using static BaseStatData;


[System.Serializable]
public class GameData
{
    public int material;
    public int waveIndex;
    public List<BaseGunData> gunData;
    public List<BackPackManager.ItemStack> itemData;
    public List<BaseGunCardData> lastGunCards;
    public List<BaseItemCardData> lastItemCards;
    public StatData statData;


    public GameData()
    {
        this.material = 0;
        this.waveIndex = 1;
        this.gunData = new List<BaseGunData>();
        this.itemData = new List<BackPackManager.ItemStack>();
        this.lastGunCards = new List<BaseGunCardData>();
        this.lastItemCards = new List<BaseItemCardData>();
        this.statData = new StatData();
    }
}
