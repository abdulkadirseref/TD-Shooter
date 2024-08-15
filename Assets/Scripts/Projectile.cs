using System.Collections;
using UnityEngine;
using UnityEngine.Pool;



public class Projectile : MonoBehaviour
{
    public IObjectPool<Projectile> objectPool;
    public IObjectPool<Projectile> ObjectPool { set => objectPool = value; }
    [SerializeField] private float delayTime;
    public BaseGun basegun;



    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(delayTime));
    }


    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.Release(this);
    }
}
