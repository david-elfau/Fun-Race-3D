using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerController : MonoBehaviour
{

    public Camera originalCamera;
    public Camera newCamera;

    public GameObject player;

    public float speed = 1f;

    bool moveCamera = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(moveCamera)
        {
            Vector3 movingDirection = newCamera.transform.position - originalCamera.transform.position;
            if(movingDirection.magnitude>0.1)
            {
                originalCamera.transform.position += movingDirection.normalized * speed * Time.deltaTime;
                originalCamera.transform.LookAt(player.transform);
            }
            else
            {
                moveCamera = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveCamera = true;
        }
    }
}
