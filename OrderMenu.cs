using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrderMenu : MonoBehaviour
{


    public Sprite squareSprite;

    public Sprite triangleSprite;

    public Sprite circleSprite;


    Sprite currentSprite;


    public Image Image1;

    public Image Image2;

    public Image Image3;


    public Image BackgroundImage1;

    public Image BackgroundImage2;

    public Image BackgroundImage3;


    public GameObject player;


    ShapeController shapeController;



    int firstOrder;

    int secondOrder;

    int thirdOrder;


    float currentOrder;

    public static bool paused = false;


    public GameObject pauseMenuUI;


    void Start()
    {
        shapeController = player.GetComponent<ShapeController>();
    }



    public Image GetImage(int index)
    {

        if (index == 1)
        {
            return Image1;
        }
        else if (index == 2)
        {
            return Image2;
        }
        else 
        {
            return Image3;
        }

    }


    void Update()
    {


        currentOrder = shapeController.getCurrentOrder();

        Debug.Log(currentOrder);

        BackgroundImage1.enabled = currentOrder == 1;
        BackgroundImage2.enabled = currentOrder == 2;
        BackgroundImage3.enabled = currentOrder == 3;

    




        if (Input.GetKey(KeyCode.Q))
        {
            paused = true;
        }
        else
        {
            
            paused = false;
        }

        if (paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.4f;
        paused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false; 

    }

    public void FirstButton()
    {

         if (Image1.sprite == squareSprite)
        {
            Image1.sprite = circleSprite;
        }
        else if (Image1.sprite == circleSprite)
        {
            Image1.sprite = triangleSprite;
        }
        else if (Image1.sprite == triangleSprite)
        {
            Image1.sprite = squareSprite;
        }




    }

    public void SecondButton()
    {
           if (Image2.sprite == squareSprite)
        {
            Image2.sprite = circleSprite;
        }
        else if (Image2.sprite == circleSprite)
        {
            Image2.sprite = triangleSprite;
        }
        else if (Image2.sprite == triangleSprite)
        {
            Image2.sprite = squareSprite;
        }
    }

    public void ThirdButton()
    {
        if (Image3.sprite == squareSprite)
        {
            Image3.sprite = circleSprite;
        }
        else if (Image3.sprite == circleSprite)
        {
            Image3.sprite = triangleSprite;
        }
        else if (Image3.sprite == triangleSprite)
        {
            Image3.sprite = squareSprite;
        }
    }
}
