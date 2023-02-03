using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    int extraLanes;

    int lane = 0;
    int desLane = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new  Vector3(transform.position.x, transform.position.y, transform.position.z); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
