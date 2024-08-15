using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }
    public int materialAmount;
    public List<BaseGunCardData> gunCardDatas;
    public List<BaseItemCardData> itemCards;
    public List<ScriptableObject> concatenatedList = new List<ScriptableObject>();


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
        concatenatedList.AddRange(gunCardDatas);
        concatenatedList.AddRange(itemCards);
    }


    private void OnEnable()
    {
        WaveManager.OnTimerReachesZero += SetGunPrice;
    }


    private void OnDisable()
    {
        WaveManager.OnTimerReachesZero -= SetGunPrice;
    }


    public void SetGunPrice()
    {
        foreach (BaseGunCardData gunData in gunCardDatas)
        {
            // gunData.price += 2;
        }
    }


    public void LoadData(GameData data)
    {
        this.materialAmount = data.material;
    }


    public void SaveData(ref GameData data)
    {
        data.material = this.materialAmount;
    }
}
