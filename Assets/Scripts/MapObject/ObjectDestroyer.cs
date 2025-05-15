using System;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PowerUp>() != null || other.gameObject.GetComponent<Obstacle>() != null ||
            other.gameObject.GetComponent<Enemy>() != null ||
            other.gameObject.GetComponent<Gem>() != null)
            Destroy(other.gameObject);
    }
}
