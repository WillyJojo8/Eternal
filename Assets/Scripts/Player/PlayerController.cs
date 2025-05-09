using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteSide;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.3f;

    private float fireTimer;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastLookDirection = Vector2.right;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        UpdateSpriteDirection();

        fireTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = fireCooldown;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateSpriteDirection()
    {
        if (movement.x != 0)
        {
            spriteRenderer.sprite = spriteSide;
            spriteRenderer.flipX = movement.x > 0;
            lastLookDirection = movement.x > 0 ? Vector2.right : Vector2.left;
        }
        else if (movement.y > 0)
        {
            spriteRenderer.sprite = spriteUp;
            spriteRenderer.flipX = false;
            lastLookDirection = Vector2.up;
        }
        else if (movement.y < 0)
        {
            spriteRenderer.sprite = spriteDown;
            spriteRenderer.flipX = false;
            lastLookDirection = Vector2.down;
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.direction = lastLookDirection;
    }
}