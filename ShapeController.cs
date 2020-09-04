using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShapeController : MonoBehaviour
{





    public Sprite diamond;

    public Sprite circle;

    public Sprite square;


    public Sprite triangle;

    public Sprite squircle;

    SpriteRenderer objectSprite;






    const float firstOrder = 1;
    const float secondOrder = 2;
    const float thirdOrder = 3;

    const float fourthOrder = 4;

    const float fifthOrder = 5;


    Color diamondColor = new Color(1, 1, 1, 1);
    Color squareColor = new Color(1, 0, 0, 1f);

    Color circleColor = new Color(0f, 0f, 1, 1);

    Color triangleColor = new Color(0.9f, 1f, 0.9f, 1);


    


    float currentOrder;

    float timeShiftHeld;

    float timeHeld;

    // Start is called before the first frame update
    void Start()
    {


        currentOrder = firstOrder;

        objectSprite = gameObject.GetComponent<SpriteRenderer>();


        objectSprite.color = squareColor;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("e"))
        {
            changeSprite();
        }
        

        if (Input.GetKey(KeyCode.LeftControl))
        {
            timeShiftHeld += Time.deltaTime;
            shrink();
        }
        else
        {
            timeShiftHeld = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            timeHeld += Time.deltaTime;
            expand();

        }
        else
        {
            timeHeld = 0f;
        }
    }


    public float getCurrentOrder()
    {
        return currentOrder;
    }


    void expand()
    {
        if (currentOrder == firstOrder)
        {
            Vector3 currentTransform = transform.localScale;
            if (currentTransform.y > 1)
            {
                transform.localScale -= new Vector3(0, 3f * timeHeld, 0);
            }
        }
    }



    void shrink()
    {
        if (currentOrder == firstOrder)
        {
            Vector3 currentTransform = transform.localScale;
            if (currentTransform.y < 8)
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
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == secondOrder)
        {
            currentOrder = thirdOrder;
            objectSprite.color = diamondColor;
            objectSprite.sprite = diamond;
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == thirdOrder)
        {
            currentOrder = fourthOrder;
            objectSprite.color = new Color(1, 1, 1, 1);
            objectSprite.sprite = triangle;
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == fourthOrder)
        {
            currentOrder = fifthOrder;
            objectSprite.sprite = squircle;
            objectSprite.color = new Color(1, 1, 1, 1);
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == fifthOrder)
        {
            currentOrder = firstOrder;
            objectSprite.color = squareColor;
            objectSprite.sprite = square;
            //controller.setLayerMask(collisionsLayers[3]);
        }
    }
}
