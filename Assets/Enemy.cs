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
        Init(100);
    }

    void Init(int newHealth)
    {
        maxHealth = newHealth;
        health = maxHealth;
    }

    private void Update()
    {
        healthSlider.value = health / maxHealth;
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Death();
    }

    private void Death()
    {
        Instantiate(deathFX, this.transform.position, quaternion.identity);
        Destroy(this.gameObject);
    }
}
