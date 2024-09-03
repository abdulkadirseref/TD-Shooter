using UnityEngine;



public class StatManager : MonoBehaviour, IDataPersistence
{
    public static StatManager Instance { get; private set; }
    public BaseStatData baseStatData;
    public GameObject gunSelectPanel;
    public GameObject characterImage;


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
    }

    private void Start()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
    }

    public void SetStatData(BaseStatData statData)
    {
        baseStatData = statData;
        gunSelectPanel.SetActive(true);
        characterImage.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        if (baseStatData != null)
        {
            baseStatData.health = data.statData.health;
            baseStatData.attackSpeed = data.statData.attackSpeed;
            baseStatData.rangedDamage = data.statData.rangedDamage;
            baseStatData.damage = data.statData.damage;
            baseStatData.armor = data.statData.armor;
            baseStatData.moveSpeed = data.statData.moveSpeed;
            baseStatData.range = data.statData.range;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (baseStatData != null)
        {
            data.statData.health = baseStatData.health;
            data.statData.attackSpeed = baseStatData.attackSpeed;
            data.statData.moveSpeed = baseStatData.moveSpeed;
            data.statData.range = baseStatData.range;
            data.statData.armor = baseStatData.armor;
            data.statData.damage = baseStatData.damage;
            data.statData.rangedDamage = baseStatData.rangedDamage;
        }
    }
}
