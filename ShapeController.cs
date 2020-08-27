using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{




    public Sprite diamond;

    public Sprite circle;

    public Sprite square;


    public Sprite triangle;

    SpriteRenderer objectSprite;





    const float firstOrder = 1;
    const float secondOrder = 2;
    const float thirdOrder = 3;

    const float fourthOrder = 4;


    Color diamondColor = new Color(1, 0, 1, 20);
    Color squareColor = Color.red;

    Color circleColor = Color.blue;

    Color triangleColor = Color.green;


    


    float currentOrder;

    float timeShiftHeld;

    // Start is called before the first frame update
    void Start()
    {


        currentOrder = firstOrder;

        objectSprite = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("c"))
        {
            changeSprite();
        }
        

        if (Input.GetKey(KeyCode.LeftControl))
        {
            timeShiftHeld += Time.deltaTime;
            doSkill();
        }
        else
        {
            timeShiftHeld = 0f;
        }
    }


    public float getCurrentOrder()
    {
        return currentOrder;
    }



    void doSkill()
    {
        if (currentOrder == firstOrder)
        {
            Vector3 currentTransform = transform.localScale;
            if (currentTransform.y < 6)
            {
                transform.localScale += new Vector3(0, 2f * timeShiftHeld, 0);
            }
        }
    }




    void changeSprite()
    {

        transform.localScale = new Vector3(3, 3, 3);
        if (currentOrder == firstOrder)
        {
            currentOrder = secondOrder;
            objectSprite.color = circleColor;
            objectSprite.sprite = circle;
        }
        else if (currentOrder == secondOrder)
        {
            currentOrder = thirdOrder;
            objectSprite.color = diamondColor;
            objectSprite.sprite = diamond;
        }
        else if (currentOrder == thirdOrder)
        {
            currentOrder = firstOrder;
            objectSprite.color = squareColor;
            objectSprite.sprite = square;
        }
    }
}
