using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    public Projectile2D projectile;
    public int health, maxHealth = 100;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private float hitDelay=1f, hitTimer;
    private void Start()
    {
        health = maxHealth;
        healthSlider.value = (float)health / maxHealth;
        hitTimer = 0;
    }

    private void Update()
    {
        if (hitTimer < hitDelay) hitTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (hitTimer < hitDelay) return;
        
            TakeDamage(25);

            EnemySpawner.instance.stageLevel -= 4;
            hitTimer = 0;
        }

        if (other.CompareTag("Gem"))
        {
            AudioManager.instance.PlaySFX(4);
            ScoreManager.instance.AddGem(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int _damage)
    {
        if (projectile.isShielding)
        {
            projectile.StopShield();
            AudioManager.instance.PlaySFX(21);
            return;
        }

        health -= _damage;
        healthSlider.value = (float)health / maxHealth;
        if (health <= 0) Death();

        ScoreManager.instance.ShowHurtFX();
        AudioManager.instance.PlaySFX(20);
    }
    
    public void TakeHeal(int _heal)
    {
        health += _heal;
        healthSlider.value = (float)health / maxHealth;
        if (health > maxHealth) health = maxHealth;
        
        AudioManager.instance.PlaySFX(8);
    }

    void Death()
    {
        ScoreManager.instance.GameOver();
        Destroy(this.gameObject);
        
        AudioManager.instance.PlaySFX(5);
        AudioManager.instance.PlaySFX(1);
    }
}
