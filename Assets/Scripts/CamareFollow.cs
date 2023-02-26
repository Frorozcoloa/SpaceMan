using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamareFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset = new Vector3(0.2f,0.2f,-10f);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        target = GameObject.Find("Player").transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }
    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool is_somooth)
    {
        Vector3 destination = new Vector3(
            target.position.x - offset.x,
            offset.y,
            offset.z
            );
        if ( is_somooth )
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                destination,
                ref velocity,
                dampingTime
                );
        }
        else
        {
            transform.position = destination;
        }
    }
}
