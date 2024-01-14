using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGunCardData : ScriptableObject
{
    public string cardName;
    public Image cardImage;
    public string damage;
    public string totalGunDamage;
    public string coolDown;
    public string range;
    public string piercing;
    public string description;
    public Button buyButton;
}
