using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 20f;

    private float rotationY = 0;

    private Vector3 offset;

    void Start()
    {

    }

    void Update()
    {
        float modifiedRotationSpeed = rotationSpeed / Time.timeScale;
        if (Input.GetMouseButton(1) && Input.GetAxis("Mouse X") != 0)
        {
            rotationY += Input.GetAxis("Mouse X") * modifiedRotationSpeed * 10;
            rotationY %= 360;     
        }
        else
        {
            rotationY = 0;
        }

        transform.Rotate(0, rotationY * Time.deltaTime, 0);
    }
}
