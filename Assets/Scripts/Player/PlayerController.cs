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

    private PlayerStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (playerStats.isDead) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        UpdateSpriteDirection();

        if (!playerStats.hasSelectedPerk) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = fireCooldown;
        }
    }

    void FixedUpdate()
    {
        if (playerStats.isDead) return;

        float totalSpeed = moveSpeed;

        if (GameManager.Instance != null)
        {
            totalSpeed += GameManager.Instance.GetPlayerSpeedBonus();
        }

        rb.MovePosition(rb.position + movement * totalSpeed * Time.fixedDeltaTime);
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
