using UnityEngine;

public class myCamera : MonoBehaviour
{

    public GameObject target;
    public float distance = 10.0f;
    public float sensitivity = 1.0f;
    public float zoomSpeed = 1.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 20.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * sensitivity;
            y -= Input.GetAxis("Mouse Y") * sensitivity;
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.transform.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
