using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


public AudioSource m_AudioSource;
Animator m_Animator;
public AudioSource m_JetPack;
Rigidbody2D rigidbody;
BoxCollider2D boxCollider2D;

public LayerMask collisionLayer;

private bool isFlying = false;
private float timeKeyHeld = 0.0f;

public ParticleSystem jetpackAir;

public ParticleSystem blownDust;

private bool isFacingRight;

private float horizontalInput;
private float verticalInput;



public float MaxGravityDistance;

private bool aboveGravity = false;

private bool crouching = false;

public float maxSpeed; 

public Vector3 scaleChange;

private bool scaleChanged = false;

public float scaleTimer;

private float localTimer;

private bool isTouchingWall;
private bool isTouchingLedge;

public Transform wallCheckPosition;
public Transform ledgeCheckPosition;

public float wallCheckDistance;

private bool canClimbLedge = false;

private bool ledgeDetected;

private Vector2 ledgePositionBottom;

private Vector2 ledgePos1;
private Vector2 ledgePos2;


public float yOffset1;
public float yOffset2;
public float xOffset1;
public float xOffset2;

private bool canMove = true;








    void Start()
    {
         m_Animator = GetComponent<Animator>();
         rigidbody = GetComponent<Rigidbody2D>();
         boxCollider2D = GetComponent<BoxCollider2D>();

    }


    void Update()
    {
        canMove = true;
        crouching = false;
        isFacingRight = IsFacingRight();
        CheckLedge();
        CheckClimbLedge();
        wallCheckDistance = boxCollider2D.bounds.extents.x + 1f;
        CheckGravity();



         if (!aboveGravity && canMove)
        {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        float speed = 1.5f;

        bool grounded = IsGrounded();



        if ((Input.GetKey("w") && verticalInput > 0))
        {
            isFlying = true;
        }
        else if (Input.GetKey("up"))
        {
            isFlying = true;
        }
        else if (!grounded)
        {
            m_Animator.SetBool("LandingFinished", false);
            isFlying = true;
            jetpackAir.Stop();
            blownDust.Stop();
        }
        else if (isFlying && grounded)
        {
            Landing();
            crouching = false;
        }
        else if ((Input.GetKey("c") && grounded) || (Input.GetKey("down") && grounded) || (Input.GetKey("s") && grounded))
        {
            jetpackAir.Stop();
            blownDust.Stop();
            isFlying = false;
            crouching = true;
        }
        else{
            jetpackAir.Stop();
            blownDust.Stop();
            isFlying = false;
        }
        }
    }


    private void FinishCrouch()
    {
        m_Animator.SetBool("OutOfCrouch", true);
    }

    private void playParticles()
    {
        {
            jetpackAir.Play ();
            m_JetPack.Play ();
            if (IsGrounded())
            {
                blownDust.Play ();
            }
    }
    }


    private void CheckLedge()
    {
        Vector2 direction;

        if (!ledgeDetected)
        {
        if (IsFacingRight())
        {
            direction = Vector2.right;
        }
        else {
            direction = Vector2.left;
        }
        isTouchingWall = Physics2D.Raycast(wallCheckPosition.position, direction, wallCheckDistance, collisionLayer);

        Debug.DrawRay(wallCheckPosition.position, direction * wallCheckDistance, Color.red);

        isTouchingLedge = Physics2D.Raycast(ledgeCheckPosition.position, direction, wallCheckDistance, collisionLayer);
        Debug.DrawRay(ledgeCheckPosition.position, direction * wallCheckDistance, Color.red);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePositionBottom = wallCheckPosition.position;
            m_Animator.SetInteger("State", 6);
            verticalInput = 0;
            horizontalInput = 0;
            canMove = false;
        }
        }
    }


    private void CheckClimbLedge()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;


            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePositionBottom.x + wallCheckDistance) - xOffset1, Mathf.Floor(ledgePositionBottom.y) + yOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePositionBottom.x + wallCheckDistance) + xOffset2, Mathf.Floor(ledgePositionBottom.y) + yOffset2);
            }
            else {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePositionBottom.x - wallCheckDistance) + xOffset1, Mathf.Floor(ledgePositionBottom.y) + yOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePositionBottom.x - wallCheckDistance) - xOffset2, Mathf.Floor(ledgePositionBottom.y) + yOffset2);
            }
        }
        if (canClimbLedge)
        {
            transform.position = ledgePos1;
            canMove = false;
            horizontalInput = 0;
            verticalInput = 0;
            isFlying = false;
        }
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        ledgeDetected = false;
        horizontalInput = 0;
        verticalInput = 0;
        m_Animator.SetInteger("State", 0);
        rigidbody.velocity = new Vector2(0f, 0f);
    }



    private void CheckGravity()
    {
        Vector2 position = transform.position;
        if (position.y > MaxGravityDistance)
        {
            rigidbody.gravityScale = 0f;
            aboveGravity = true;
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
        else if (position.y > (MaxGravityDistance - 10))
        {
            rigidbody.AddForce(new Vector2(0f, -5f));
        }
    }



    private void Landing()
    {
        getFallSpeed();
            m_Animator.SetInteger("State", 4);
            boxCollider2D.size = new Vector2(2.0f, 5.03f);
            isFlying = false;
            jetpackAir.Stop();
            blownDust.Stop();
            if (transform.localScale == new Vector3(1f, 0.9f, 1f))
            {
            transform.localScale -= scaleChange;
            Debug.Log(transform.localScale);
            localTimer = scaleTimer;
            scaleChanged = true;
            }
    }


    private void getFallSpeed()
    {
        Vector3 velocity = rigidbody.velocity;
        if (velocity.magnitude >= maxSpeed)
        {
            Debug.Log("DEAD");
        }
    }



    private void GroundedMovement()
    {

        if (scaleChanged)
        {
            localTimer -= Time.deltaTime;
            if (localTimer <= 0)
            {
            transform.localScale += scaleChange;
            scaleChanged = false;
            }
        }
        crouching = false;
        float speed = 1.5f;
        if (horizontalInput != 0)
        {
         boxCollider2D.size = new Vector2(3.5f, 5.03f);
        m_Animator.SetBool("LandingFinished", true);
        timeKeyHeld = timeKeyHeld + Time.deltaTime;
        if (timeKeyHeld >= 5)
        {
            speed = 2f;
        }
        else if (timeKeyHeld >= 8)
        {
            speed = 3f;
        }
        else if (timeKeyHeld >= 10)
        {
            speed = 4f;
        }
        m_Animator.SetInteger("State", 1);
        if (!m_AudioSource.isPlaying)
         {
             m_JetPack.Stop ();
            m_AudioSource.Play ();
        }

        m_Animator.SetFloat("WalkMultiplier", speed);
        flipCharacter(horizontalInput);

        Vector2 position = transform.position;
        float multiplier = 14.0f;

        switch(speed)
        {
            case 1.5f :
            multiplier = 14.0f;
            break;
            case 2.0f :
            multiplier = 15.0f;
            break;
            case 3.0f :
            multiplier = 16.0f;
            break;
            case 4.0f :
            multiplier = 17.0f;
            break;
        }
        position.x = position.x + multiplier * horizontalInput * Time.deltaTime;

        transform.position = position;
        }
        else
        {
        if (m_Animator.GetBool("LandingFinished"))
            {
                 m_Animator.SetInteger("State", 0);
            }
        boxCollider2D.size = new Vector2(2.0f, 5.03f);
            m_AudioSource.Stop ();
            m_JetPack.Stop ();
            timeKeyHeld = 0.0f;
        }
     }

     private void DoCrouching()
     {
         m_Animator.SetInteger("State", 5);
     }

    void FixedUpdate()
    {

        Debug.Log(rigidbody.velocity.magnitude);

    if (!aboveGravity && canMove)
    {
        if (!isFlying)
        {
            if (crouching)
            {
                DoCrouching();
                m_AudioSource.Stop();
            }
            else{
            GroundedMovement();
            }
        }


        if (isFlying)
        {
            if (crouching)
            {
                DoCrouching();
                m_AudioSource.Stop();
                jetpackAir.Stop();
                m_JetPack.Stop();
            }
            else{
            FlyingMovement();
            }
        }
    }
    }

    private void FlyingMovement()
    {
        m_AudioSource.Stop ();
        flipCharacter(horizontalInput);
        if (IsGrounded())
        {
        playParticles();
        crouching = false;
           boxCollider2D.size = new Vector2(2.0f, 5.03f);
            m_Animator.SetInteger("State", 2);
            if (verticalInput < 0) {
               verticalInput = 0;
            }
            rigidbody.AddForce(new Vector2(0, 200f));   
        }
        else {    
            m_Animator.SetInteger("State", 3);
        }
            Vector2 position = transform.position;
            position.x = position.x + (15.0f * horizontalInput * Time.deltaTime);
            transform.position = position;
            
    }









    bool IsGrounded() {
        float extraHeightText = 0.2f;
        RaycastHit2D raycastHit1 = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeightText, collisionLayer);
        RaycastHit2D raycastHit2 = Physics2D.Raycast(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0f, 0f), Vector2.down, boxCollider2D.bounds.extents.y + extraHeightText, collisionLayer);
        RaycastHit2D raycastHit3 = Physics2D.Raycast(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0f, 0f), Vector2.down, boxCollider2D.bounds.extents.y + extraHeightText, collisionLayer);
        if (raycastHit1 || raycastHit2 || raycastHit3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsFacingRight() {
        if (transform.eulerAngles == new Vector3(0, 0, 0))
        {
            return true;
        }
        else {
            return false;
        }
       return true;
    }



    void flipCharacter(float horizontal)
    {
        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
