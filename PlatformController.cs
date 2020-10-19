using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlatformController : RaycastController
{


    public Vector3[] localWaypoints;

    public float speed;

    public bool cyclic;

    public float waitTime;
    [Range(0, 2)]
    public float easing;

    int fromWaypointIndex;
    float percentage;

    public float platformDirectionY;

    float nextMoveTime;
    Vector3[] globalWaypoints;

    public LayerMask passengerMask;


    List<PassengerMovement> passengerMovement;

    Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D> ();
    public override void Start()
    {
        base.Start ();





        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
        
    }

    void Update()
    {

        UpdateRaycastOrigins();



        Vector3 velocity = CalculatePlatformMovement();
        platformDirectionY = Mathf.Sign(velocity.y);

        CalculatePassengerMovement(velocity);

        MovePassengers(true);




        transform.Translate(velocity);


        MovePassengers(false);
        
    }


    float Ease(float x)
    {
        float a = easing + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }


    Vector3 CalculatePlatformMovement()
    {

        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }
        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentage += Time.deltaTime * speed/distanceBetweenWaypoints;
        percentage = Mathf.Clamp01 (percentage);

        float easedPercentage = Ease (percentage);
        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentage);

        if (percentage >= 1)
        {
            percentage = 0;
            fromWaypointIndex ++;

            if (!cyclic)
            {
            if (fromWaypointIndex >= globalWaypoints.Length - 1)
            {
                fromWaypointIndex = 0;
                System.Array.Reverse(globalWaypoints);
            }
            }

            nextMoveTime = Time.time + waitTime;
        }
        return newPos - transform.position;
    }



    void MovePassengers(bool beforeMovePlatform)
    {

        foreach (PassengerMovement passenger in passengerMovement)
        {
            if (!passengerDictionary.ContainsKey(passenger.transform))
            {
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }
            if (passenger.moveBeforePlatform == beforeMovePlatform)
            {
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }

    }


    void CalculatePassengerMovement(Vector3 velocity)
    {





        HashSet<Transform> movedPassengers = new HashSet<Transform>();

        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);

        float directionY = Mathf.Sign(velocity.y);

        //Vertical platform

        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);
                   Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);


                   if (hit && hit.distance != 0)
                   {

                       if (!movedPassengers.Contains(hit.transform))
                       {

                           movedPassengers.Add(hit.transform);
                       float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                       float pushX = (directionY == 1)?velocity.x:0;


                       passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), directionY == 1, true)); 
                       }
                   }

        }
        }


        //Horizontal Platform


        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);


        
                   if (hit && hit.distance != 0)
                   {

                       if (!movedPassengers.Contains(hit.transform))
                       {

                           movedPassengers.Add(hit.transform);
                       float pushY = -skinWidth;

                       float pushX = velocity.x - (hit.distance - skinWidth) * directionX;


                       passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), false, true)); 
                       }
                   }
        }
        }


        //Passenger on top of platform


        if (directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {


             float rayLength = skinWidth * 2;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);
                   Debug.DrawRay(rayOrigin, Vector2.up * rayLength, Color.red);


                   if (hit && hit.distance != 0)
                   {


                       if (!movedPassengers.Contains(hit.transform))
                       {

                           movedPassengers.Add(hit.transform);
                       float pushY = velocity.y;

                       float pushX = velocity.x;


                       passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX, pushY), true, true)); 
                       }
                   }

        }

        }

    }


    struct PassengerMovement
    {
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }



    void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;
            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying)?globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
    
}
