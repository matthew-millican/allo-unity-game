using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShapeController : MonoBehaviour
{





    public Sprite diamond;

    public Sprite circle;

    public Sprite square;


    public Sprite triangle;

    public Sprite squircle;


    public Sprite menuCircle;
    public Sprite menuSquare;

    SpriteRenderer objectSprite;


    public GameObject canvas;


    OrderMenu canvasController;



    Sprite firstSprite;

    Sprite secondSprite;


    Sprite thirdSprite;



    Color firstColor;

    Color secondColor;

    Color thirdColor;









    const float firstOrder = 1;
    const float secondOrder = 2;
    const float thirdOrder = 3;

    const float fourthOrder = 4;

    const float fifthOrder = 5;


    Color diamondColor = new Color(1, 1, 1, 1);
    Color squareColor = new Color(1, 0, 0, 1f);

    Color circleColor = new Color(0f, 0f, 1, 1);

    Color triangleColor = new Color(1, 1, 1, 1);


    


    float currentOrder;

    float timeShiftHeld;

    float timeHeld;



    int behaviour;


    bool dead;
    // Start is called before the first frame update
    void Start()
    {

        canvasController = canvas.GetComponent<OrderMenu>();


        currentOrder = firstOrder;

        objectSprite = gameObject.GetComponent<SpriteRenderer>();


        objectSprite.color = squareColor;

        dead = false;
        
    }

    // Update is called once per frame
    void Update()
    {



        UpdateSprites();


        if (Input.GetKeyDown("e"))
        {
            changeSprite();
        }
        
    }

    public void setDeath(bool dead)
    {
        this.dead = dead;
    }


    public int Behaviour()
    {


        if (objectSprite.sprite == square)
        {
            return 1;
        }
        if (objectSprite.sprite == circle)
        {
            return 2;
        }
        
        return 3;
    }


    public void UpdateSprites()
    {

        Sprite currentSprite = objectSprite.sprite;


        Image Image1 = canvasController.GetImage(1);
        Image Image2 = canvasController.GetImage(2);
        Image Image3 = canvasController.GetImage(3);

        checkFirstImageSprite(Image1);
        checkSecondImageSprite(Image2);
        checkThirdImageSprite(Image3);

        if (!dead)
        {
            ReloadSprite();
        }


    }

    void checkFirstImageSprite(Image image)
    {

        if (image.sprite == triangle)
        {
            firstSprite = triangle;
            firstColor = triangleColor;
        }
        else if (image.sprite == menuSquare)
        {
            firstSprite = square;
            firstColor = squareColor;
        }
        else if (image.sprite == menuCircle)
        {
            firstSprite = circle;
            firstColor = circleColor;
        }

    }
    
    void checkSecondImageSprite(Image image)
    {

        if (image.sprite == triangle)
        {
            secondSprite = triangle;
            secondColor = triangleColor;
        }
        else if (image.sprite == menuSquare)
        {
            secondSprite = square;
            secondColor = squareColor;
        }
        else if (image.sprite == menuCircle)
        {
            secondSprite = circle;
            secondColor = circleColor;
        }

    }


    void checkThirdImageSprite(Image image)
    {

        if (image.sprite == triangle)
        {
            thirdSprite = triangle;
            thirdColor = triangleColor;
        }
        else if (image.sprite == menuSquare)
        {
            thirdSprite = square;
            thirdColor = squareColor;
        }
        else if (image.sprite == menuCircle)
        {
            thirdSprite = circle;
            thirdColor = circleColor;
        }

    }



    public float getCurrentOrder()
    {
        return currentOrder;
    }

    





    public void ReloadSprite()
    {
        if (currentOrder == firstOrder)
        {
            objectSprite.color = firstColor;
            objectSprite.sprite = firstSprite;
        }
        else if (currentOrder == secondOrder)
        {
            objectSprite.color = secondColor;
            objectSprite.sprite = secondSprite;
        }
        else if (currentOrder == thirdOrder)
        {
            objectSprite.color = thirdColor;
            objectSprite.sprite = thirdSprite;
        }
    }




    void changeSprite()
    {
        transform.localScale = new Vector3(3, 3, 3);
        if (currentOrder == firstOrder)
        {
            currentOrder = secondOrder;
            objectSprite.color = secondColor;
            objectSprite.sprite = secondSprite;
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == secondOrder)
        {
            currentOrder = thirdOrder;
            objectSprite.color = thirdColor;
            objectSprite.sprite = thirdSprite;
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == thirdOrder)
        {
            currentOrder = firstOrder;
            objectSprite.color = firstColor;
            objectSprite.sprite = firstSprite;
            //controller.setLayerMask(collisionsLayers[3]);
        }
    }
}
