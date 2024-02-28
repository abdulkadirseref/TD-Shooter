using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseGun : MonoBehaviour
{
    public BaseGunData baseGunData;
    public float gunDamage;
    public float fireRate;
    public int range;
    public string gunName;
    public int piercing;
    public int bulletSpeed;
    public float timeBetweenShots;
    public LayerMask targetMask;
    public Transform firePoint;
    public Collider2D nearestEnemyCollider = null;
    float minDistance = 100f;
    private IObjectPool<Projectile> objectPool;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 20;
    public bool canShoot;





    private void Awake()
    {
        objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
        collectionCheck, defaultCapacity, maxSize);
    }


    private void Start()
    {
        gunDamage = CalculateDamage();
        fireRate = baseGunData.fireRate;
        range = baseGunData.range + StatManager.Instance.baseStatData.range;
        gunName = baseGunData.gunName;
        piercing = baseGunData.piercing;
        bulletSpeed = baseGunData.bulletSpeed;
    }


    private void Update()
    {
        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }

        DetectEnemies();

        if (nearestEnemyCollider == null)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }


    private Projectile CreateProjectile()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }


    public void OnReleaseToPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }


    private void OnGetFromPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }


    private void OnDestroyPooledObject(Projectile pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }


    public void Shoot()
    {
        if (timeBetweenShots <= 0 && canShoot == true && nearestEnemyCollider != null)
        {
            Projectile bulletObject = objectPool.Get();
            Rigidbody2D bulletRb = bulletObject.GetComponent<Rigidbody2D>();
            bulletObject.transform.position = firePoint.transform.position;
            Vector2 targetPosition = nearestEnemyCollider.transform.position;

            StartCoroutine(MoveBullet(bulletRb, bulletObject.transform.position, targetPosition));
            bulletObject.Deactivate();
            timeBetweenShots = fireRate;
        }
    }


    private IEnumerator MoveBullet(Rigidbody2D bulletRb, Vector2 initialPosition, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = Vector2.Distance(initialPosition, targetPosition) / bulletSpeed;

        while (elapsedTime < duration)
        {
            // Calculate lerp progress (0 to 1)
            float t = elapsedTime / duration;

            // Lerp the bullet's position from initial to target position
            bulletRb.MovePosition(Vector2.Lerp(initialPosition, targetPosition, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    public float CalculateDamage()
    {
        int baseDamage = baseGunData.gunDamage + StatManager.Instance.baseStatData.rangedDamage;
        gunDamage = baseDamage + (baseDamage * (StatManager.Instance.baseStatData.damage / 100f));
        return gunDamage;
    }


    public void RotateGun()
    {
        // Rotate the gun towards the nearest enemy
        if (nearestEnemyCollider != null)
        {
            // Calculate the direction from the gun to the enemy
            Vector3 directionToEnemy = nearestEnemyCollider.transform.position - transform.position;
            directionToEnemy.Normalize();

            // Calculate the angle in radians
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x);

            // Convert the angle from radians to degrees
            float angleInDegrees = angle * Mathf.Rad2Deg;

            // Rotate the gun towards the enemy without changing the scale
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleInDegrees));

            // Check if the Z rotation is greater than 90 degrees or less than -90 degrees
            if (transform.rotation.eulerAngles.z > 90f && transform.rotation.eulerAngles.z < 270f)
            {
                transform.Rotate(180f, 0f, 0f);
            }
        }
    }


    public void DetectEnemies()
    {
        Collider2D[] inRangeTarget = Physics2D.OverlapCircleAll(transform.position, range, targetMask);

        minDistance = 100f;
        nearestEnemyCollider = null;

        if (inRangeTarget.Length != 0)
        {
            foreach (Collider2D collider in inRangeTarget)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemyCollider = collider;
                }
            }
            RotateGun();
            Shoot();
        }
        else
        {
            minDistance = 100f;
            nearestEnemyCollider = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
