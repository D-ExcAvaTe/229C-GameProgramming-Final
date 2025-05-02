using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private SurfaceEffector2D groundSurfaceEffector;
    [SerializeField] private float baseMoveSpeed,moveSpeed;
    private void Update()
    {
        moveSpeed = baseMoveSpeed * (1 + ((float)EnemySpawner.instance.stageLevel / 10));;
        groundSurfaceEffector.speed = moveSpeed;
        transform.position += new Vector3(moveSpeed * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        groundSurfaceEffector.speed = moveSpeed;
        transform.position = new Vector3(0, 0, 0);
    }
}
