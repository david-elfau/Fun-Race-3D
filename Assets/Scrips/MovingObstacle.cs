using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obstacle;
    public GameObject[] path;
    private int nextPosition = 0;

    public float obstacleSpeed = 5f;
    public float inertia = 0.5f;

    Vector3 desiredMovingDirection;
    Vector3 movingDirection;


    void Start()
    {

        movingDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector3 pathRemaining = path[nextPosition].transform.position - obstacle.transform.position;
        if(pathRemaining.sqrMagnitude < 0.1)
        {
            nextPosition++;
            if (nextPosition >= path.Length)
                nextPosition = 0;

            movingDirection = Vector3.zero;

            pathRemaining = path[nextPosition].transform.position - obstacle.transform.position;
        }

        desiredMovingDirection = pathRemaining;
        desiredMovingDirection = desiredMovingDirection.normalized * obstacleSpeed;

        movingDirection = desiredMovingDirection * inertia + movingDirection * (1 - inertia);

        MoveObstacle(movingDirection);
    }

    void MoveObstacle(Vector3 direction)
    {
        obstacle.transform.position += direction * Time.fixedDeltaTime;
    }
}
