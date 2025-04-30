using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xSpeed = 1;
    void Update()
    {
        float xDir = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        Vector3 moveDirection = new Vector3(xDir * xSpeed, 0f, 0f);
        transform.position += moveDirection * Time.deltaTime;
    }
}
