using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int health;
    public int armor;
    public int damage;
    public int rangedDamage;
    public int attackSpeed;
    public int range;
    public int moveSpeed;



    private void Start()
    {
        health = StatManager.Instance.baseStatData.health;
        armor = StatManager.Instance.baseStatData.armor;
        damage = StatManager.Instance.baseStatData.damage;
        rangedDamage = StatManager.Instance.baseStatData.rangedDamage;
        attackSpeed = StatManager.Instance.baseStatData.attackSpeed;
        range = StatManager.Instance.baseStatData.range;
        moveSpeed = StatManager.Instance.baseStatData.moveSpeed;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<BaseEnemy>().canDealDamage == true)
        {
            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
            health -= enemy.damage;
            enemy.canDealDamage = false;
        }
    }
}
