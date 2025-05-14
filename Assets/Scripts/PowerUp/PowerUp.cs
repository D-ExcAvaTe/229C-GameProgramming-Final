using System;
using UnityEngine;

abstract class PowerUp : MonoBehaviour
{
    abstract public void DoPowerUp(Player player);

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Done Laew");
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            DoPowerUp(player);
            Debug.Log("Done Gu");
            Destroy(gameObject);
        }
    }
    
    public float moveSpeed = 5f;

    public void Init(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }

    private void Update()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
    }
}
