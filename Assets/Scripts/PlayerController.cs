using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    private float horizontal;
    public float speed = 5f;
    public Joystick joystick;
    private bool isFacingRight = true;

    public Transform gun;
    public GameObject bulletPrefab;
    public int bulletCount = 10;

    [SerializeField] HealthBar healthBar;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 movement = new Vector3(horizontal, vertical, 0f) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        Flip();
    }
    
    public void ShootGun()
    {
        Shoot();
    }

    void Shoot()
    {
        if (bulletCount > 0)
        {
            Instantiate(bulletPrefab, gun.position, gun.rotation);
            bulletCount--;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
