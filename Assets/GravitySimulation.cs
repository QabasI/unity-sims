using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    public List<Rigidbody> bodies; // List of all bodies in the simulation
    public float gravitationalConstant = 1f;

    void FixedUpdate()
    {
    for (int i = 0; i < bodies.Count; i++)
    {
        for (int j = 0; j < bodies.Count; j++)
        {
            if (i != j)
            {
                Rigidbody bodyA = bodies[i];
                Rigidbody bodyB = bodies[j];

                // Validate bodies
                if (bodyA == null || bodyB == null || bodyA.mass <= 0 || bodyB.mass <= 0)
                    continue;

                Vector3 direction = bodyB.position - bodyA.position;
                float distance = direction.magnitude;

                // Skip calculation if distance is too small
                if (distance < 0.01f)
                    continue;

                // Calculate force
                Vector3 force = (gravitationalConstant * bodyA.mass * bodyB.mass / (distance * distance)) * direction.normalized;

                // Apply force
                bodyA.AddForce(force);
            }
        }
    }
    }

}
