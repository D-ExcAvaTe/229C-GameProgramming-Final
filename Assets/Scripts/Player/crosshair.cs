using UnityEngine;

public class crosshair : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
