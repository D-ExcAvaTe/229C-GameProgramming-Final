using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int health, maxHealth = 100;
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
        if(!other.CompareTag("Obstacle")) return;
        if (hitTimer < hitDelay) return;
        
        TakeDamage(25);

        EnemySpawner.instance.stageLevel -= 4;
        hitTimer = 0;
    }

    void TakeDamage(int _damage)
    {
        health -= _damage;
        healthSlider.value = (float)health / maxHealth;
        if (health <= 0) Death();
        
        AudioManager.instance.PlaySFX(20);
    }

    void Death()
    {
        ScoreManager.instance.GameOver();
        Destroy(this.gameObject);
        
        AudioManager.instance.PlaySFX(5);
        AudioManager.instance.PlaySFX(1);
    }
}
