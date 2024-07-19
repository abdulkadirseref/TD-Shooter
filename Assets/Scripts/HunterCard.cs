using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HunterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject statPanel;



    public void OnPointerEnter(PointerEventData eventData)
    {
        statPanel.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        statPanel.SetActive(false);
    }
}
