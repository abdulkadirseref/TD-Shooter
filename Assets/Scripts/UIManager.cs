using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timerText;
    public Text waveText;
    public Text damageText;
    public TextMeshProUGUI materialText;



    private void Start()
    {
        waveText.text = "Wave " + WaveManager.Instance.waveIndex.ToString();
    }


    private void Update()
    {
        timerText.text = WaveManager.Instance.waveTimer.ToString("F0");
        materialText.text = GameManager.Instance.materialAmount.ToString();
    }
}
