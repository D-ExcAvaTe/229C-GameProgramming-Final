using System;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDmg = 5;
    public GameObject hitFx;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("BulletHit")) return;
        transform.parent = other.transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy!=null)
        {
            enemy.TakeDamage(bulletDmg);
            Instantiate(hitFx, enemy.transform.position, quaternion.identity, enemy.transform);
            Destroy(gameObject);
        }
    }
}
