using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneDirector : MonoBehaviour
{

    public void ActivateGunSelectMenu()
    {
        SceneManager.LoadScene(0);
    }

}
