using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 2f;
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Launch(Vector3 dir, Sprite rockSprite)
    {
        direction = dir.normalized;
        if (spriteRenderer != null)
            spriteRenderer.sprite = rockSprite;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


}
