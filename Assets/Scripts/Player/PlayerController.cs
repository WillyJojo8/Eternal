﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.3f;
    public float spreadFireCooldown = 1.0f;  

    public Animator animator;
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

        if (movement != Vector2.zero)
            lastLookDirection = movement;

        UpdateSpriteDirection();


        if (!playerStats.hasSelectedPerk) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = playerStats.hasSpreadShot 
            ? spreadFireCooldown 
            : fireCooldown;
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
    // Actualizar los parámetros del Animator
    animator.SetFloat("MoveX", movement.x);
    animator.SetFloat("MoveY", movement.y);

    // Control de sprites visuales (no afecta Animator directamente)
    if (movement.x > 0.1f)
    {
        spriteRenderer.sprite = spriteRight;
    }
    else if (movement.x < -0.1f)
    {
        spriteRenderer.sprite = spriteLeft;
    }
    else if (movement.y > 0.1f)
    {
        spriteRenderer.sprite = spriteUp;
    }
    else if (movement.y < -0.1f)
    {
        spriteRenderer.sprite = spriteDown;
    }
}



    void FireBullet()
    {

        if (playerStats.hasSpreadShot)
    {
        var perk = SpreadShotPerk.Current;
        if (perk != null && perk.pelletCount > 1)
        {
            // Calculamos el ángulo inicial y paso
            float half = perk.angleSpread / 2f;
            float step = perk.pelletCount > 1
                ? perk.angleSpread / (perk.pelletCount - 1)
                : 0f;

            for (int i = 0; i < perk.pelletCount; i++)
            {
                // Ángulo relativo respecto a la última dirección
                float angle = -half + step * i;
                // Rotamos la dirección base
                Vector2 dir = Quaternion.Euler(0, 0, angle) * lastLookDirection;

                // Instanciamos y configuramos la bala
                GameObject pellet = Instantiate(
                    bulletPrefab,
                    firePoint.position,
                    Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)
                );
                Bullet bs = pellet.GetComponent<Bullet>();
                bs.direction = dir;

                // Copiar otros perks si quieres:
                if (playerStats.hasPiercingShot)
                    bs.pierceCount = PiercingShotPerk.Current?.pierceCount ?? 0;
            }
            return;
        }
    }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.direction = lastLookDirection;


        if (playerStats.hasPiercingShot)
        {
            var perk = PiercingShotPerk.Current;  
            if (perk != null)
                bulletScript.pierceCount = perk.pierceCount;
        }
    }
}
