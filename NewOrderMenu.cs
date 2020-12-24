using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewOrderMenu : MonoBehaviour
{


    Player playerScript;


    public Sprite squareSprite;

    public Sprite triangleSprite;

    public Sprite circleSprite;

    public Sprite squircleSprite;


    Sprite currentSprite;


    public Image Image1;

    public Image Image2;

    public Image Image3;

    public Image Image4;


    public Image BackgroundImage1;

    public Image BackgroundImage2;

    public Image BackgroundImage3;
    public Image BackgroundImage4;


    public GameObject player;


    ShapeController shapeController;



    int firstOrder;

    int secondOrder;

    int thirdOrder;
    int fourthOrder;


    float currentOrder;

    public static bool paused = false;


    public GameObject pauseMenuUI;

    public bool active;


    void Start()
    {
        shapeController = player.GetComponent<ShapeController>();

        playerScript = player.GetComponent<Player>(); 
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
        else if (index == 3)
        {
            return Image3;
        }
        else {
            return Image4;
        }

    }


    void Update()
    {


        if (active) {


        currentOrder = shapeController.getCurrentOrder();

        BackgroundImage1.enabled = currentOrder == 1;
        BackgroundImage2.enabled = currentOrder == 2;
        BackgroundImage3.enabled = currentOrder == 3;
        BackgroundImage4.enabled = currentOrder == 4;

    




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
        Time.timeScale = playerScript.GetActiveTimeScale();
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
            Image1.sprite = squircleSprite;
        }
        else if (Image1.sprite == squircleSprite) {
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
            Image2.sprite = squircleSprite;
        }
        else if (Image2.sprite == squircleSprite) {
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
            Image3.sprite = squircleSprite;
        }
        else if (Image3.sprite == squircleSprite) {
            Image3.sprite = squareSprite;
        }

    }

       public void FourthButton()
    {
        if (Image4.sprite == squareSprite)
        {
            Image4.sprite = circleSprite;
        }
        else if (Image4.sprite == circleSprite)
        {
            Image4.sprite = triangleSprite;
        }
        else if (Image4.sprite == triangleSprite)
        {
            Image4.sprite = squircleSprite;
        }
        else if (Image4.sprite == squircleSprite) {
            Image4.sprite = squareSprite;
        }

    }
}
