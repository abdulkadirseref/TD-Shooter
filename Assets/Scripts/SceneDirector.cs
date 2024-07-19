using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneDirector : MonoBehaviour
{

    public void ActivateGunSelectMenu()
    {
        SceneManager.LoadScene(1);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(2);
        WaveManager.Instance.StartWave();
        GameManager.Instance.materialAmount = 0;
    }
}
