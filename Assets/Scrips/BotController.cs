using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : PlayerController
{
    enum BotStatus { RunOutside, RunOnTrigger, Waiting };


    BotStatus status = BotStatus.RunOutside;
    bool insideCheckpoint = false;
    public float MaxWaitingTime = 2; 
    private float timeWaiting = 1f;
    private Vector3 OFFSET_BOT_POSITION = new Vector3(0, 0, 1f);

    void FixedUpdate()
    {
        if (GameManager.Instance.status == GameManager.RaceStatus.Running)
        {
            CheckWaitingTime();

            if (status == BotStatus.Waiting)
            {
                Debug.Log("WAITING");
                desiredMovingDirection = Vector3.zero;
            }
            else
            {
                Debug.Log("RUNNING");
                desiredMovingDirection = Vector3.right * playerSpeed;
            }

            movingDirection = desiredMovingDirection * inertia + movingDirection * (1 - inertia);
            MovePlayer(movingDirection);
        }
    }

    void Update()
    {
        animator.SetFloat("Speed_f", movingDirection.magnitude / playerSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            status = BotStatus.RunOutside;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (other.CompareTag("Finish"))
        {
            FinishRace();

        }
        else if (other.CompareTag("Respawn"))
        {
            StopRunning();
            SaveCheckpoint(other.gameObject);

        }
    }

    private void Die()
    {
        ResetToCheckPoint();
    }
    private void FinishRace()
    {
        GameManager.Instance.BotArrived();
    }

    void ResetToCheckPoint()
    {
        this.transform.position = lastCheckpoint.transform.position + OFFSET_BOT_POSITION;
        Debug.Log(" Bot RESET CHECKPOINT " + this.transform.position);
    }


    void StopRunning()
    {
        if (status == BotStatus.RunOutside)
        {
            timeWaiting = UnityEngine.Random.Range(0, MaxWaitingTime);
            status = BotStatus.Waiting;
        }
    }

    private void CheckWaitingTime()
    {
        if(status == BotStatus.Waiting)
        {
            timeWaiting -= Time.fixedDeltaTime;
            if (timeWaiting <= 0)
            {
                status = BotStatus.RunOnTrigger;
            }
        }
    }
}
