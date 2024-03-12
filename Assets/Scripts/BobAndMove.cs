using UnityEngine;

public class BobAndMove : MonoBehaviour
{
    public float bobSpeed = 1f; // Speed of bobbing motion
    public float bobHeight = 0.5f; // Height of bobbing motion
    public float moveSpeed = 1f; // Speed of horizontal movement
    public float moveRange = 1f; // Range of horizontal movement
    public float rotationSpeed = 45f; // Speed of rotation in degrees per second
    public float rotationRange = 30f; // Range of rotation in degrees
    public float scaleFactor = 2f; // Scale factor for enlarging the prefab

    private Vector3 initialPosition;
    private float startTime;

    void Start()
    {
        initialPosition = transform.position;
        startTime = Time.time;

        // Enlarge the prefab
        transform.localScale *= scaleFactor;
    }

    void Update()
    {
        // Bobbing motion
        float yOffset = Mathf.Sin((Time.time - startTime) * bobSpeed) * bobHeight;
        Vector3 bobbingPosition = initialPosition + new Vector3(0f, yOffset, 0f);
        transform.position = bobbingPosition;

        // Horizontal movement
        float xOffset = Mathf.Sin((Time.time - startTime) * moveSpeed) * moveRange;
        Vector3 movement = new Vector3(xOffset, 0f, 0f);
        transform.position += movement * Time.deltaTime;

        // Rotation
        float rotationOffset = Mathf.Sin((Time.time - startTime) * rotationSpeed) * rotationRange;
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationOffset);
        transform.rotation = rotation;
    }
}
