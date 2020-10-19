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

    NewOrderMenu newCanvas;



    Sprite firstSprite;

    Sprite secondSprite;


    Sprite thirdSprite;

    Sprite fourthSprite;



    Color firstColor;

    Color secondColor;

    Color thirdColor;

    Color fourthColor;









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

    public bool squircleUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        canvasController = canvas.GetComponent<OrderMenu>();
        newCanvas = canvas.GetComponent<NewOrderMenu>();


        currentOrder = firstOrder;

        objectSprite = gameObject.GetComponent<SpriteRenderer>();


        objectSprite.color = squareColor;

        dead = false;

        //squircleUnlocked = false;
        
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
        if (objectSprite.sprite == triangle) 
        {
            return 3;
        }

        return 4;
    }


    public void UpdateSprites()
    {

        Sprite currentSprite = objectSprite.sprite;


        Image Image1;
        Image Image2;
        Image Image3;
        Image Image4;
        if (!squircleUnlocked) {
        Image1 = canvasController.GetImage(1);
        Image2 = canvasController.GetImage(2);
        Image3 = canvasController.GetImage(3);
        checkFirstImageSprite(Image1);
        checkSecondImageSprite(Image2);
        checkThirdImageSprite(Image3);
        }
        else if (squircleUnlocked) {
            Image1 = newCanvas.GetImage(1);
            Image2 = newCanvas.GetImage(2);
            Image3 = newCanvas.GetImage(3);
            Image4 = newCanvas.GetImage(4);
            checkFirstImageSprite(Image1);
            checkSecondImageSprite(Image2);
            checkThirdImageSprite(Image3);
            checkFourthImageSprite(Image4);
        }

        if (!dead)
        {
            ReloadSprite();
        }


    }


    public void setOrder(float order) {
        currentOrder = order;
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
        else if (image.sprite == squircle) {
            firstSprite = squircle;
            firstColor = triangleColor;
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
        else if (image.sprite == squircle) {
            secondSprite = squircle;
            secondColor = triangleColor;
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
        else if (image.sprite == squircle) {
            thirdSprite = squircle;
            thirdColor = triangleColor;
        }

    }

    
    void checkFourthImageSprite(Image image)
    {

        if (image.sprite == triangle)
        {
            fourthSprite = triangle;
            fourthColor = triangleColor;
        }
        else if (image.sprite == menuSquare)
        {
            fourthSprite = square;
            fourthColor = squareColor;
        }
        else if (image.sprite == menuCircle)
        {
            fourthSprite = circle;
            fourthColor = circleColor;
        }
        else if (image.sprite == squircle) {
            fourthSprite = squircle;
            fourthColor = triangleColor;
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
        else if (currentOrder == fourthOrder) 
        {

            Debug.Log("!!!");
            objectSprite.color = fourthColor;
            objectSprite.sprite = fourthSprite;
        }
    }


    public void Unlocked() {
        squircleUnlocked = true;
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
        else if (currentOrder == thirdOrder && squircleUnlocked)
        {
            currentOrder = fourthOrder;
            objectSprite.color = fourthColor;
            objectSprite.sprite = fourthSprite;
            //controller.setLayerMask(collisionsLayers[3]);
        }
        else if (currentOrder == thirdOrder) {
            currentOrder = firstOrder;
            objectSprite.color = firstColor;
            objectSprite.sprite = firstSprite;
        }
        else if (currentOrder == fourthOrder) {
            currentOrder = firstOrder;
            objectSprite.color = firstColor;
            objectSprite.sprite = firstSprite;
        }
    }
}
