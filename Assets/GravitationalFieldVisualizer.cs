using UnityEngine;
using System.Collections.Generic;

public class GravitationalFieldVisualizer : MonoBehaviour
{
    public List<Rigidbody> bodies; // The gravitational sources
    public float gridSpacing = 1f; // Spacing between field points
    public int gridResolution = 10; // Number of points along each axis
    public Material lineMaterial; // Material for the line (assign this in Inspector)

    void Start()
    {
        Vector3 center = Vector3.zero;

        // Create grid points
        for (int x = -gridResolution; x <= gridResolution; x++)
        {
            for (int y = -gridResolution; y <= gridResolution; y++)
            {
                for (int z = -gridResolution; z <= gridResolution; z++)
                {
                    Vector3 point = center + new Vector3(x, y, z) * gridSpacing;
                    Vector3 fieldDirection = CalculateGravitationalField(point);

                    if (fieldDirection != Vector3.zero)
                    {
                        // Create the arrow using LineRenderer
                        CreateArrow(point, fieldDirection);
                    }
                }
            }
        }
    }

    Vector3 CalculateGravitationalField(Vector3 point)
    {
        Vector3 field = Vector3.zero;

        foreach (Rigidbody body in bodies)
        {
            Vector3 direction = body.position - point;
            float distance = direction.magnitude;

            if (distance > 0.01f) // Avoid division by zero
            {
                field += (body.mass / (distance * distance)) * direction.normalized;
            }
        }

        return field;
    }

    // Function to create the arrow using LineRenderer
    void CreateArrow(Vector3 position, Vector3 direction)
    {
        GameObject arrowObject = new GameObject("Arrow");
        LineRenderer lineRenderer = arrowObject.AddComponent<LineRenderer>();

        lineRenderer.material = lineMaterial; // Set the material
        lineRenderer.startWidth = 0.1f; // Arrow line width
        lineRenderer.endWidth = 0.1f;   // Arrow line width
        lineRenderer.positionCount = 2; // Arrow has two points (start and end)

        // Set start and end positions of the line
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + direction * 2f); // Adjust arrow length

        // Optional: Add an arrowhead using a small cone (LineRenderer or a 3D model)
    }
}
