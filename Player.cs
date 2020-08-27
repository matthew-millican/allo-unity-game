 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (ShapeController))]
public class Player : MonoBehaviour
{
    Controller2D controller;

    bool isDashing = false;




    ShapeController shapeController;


    public float doubleTapTime;
    KeyCode lastKeyCode;


    float numberOfJumps;


    float currentOrder;
    float moveSpeed = 22;


    float dashSpeed = 800;


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

        if (currentOrder == 4)
        {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.3f;
            }
            lastKeyCode = KeyCode.D;
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
        velocity.x = Mathf.SmoothDamp(velocity.x, velocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y = 0f;
        controller.Move(velocity * Time.deltaTime);
        yield return new WaitForSeconds(0f);
        isDashing = false;
    }



    void getBehaviour(float currentOrder)
    {
        if (currentOrder == 1f)
        {
            jumpHeight = 7f;
            timeToJumpApex = 0.3f;
        }
        else if (currentOrder == 2f)
        {
            jumpHeight = 9f;
            timeToJumpApex = 0.302f;
        }


        gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }
}
