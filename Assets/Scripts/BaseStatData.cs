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

    [Header("Default Values")]
    public int defaultHealth;
    public int defaultArmor;
    public int defaultDamage;
    public int defaultRangedDamage;
    public int defaultAttackSpeed;
    public int defaultRange;
    public int defaultMoveSpeed;


    [System.Serializable]
    public class StatData
    {
        public int health;
        public int armor;
        public int damage;
        public int rangedDamage;
        public int attackSpeed;
        public int range;
        public int moveSpeed;


        public StatData()
        {
            this.health = 0;
            this.armor = 0;
            this.damage = 0;
            this.rangedDamage = 0;
            this.attackSpeed = 0;
            this.range = 0;
            this.moveSpeed = 0;
        }
    }
}
