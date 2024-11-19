using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    public Text timerText;
    public Text waveText;
    public Text damageText;
    public TextMeshProUGUI materialText;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;


    private void OnEnable()
    {
        PlayerManager.OnDamageTaken += UpdateHealthBar;
    }

    private void OnDisable()
    {
        PlayerManager.OnDamageTaken -= UpdateHealthBar;
    }


    private void Start()
    {
        waveText.text = "Wave " + WaveManager.Instance.waveIndex.ToString();
        if (healthText != null)
        {
            healthText.text = playerManager.health + "/" + StatManager.Instance.baseStatData.health;
        }
    }


    private void Update()
    {
        timerText.text = WaveManager.Instance.waveTimer.ToString("F0");
        materialText.text = GameManager.Instance.materialAmount.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)playerManager.health / StatManager.Instance.baseStatData.health;
        healthText.text = playerManager.health + "/" + StatManager.Instance.baseStatData.health;
    }
}
