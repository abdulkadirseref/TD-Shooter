using UnityEngine;



[CreateAssetMenu(menuName = "Stats/StatData")]
public class BaseStatData : ScriptableObject
{
    public int health;
    public int armor;
    public int damage;
    public int rangedDamage;
    public int attackSpeed;
    public int range;
    public int moveSpeed;
}
