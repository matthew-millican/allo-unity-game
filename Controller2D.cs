using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ShapeController))]
public class Controller2D : RaycastController
{

    public CollisionInfo collisions;

      public PlatformController platform;


    Vector2 playerInput;

    float maxAngle = 55;

    float maxDescendAngle = 50;

    ShapeController shapeController;



    

    public override void Start()
    {

        base.Start ();
        collisions.faceDir = 1;

        shapeController = gameObject.GetComponent<ShapeController>();
    }


    public void Move(Vector3 velocity, bool standingOnPlatform)
    {
        Move (velocity, Vector2.zero, standingOnPlatform);
    }



    public void Move(Vector3 velocity, Vector2 input, bool standingOnPlatform = false)
    {
        collisions.Reset();
         UpdateRaycastOrigins();

         collisions.velocityOld = velocity;


         playerInput = input;

         if (velocity.x != 0)
         {
             collisions.faceDir = (int) Mathf.Sign(velocity.x);
         }

         if (velocity.y < 0)
         {
             DescendSlope(ref velocity);
         }

        HorizontalCollisions(ref velocity);


         if (velocity.y != 0)
         {
             VerticalCollisions(ref velocity);
         }





        transform.Translate(velocity);

        if (standingOnPlatform)
        {
            collisions.below = true;
        }
    }


    void VerticalCollisions(ref Vector3 velocity)
    {

        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
                   Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {



                if (hit.collider.tag == "WhitePlatform" || hit.collider.tag == "MovingPlatform")
                {
                    if (directionY == 1 || hit.distance == 0)
                    {
                        continue;
                    }

                    if (hit.collider == collisions.fallingThroughPlatform)
                    {
                        continue;
                    }
                    if (playerInput.y == -1)
                    {
                        if (hit.collider.tag == "MovingPlatform")
                        {
                            Debug.Log(platform.platformDirectionY);
                            if (platform.platformDirectionY == -1)
                            {
                                collisions.fallingThroughPlatform = hit.collider;
                                continue;
                            }
                        }
                        else
                        {
                            collisions.fallingThroughPlatform = hit.collider;
                            continue;
                        }
                    }
                }
                collisions.fallingThroughPlatform = new Collider2D();
                 bool alive = getColor(hit.transform.gameObject);

                 velocity.y = (hit.distance - skinWidth) * directionY;
                 rayLength = hit.distance;


                 if (collisions.climbingSlope)
                 {
                     velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                 }

                   collisions.below = directionY == -1;
                 collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);


            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }


    bool getColor(GameObject gameObject)
    {

        float currentOrder = shapeController.getCurrentOrder();
        Color currentColor = gameObject.GetComponent<Renderer>().material.color;
        if (currentColor == new Color(1, 1, 1, 1))
        {
            //No death
            return true;
        }
        else
        {
            Color shapeColor = new Color(0, 0, 0, 0);

            if (currentOrder == 1)
            {
                shapeColor = new Color(1, 0, 0, 1);
            }
            else if (currentOrder == 2)
            {
                shapeColor = new Color(0f, 0f, 1, 1);
            }
            else if (currentOrder == 3)
            {
                shapeColor = new Color(1, 1, 1, 1);
            }
            else if (currentOrder == 4)
            {
                if (currentColor != new Color(0, 1, 0, 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (currentOrder == 5)
            {
                if (currentColor != new Color(1, 1, 0, 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }


            if (shapeColor != currentColor)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    void HorizontalCollisions(ref Vector3 velocity)
    {

        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;


        if (Mathf.Abs(velocity.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {

                bool alive = getColor(hit.transform.gameObject);

                if (hit.distance == 0)
                {
                    continue;
                }

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngle <= maxAngle)
                {

                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityOld;
                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance-skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                     rayLength = hit.distance;


                     if (collisions.climbingSlope)
                     {
                         velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                     }
                     collisions.left = directionX == -1;
                     collisions.right = directionX == 1;
                }
            }
        }
    }


    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
         float moveDistance = Mathf.Abs(velocity.x);
         float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
         if (velocity.y <= climbVelocityY)
         {
             velocity.y = climbVelocityY;
         }
         velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
         collisions.below = true;
         collisions.climbingSlope = true;
         collisions.slopeAngle = slopeAngle;
    }

    void DescendSlope(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomRight:raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;
 

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }


    public struct CollisionInfo {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;

        public bool descendingSlope;

        public float slopeAngle, slopeAngleOld;

        public Vector3 velocityOld;
        
        public int faceDir;


        public Collider2D fallingThroughPlatform;
        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
            descendingSlope = false;
        }


    }
}
