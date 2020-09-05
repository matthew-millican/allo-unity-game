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

    public float minJumpHeight = 1f;
    public float timeToJumpApex;

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    public float wallSpeedSlideMax = 3;

    public float wallStickTime = 0.25f;

    float timeToWallUnstick;


    public Vector2 wallJumpClimb;

    public Vector2 wallJumpOff;
    public Vector2 wallLeap;



    bool hasDoubleJumped;

    bool isFlying = false;



    Vector3 velocity;

    float gravity;

    float jumpVelocity;


    float minJumpVelocity;

    float velocityXSmoothing;
    void Start()
    {

        shapeController = GetComponent<ShapeController>();
        controller = GetComponent<Controller2D>();
        currentOrder = shapeController.getCurrentOrder();

        gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight);

    }

    void Update()
    {
         Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
         int wallDirX = (controller.collisions.left) ? -1 : 1;

        bool wallSliding = false;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);


        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0 && (currentOrder == 1 || currentOrder == 5))
        {
            wallSliding = true;


            if (velocity.y < -wallSpeedSlideMax)
            {
                velocity.y = -wallSpeedSlideMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;
                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else{
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }

        isFlying = false;

        if (controller.collisions.below)
        {
            hasDoubleJumped = true;
        }

        currentOrder = shapeController.getCurrentOrder();
        getBehaviour(currentOrder);


        if ((Input.GetKey(KeyCode.W) && currentOrder == 3))
        {
            isFlying = true;
            velocity.y = jumpVelocity;
        }

        if (!isFlying)
        {


        if (((Input.GetKeyDown(KeyCode.W) && (currentOrder == 2 || currentOrder == 5) && !hasDoubleJumped)) || (Input.GetKeyDown(KeyCode.UpArrow) && (currentOrder == 2 || currentOrder == 5) && !hasDoubleJumped))
        {
            hasDoubleJumped = true;
            velocity.y = jumpVelocity;
            isFlying = false;
        }

        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow)))
        {
            if (currentOrder != 3)
            {
                if (wallSliding && (currentOrder == 1 || currentOrder == 5))
                {
                    if (wallDirX == input.x)
                    {
                        velocity.x = -wallDirX * wallJumpClimb.x;
                        velocity.y = wallJumpClimb.y;
                    }
                    else if (input.x == 0)
                    {
                        velocity.x = -wallDirX * wallJumpOff.x;
                        velocity.y = wallJumpOff.y;
                    }
                    else 
                    {
                        velocity.x = -wallDirX * wallLeap.x;
                        velocity.y = wallLeap.y;
                    }
                }
                if (controller.collisions.below && velocity.y == 0)
                {
                velocity.y = jumpVelocity;
                hasDoubleJumped = false;
                isFlying = false;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.UpArrow)))
        {

            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }
        }



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
           velocity.y += gravity * Time.deltaTime;
           controller.Move(velocity * Time.deltaTime, input);

        }


        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }



    IEnumerator Dash (float direction)
    {

        isDashing = true;
        float velocityX = dashSpeed * direction;
        Debug.Log(velocityX);
        velocity.x = velocityX;
        velocity.y = 0f;
        controller.Move(velocity * Time.deltaTime, Vector2.zero);
        yield return new WaitForSeconds(0.00002f);
        isDashing = false;
    }



    void getBehaviour(float currentOrder)
    {
        if (currentOrder == 1f)
        {
            moveSpeed = 25;
            jumpHeight = 17f;
            timeToJumpApex = 0.5f;
        }
        else if (currentOrder == 2f)
        {
            moveSpeed = 28f;
            jumpHeight = 15f;
            timeToJumpApex = 0.45f;
        }
        else if (currentOrder == 3f)
        {
            moveSpeed = 24;
            jumpHeight = 4f;
            timeToJumpApex = 0.25f;
        }
        else if (currentOrder == 4f)
        {
            moveSpeed = 35f;
            jumpHeight = 10f;
            timeToJumpApex = 0.4f;
        }
        else if (currentOrder == 5f)
        {
            moveSpeed = 28f;
            jumpHeight = 17f;
            timeToJumpApex = 0.5f;
        }


        gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }
}
