using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : MonoBehaviour
{



    public float changeAmount;

    // Update is called once per frame
    void Update()
    {

        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + changeAmount, 0);






        
    }
}
