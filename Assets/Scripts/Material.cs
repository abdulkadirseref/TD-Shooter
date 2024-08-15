using UnityEngine;



public class Material : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.materialAmount += 2;
            Destroy(gameObject);
        }
    }
}
