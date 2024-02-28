using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BaseEnemy : MonoBehaviour
{
    public BaseEnemyData baseEnemyData;
    public float health;
    public int damage;
    public float moveSpeed;
    public int spawnDelay;
    public int startDelay;
    public int quantityPerSpawn;
    public float moveOffset;
    public float chanceToDropMaterial;
    public Transform playerPosition;
    public GameObject floatingTextPrefab;
    public GameObject material;

    private float timeBtwAttacks;
    public float startTimeBtwAttacks = 2f;
    public bool canDealDamage;



    private void Start()
    {
        health = baseEnemyData.health;
        damage = baseEnemyData.damage;
        moveSpeed = baseEnemyData.moveSpeed;
        spawnDelay = baseEnemyData.spawnDelay;
        startDelay = baseEnemyData.startDelay;
        quantityPerSpawn = baseEnemyData.quantityPerSpawn;
        chanceToDropMaterial = baseEnemyData.chanceToDropMaterial;
        playerPosition = FindObjectOfType<PlayerMovement>().transform;
    }


    private void Update()
    {
        Move();
        DamageDelay();
    }


    public void Move()
    {
        Vector2 direction = playerPosition.position - transform.position;
        float distance = direction.magnitude;
        if (distance > moveOffset)
        {
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }


    public void DropMaterial()
    {
        float randomValue = Random.value;

        if (chanceToDropMaterial >= randomValue)
        {
            Instantiate(material, transform.position, Quaternion.identity);
        }
    }


    private void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position + new Vector3(3, 0), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }


    private void Die()
    {
        if (health <= 0)
        {
            DropMaterial();
            Destroy(gameObject);
        }
    }


    private void DamageDelay()
    {
        if (timeBtwAttacks < 0)
        {
            canDealDamage = true;
            timeBtwAttacks = startTimeBtwAttacks;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Projectile projectile = collision.collider.GetComponent<Projectile>();
            health -= projectile.basegun.CalculateDamage();
            ShowDamage(projectile.basegun.CalculateDamage().ToString());
            projectile.objectPool.Release(projectile);
            Die();
        }
    }
}
