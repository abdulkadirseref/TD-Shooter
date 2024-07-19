using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int materialAmount;
    public BaseGunCardData[] gunCardDatas;


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
}
