using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneDirector : MonoBehaviour
{

    public void ActivateGunSelectMenu()
    {
        SceneManager.LoadScene(2);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(3);
        WaveManager.Instance.StartWave();
    }
}
