using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunSelectIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject gunPanel;



    public void OnPointerEnter(PointerEventData eventData)
    {
        gunPanel.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        gunPanel.SetActive(false);
    }
}
