using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (ShapeController))]
public class DeathController : MonoBehaviour
{


    ShapeController shapeController;


    Color currentShapeColor;

    Color currentObstacleColor;
    // Start is called before the first frame update
    void Start()
    {
        shapeController = GetComponent<ShapeController>();
    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
