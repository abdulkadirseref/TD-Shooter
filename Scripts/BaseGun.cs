using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public BaseGunData baseGunData;
    

    public float gunDamage;
    public int fireRate;
    public int range;
    public string gunName;
    public int piercing;
    public int bulletSpeed;
    public float timeBetweenShots;
    public GameObject bulletPrefab;



    public LayerMask targetMask;
    public Transform firePoint;
    public Collider2D nearestEnemyCollider = null;
    float minDistance = 500f;


    private void Start()
    {
        gunDamage = CalculateDamage();
        fireRate = baseGunData.fireRate;
        range = baseGunData.range;
        gunName = baseGunData.gunName;
        piercing = baseGunData.piercing;
        bulletSpeed = baseGunData.bulletSpeed;
        bulletPrefab = baseGunData.bulletPrefab;
    }


    private void Update()
    {
        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }
        DetectEnemies();
    }


    public void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            Vector2 initialPosition = firePoint.transform.position;
            Vector2 targetPosition = nearestEnemyCollider.transform.position;

            StartCoroutine(MoveBullet(bulletRb, initialPosition, targetPosition));

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
        int baseDamage = baseGunData.gunDamage + StatManager.Instance.rangedDamage;
        gunDamage = baseDamage + (baseDamage * (StatManager.Instance.damage / 100f));
        return gunDamage;
    }


    public void DetectEnemies()
    {
        Collider2D[] inRangeTarget = Physics2D.OverlapCircleAll(transform.position, range, targetMask);

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

            Shoot();
        }
        else
        {
            minDistance = 500f;
            nearestEnemyCollider = null;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseGunData.range);
    }

}
