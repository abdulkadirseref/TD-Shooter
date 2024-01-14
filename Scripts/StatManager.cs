using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    public BaseStatData baseStatData;

    public int health;
    public int armor;
    public int damage;
    public int rangedDamage;
    public int attackSpeed;
    public int range;
    public int moveSpeed;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        health = baseStatData.health;
        armor = baseStatData.armor;
        damage = baseStatData.damage;
        rangedDamage = baseStatData.rangedDamage;
        attackSpeed = baseStatData.attackSpeed;
        range = baseStatData.range;
        moveSpeed = baseStatData.moveSpeed;
    }
}
