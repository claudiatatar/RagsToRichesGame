using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 90f;

    [Header("Floating")]
    public float floatHeight = 0.3f;
    public float floatSpeed = 1.5f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate around the Y axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // Bob up and down using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
