using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Transform collider;
    [SerializeField] private float baseMoveSpeed,moveSpeed;
    private void Update()
    {
        moveSpeed = baseMoveSpeed * (1 + ((float)EnemySpawner.instance.stageLevel / 10));;
        collider.position += new Vector3(moveSpeed * Time.deltaTime, transform.position.y);
        transform.position += new Vector3(moveSpeed * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collider.position = new Vector3(0, -8.606f, 0);
        transform.position = new Vector3(0, 0, 0);
    }
}
