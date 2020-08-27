 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (ShapeController))]
public class Player : MonoBehaviour
{
    Controller2D controller;

    bool isDashing;




    ShapeController shapeController;


    public float doubleTapTime;
    KeyCode lastKeyCode;


    float numberOfJumps;


    float currentOrder;
    public float moveSpeed = 22;


    public float dashSpeed = 1000;


    public float jumpHeight;
    public float timeToJumpApex;

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;



    Vector3 velocity;

    float gravity;

    float jumpVelocity;

    float velocityXSmoothing;
    void Start()
    {
        shapeController = GetComponent<ShapeController>();
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

    }

    void Update()
    {

        currentOrder = shapeController.getCurrentOrder();
        getBehaviour(currentOrder);


        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if ((Input.GetKey(KeyCode.W) && controller.collisions.below) || (Input.GetKey(KeyCode.UpArrow) && controller.collisions.below))
        {
            if (currentOrder != 3)
            {
                velocity.y = jumpVelocity;
            }
        }



        float targetVelocityX = input.x * moveSpeed;
        currentOrder = shapeController.getCurrentOrder();
        if (currentOrder == 4)
           {
           if (Input.GetKeyDown(KeyCode.D))
           {
               if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
               {
                   isDashing = true;
                   StartCoroutine(Dash(1f));
               }
               else
               {
                   doubleTapTime = Time.time + 0.3f;
               }
               lastKeyCode = KeyCode.D;
           }
           else if (Input.GetKeyDown(KeyCode.A))
           {
               if ((doubleTapTime > Time.time && lastKeyCode == KeyCode.A))
               {
                   isDashing = true;
                   StartCoroutine(Dash(-1f));
               }
               else{
                   doubleTapTime = Time.time + 0.3f;
               }
               lastKeyCode = KeyCode.A;
           }
        }
        if (!isDashing)
        {
           velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
           velocity.y += gravity * Time.deltaTime;
           controller.Move(velocity * Time.deltaTime);
        }
    }



    IEnumerator Dash (float direction)
    {

        isDashing = true;
        float velocityX = dashSpeed * direction;
        Debug.Log(velocityX);
        velocity.x = velocityX;
        velocity.y = 0f;
        controller.Move(velocity * Time.deltaTime);
        yield return new WaitForSeconds(0.00002f);
        isDashing = false;
    }



    void getBehaviour(float currentOrder)
    {
        if (currentOrder == 1f)
        {
            moveSpeed = 25;
            jumpHeight = 7f;
            timeToJumpApex = 0.3f;
        }
        else if (currentOrder == 2f)
        {
            moveSpeed = 26;
            jumpHeight = 9f;
            timeToJumpApex = 0.302f;
        }
        else if (currentOrder == 3f)
        {
            moveSpeed = 24;
            jumpHeight = 6f;
            timeToJumpApex = 0.3f;
        }
        else if (currentOrder == 4f)
        {
            moveSpeed = 27;
            jumpHeight = 8f;
            timeToJumpApex = 0.3f;
        }
        else if (currentOrder == 5f)
        {
            moveSpeed = 26f;
            jumpHeight = 9f;
            timeToJumpApex = 0.3f;
        }


        gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }
}
