using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectionEvent : MonoBehaviour
{
    public delegate void GunSelectedHandler(BaseGunData gunData);
    public static event GunSelectedHandler OnGunSelected;

    public static void InvokeGunSelection(BaseGunData gunData)
    {
        OnGunSelected?.Invoke(gunData);
    }
}
