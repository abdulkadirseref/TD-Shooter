using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseGunCard : MonoBehaviour
{
    public BaseGunCardData baseGunCardData;
    public BaseGunData baseGunData;

    public Image cardImage;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardTypeNameText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI coolDownText;
    public TextMeshProUGUI piercingText;
    public TextMeshProUGUI descriptionText;
    public Text priceText;



    private void Start()
    {
        int baseDamage = baseGunData.gunDamage + StatManager.Instance.baseStatData.rangedDamage;
        float gunDamage = baseDamage + (baseDamage * (StatManager.Instance.baseStatData.damage / 100f));

        damageText.text = "Damage: " + gunDamage.ToString();
        baseGunCardData.damage = gunDamage.ToString();
        baseGunCardData.piercing = baseGunData.piercing.ToString();
        baseGunCardData.coolDown = baseGunData.fireRate.ToString();
        baseGunCardData.range = baseGunData.range.ToString();
    }

    private void OnEnable()
    {
        cardNameText.text = baseGunData.name;
        cardImage.sprite = baseGunData.gunIcon;
        rangeText.text = "Range : " + baseGunData.range.ToString();
        coolDownText.text = "CoolDown : " + baseGunData.fireRate.ToString();
        piercingText.text = "Piercing : " + baseGunData.piercing.ToString();
        descriptionText.text = baseGunData.description;
        priceText.text = "Price: " + baseGunCardData.price.ToString();
    }
}
