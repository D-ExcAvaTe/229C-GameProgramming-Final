using System;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Transform collider;
    [SerializeField] private float moveSpeed;
    private void Update()
    {
        collider.position += new Vector3(moveSpeed * Time.deltaTime, transform.position.y);
        transform.position += new Vector3(moveSpeed * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collider.position = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
    }
}
