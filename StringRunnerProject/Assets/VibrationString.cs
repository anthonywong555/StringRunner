using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class VibrationString : MonoBehaviour
{
    // String properties
    public float width; // Width of the the string
    public float baseHeight;    // Height of the string
    public float maxDelta = 2.0f;   // How far a node can stretch

    public float springConstant = 0.02f;    // The constant used to calulate the adjusting force of the springe
    public float damping = 0.04f;   // Damping on the string
    public float spread = 0.05f;    // Spread of disturbences on the string

    // Node properties
    Vector2[] nodePositions;
    float[] nodeVelocities;
    float[] nodeAccelerations;

    // Components
    LineRenderer stringRenderer;
    EdgeCollider2D edgeCollider;

    private void Start()
    {
        CreateWater();
    }

    public void CreateWater()
    {
        Vector2 startPosition = transform.position;
        baseHeight = startPosition.y;

        // Lines between nodes
        int lineCount = Mathf.RoundToInt(width) * 5;
        // Nodes connecting lines
        int nodeCount = lineCount + 1;

        nodePositions = new Vector2[nodeCount];
        nodeVelocities = new float[nodeCount];
        nodeAccelerations = new float[nodeCount];

        stringRenderer = GetComponent<LineRenderer>();
        stringRenderer.useWorldSpace = false;
        stringRenderer.numPositions = nodeCount;
        stringRenderer.startWidth = .1f;
        stringRenderer.endWidth = .1f;

        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.points = new Vector2[nodeCount + 1];

        for (int i = 0; i < nodeCount; ++i)
        {
            nodePositions[i] = Vector2.right * (width * i / lineCount);
            nodeVelocities[i] = 0;
            nodeAccelerations[i] = 0;
            stringRenderer.SetPosition(i, nodePositions[i]);
        }

        edgeCollider.points = nodePositions;
    }

    public int GetClosestNode(Vector2 point)
    {
        int nodeNearest = 0;
        float distance, distanceToNearestNode = 10000;
        for (int i = 0; i < nodePositions.Length; ++i)
        {
            distance = Vector2.Distance(nodePositions[i] + (Vector2)transform.position, point);

            if (distance < distanceToNearestNode)
            {
                nodeNearest = i;
                distanceToNearestNode = distance;
            }
        }

        return nodeNearest;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nodeVelocities[nodeVelocities.Length / 2] -= 40.0f * Time.fixedDeltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            nodeVelocities[GetClosestNode(point)] -= 40.0f * Time.fixedDeltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Update nodes
        float force;    // Used to calulate how fast it moves
        float limit;
        float halfLength = (nodePositions.Length - 1) / 2.0f;
        for (var i = 0; i < nodePositions.Length; ++i)
        {
            limit = (halfLength - Mathf.Abs(halfLength - i)) / halfLength * maxDelta;

            force = springConstant * (nodePositions[i].y) + nodeVelocities[i] * damping;
            nodeAccelerations[i] = -force;

            nodePositions[i].y += nodeVelocities[i];

            nodePositions[i].y = Mathf.Min(limit, Mathf.Max(-limit, nodePositions[i].y));

            nodeVelocities[i] += nodeAccelerations[i];

            stringRenderer.SetPosition(i, nodePositions[i]);
        }
        edgeCollider.points = nodePositions;

        // Propagate Waves
        float[] leftDeltas = new float[nodePositions.Length];
        float[] rightDeltas = new float[nodePositions.Length];
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < nodePositions.Length; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = spread * (nodePositions[i].y - nodePositions[i - 1].y);
                    nodeVelocities[i - 1] += leftDeltas[i];
                }
                if (i < nodePositions.Length - 1)
                {
                    rightDeltas[i] = spread * (nodePositions[i].y - nodePositions[i + 1].y);
                    nodeVelocities[i + 1] += rightDeltas[i];
                }
            }

            for (int i = 0; i < nodePositions.Length; i++)
            {
                if (i > 0)
                {
                    nodePositions[i - 1].y += leftDeltas[i];
                }
                if (i < nodePositions.Length - 1)
                {
                    nodePositions[i + 1].y += rightDeltas[i];
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 point = collision.contacts[0].point;

        int nodeNearest = GetClosestNode(point);

        if(nodeVelocities[nodeNearest] > 0)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += Vector2.up * nodeVelocities[nodeNearest] * 10;
        }

        Debug.Log("Collision");
    }
}