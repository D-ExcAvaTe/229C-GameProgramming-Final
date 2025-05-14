using System;
using System.Collections;
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    public Transform shootPoint;

    public GameObject target;

    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletFireDelay=0.3f,bulletFireTimer;
    [SerializeField] private float baseBulletFireDelay = 0.3f;
    [SerializeField] private float bulletFireMultiplier = 1f;
    [Space]

    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bulletFireDelay = baseBulletFireDelay * bulletFireMultiplier;
        if (bulletFireTimer > 0) bulletFireTimer -= Time.deltaTime;
        
        if (Input.GetMouseButton(0))
        {
            this.rb.AddForce(new Vector2(0,jumpForce));

            // แปลงตำแหน่งเมาส์ในจอ เป็นตำแหน่งในโลก 2D
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);

                if (bulletFireTimer > 0) return;
                bulletFireTimer = bulletFireDelay;
                AudioManager.instance.PlaySFX(15);

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                // สร้างกระสุนใหม่
                Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                Destroy(firedBullet.gameObject,3f);

                // ใส่ความเร็วให้กระสุน
                firedBullet.linearVelocity = projectileVelocity;
            }//hit.collider

        }//GetMouseButtonDown
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);

    }// CalculateProjectileVelocity

    public IEnumerator FireSpeedCoroutine(float multiplier, float duration)
    {
        bulletFireMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        bulletFireMultiplier = 1f;
    }

    public void StartFireCoroutine(float multiplier, float duration)
    {
        StartCoroutine(FireSpeedCoroutine(multiplier, duration));
    }
    
}
