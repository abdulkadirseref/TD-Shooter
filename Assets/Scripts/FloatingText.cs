using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.3f;


    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
