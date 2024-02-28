using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public float health;
    public int damage;
    public float moveSpeed;
    public int spawnDelay;
    public int startDelay;
    public int quantityPerSpawn;
    public bool canSpawn;
    public float chanceToDropMaterial;
}
