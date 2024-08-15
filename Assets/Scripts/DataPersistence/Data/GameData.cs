using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int material;
    public int waveIndex;
    public List<BaseGunData> gunData;


    public GameData()
    {
        this.material = 0;
        this.waveIndex = 1;
        this.gunData = new List<BaseGunData>(); 
    }
}
