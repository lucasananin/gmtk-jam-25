using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        Destroy(gameObject, 5f);
    }
}
