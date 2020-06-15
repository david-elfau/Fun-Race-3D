using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 1f;
    public float inertia = 0.5f;

    Vector3 desiredMovingDirection;
    Vector3 movingDirection;

    public bool IsScreenTouched = false;

    public Animator animator;
    private Rigidbody rigidBody;

    private GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();

        movingDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            IsScreenTouched = true;
        } else
        {
            IsScreenTouched = false;
        }
        
        animator.SetFloat("Speed_f", movingDirection.magnitude / playerSpeed);

    }
    

    private void FixedUpdate()
    {
        if (IsScreenTouched)
        {
            desiredMovingDirection = Vector3.right * playerSpeed;
        }
        else
        {
            desiredMovingDirection = Vector3.zero;
        }

        movingDirection = desiredMovingDirection * inertia + movingDirection * (1 - inertia);

        MovePlayer(movingDirection);
    }

    void MovePlayer(Vector3 direction)
    {
        rigidBody.MovePosition(transform.position + direction * Time.fixedDeltaTime);
        this.transform.LookAt(transform.position + movingDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
        } else if (other.CompareTag("Finish"))
        {
            FinishRace();

        } else if (other.CompareTag("Respawn"))
        {            
            SaveCheckpoint(other.gameObject);
        }
    }

    private void SaveCheckpoint(GameObject gameObject)
    {
        lastCheckpoint = gameObject;
        Debug.Log("CROSS CHECKPOINT");
    }

    private void FinishRace()
    {
        Debug.Log("FINISH");
    }

    private void Die()
    {
        Debug.Log("DIE");
        ResetToCheckPoint();
    }

    private void ResetToCheckPoint()
    {
        this.transform.position = lastCheckpoint.transform.position; 
        Debug.Log("RESET CHECKPOINT");
    }
}
