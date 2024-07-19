using UnityEngine;
using UnityEngine.EventSystems;


public class GunIconInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BaseGunData baseGunData;
    public NewInfoCard newInfoCard;



    private void Start()
    {
        newInfoCard = GetComponentInChildren<NewInfoCard>();
        newInfoCard.gunNameText.text = baseGunData.gunName;
        newInfoCard.gunDamageText.text = "Damage : " + baseGunData.gunDamage.ToString();
        newInfoCard.fireRateText.text = "FireRate : " + baseGunData.fireRate.ToString();
        newInfoCard.rangeText.text = "Range : " + baseGunData.range.ToString();
        newInfoCard.piercingText.text = "Piercing : " + baseGunData.piercing.ToString();
    }


    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += SetRotation;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        newInfoCard.transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        newInfoCard.transform.rotation = Quaternion.Euler(0, 90, 0);
    }


    public void SetRotation()
    {
        if (newInfoCard != null)
        {
            newInfoCard.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
