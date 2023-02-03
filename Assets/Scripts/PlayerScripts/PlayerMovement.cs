using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    BoxCollider boxCollider; 

    [SerializeField]
    float speed;

    [SerializeField]
    int extraLanes;

    float lane = 0;
    float desLane = 0;

    [SerializeField]
    float spaceBetween;

    [SerializeField]
    float switchingSpeed;

    [SerializeField]
    float jumpSpeed;

    [SerializeField]
    float jumpHeight; 

    [SerializeField]
    float fallSpeed;

    bool falling;

   // float jumpTimer = 10;

    float height = 0;
    float heightVel = 0;
    float heightAcc = 0; 

    bool grounded;
    bool jumping;

    [SerializeField]
    float crouchAmount; 
    bool crouching; 




    // Start is called before the first frame update
    void Start()
    {
        fallSpeed /= 10;

        Crouching();
    }

    private void FixedUpdate()
    {
        lane = Mathf.Lerp(lane, desLane, switchingSpeed);
        gameObject.transform.position = new  Vector3((lane * spaceBetween), height, transform.position.z);

        if (jumping) Jumping(jumpHeight);

    }

    // Update is called once per frame
    void Update()
    {
        KeyControlls();
        if (desLane > extraLanes) desLane = extraLanes;
        if (desLane < -extraLanes) desLane = -extraLanes;

        Crouching();

    }

    void Crouching()
    {

        if (crouching)
        {
            boxCollider.size = new Vector3(boxCollider.size.x, crouchAmount, boxCollider.size.z);
            boxCollider.center = new Vector3(0, (crouchAmount - 1) / 2, 0);
        }
        else
        {
            boxCollider.size = new Vector3(boxCollider.size.x, 1, boxCollider.size.z);
            boxCollider.center = new Vector3(0, 0, 0);
        }



    }

    void  Jumping(float Jumpheight)
    {
        if (height > Jumpheight * 0.8f) falling = true;

            height = Mathf.Lerp(height, Jumpheight, jumpSpeed);

        if (falling)
        {
            heightAcc += fallSpeed;
            heightVel += heightAcc; 
            height -= heightVel; 
        }


        if (height < 0)
        {
            jumping = false;
            falling = false;
            
            height = 0;
            heightAcc = 0;
            heightVel = 0;
        }

    }

    void KeyControlls()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) desLane--;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) desLane++;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) && !jumping) jumping = true;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftControl)) crouching = true;
        else crouching = false;


    }
}
