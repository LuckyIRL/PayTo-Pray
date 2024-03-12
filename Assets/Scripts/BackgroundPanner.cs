using UnityEngine;

public class BackgroundPanner : MonoBehaviour
{
    public Transform[] pathPoints; // Array of waypoints defining the path
    public float panSpeed = 1f; // Speed at which the background image pans

    private int currentPathIndex = 0; // Index of the current waypoint

    void Update()
    {
        // Move towards the current waypoint
        transform.position = Vector3.Lerp(transform.position, pathPoints[currentPathIndex].position, Time.deltaTime * panSpeed);

        // If reached the current waypoint, move to the next waypoint
        if (Vector3.Distance(transform.position, pathPoints[currentPathIndex].position) < 0.1f)
        {
            currentPathIndex = (currentPathIndex + 1) % pathPoints.Length;
        }
    }
}
