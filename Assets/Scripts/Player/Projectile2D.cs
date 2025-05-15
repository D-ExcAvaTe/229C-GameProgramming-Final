using System.Collections;
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject target;

    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletFireDelay = 0.3f, bulletFireTimer;
    [SerializeField] private float baseBulletFireDelay = 0.3f;
    [SerializeField] private float bulletFireMultiplier = 1f;
    [Space]

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -9.81f;
    private Vector2 velocity;
   
    [Space]
    [SerializeField] private Animator shieldObject;
    public bool isShielding = false;

    private Coroutine fireCoroutine, shieldCoroutine;
    void Update()
    {
        HandleJump();
        HandleShooting();
        HandleShielding();
    }

    private void HandleShooting()
    {
        bulletFireDelay = baseBulletFireDelay * bulletFireMultiplier;
        if (bulletFireTimer > 0) bulletFireTimer -= Time.deltaTime;
        
        if (Input.GetMouseButton(0))
        {
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

                Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                Destroy(firedBullet.gameObject, 3f);

                firedBullet.velocity = projectileVelocity;
            }
        }
    }

    void HandleJump()
    {
        if (Input.GetMouseButton(0))
            velocity.y = jumpForce;
        else
            velocity.y += gravity * Time.deltaTime;


        if (transform.position.y <= -3.4) transform.position = new Vector2(transform.position.x, -3.4f);
        if (transform.position.y >= 3.1) transform.position = new Vector2(transform.position.x, 3.1f);
        
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(gravity) * time;

        return new Vector2(velocityX, velocityY);
    }

    //Firespeed บัฟ
    public IEnumerator FireSpeedCoroutine(float multiplier, float duration)
    {
        bulletFireMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        bulletFireMultiplier = 1f;
    }

    public void StartFireCoroutine(float multiplier, float duration)
    {
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
        fireCoroutine = StartCoroutine(FireSpeedCoroutine(multiplier, duration));
    }
    //Shield บัฟ
    private void HandleShielding()
    {
        shieldObject.SetBool("isAppear", isShielding);
    }

    public void StopShield()
    {
        StopCoroutine(shieldCoroutine);
        isShielding = false;
    }
    public IEnumerator ShieldCoroutine(float duration)
    {
        isShielding = true;
        yield return new WaitForSeconds(duration);
        isShielding = false;
    }

    public void StartShieldCoroutine(float duration)
    {
        if (shieldCoroutine != null) StopCoroutine(shieldCoroutine);
        shieldCoroutine = StartCoroutine(ShieldCoroutine(duration));
    }
}