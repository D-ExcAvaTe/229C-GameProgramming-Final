using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Slider healthSlider;
    public float health, maxHealth = 100, moveSpeed = 5f;

    private void Start()
    {
    }

    public void Init(int newHealth,float _moveSpeed)
    {
        maxHealth = newHealth;
        health = maxHealth;

        moveSpeed = _moveSpeed;
    }

    private void Update()
    {
        healthSlider.value = health / maxHealth;
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        AudioManager.instance.PlaySFX(9);
        if (health <= 0) Death();
    }

    private void Death()
    {
        ScoreManager.instance.AddScore(1);  
        AudioManager.instance.PlaySFX(1);
        
        Instantiate(deathFX, this.transform.position, quaternion.identity);
        Destroy(this.gameObject);
    }
}
