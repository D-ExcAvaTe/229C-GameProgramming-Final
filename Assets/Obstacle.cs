using UnityEngine;

public class Obstacle : MonoBehaviour
{
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
