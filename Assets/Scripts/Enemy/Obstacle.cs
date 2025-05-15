using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isSpinning = false;

    public void Init(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }

    private void Update()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);
        if (isSpinning) transform.Rotate(0, 0, (moveSpeed*10) * Time.deltaTime, Space.World);
    }

}
