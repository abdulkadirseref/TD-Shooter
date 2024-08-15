using UnityEngine;



public class StatManager : MonoBehaviour
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


    public void SetStatData(BaseStatData statData)
    {
        baseStatData = statData;
        gunSelectPanel.SetActive(true);
        characterImage.SetActive(false);
    }
}
