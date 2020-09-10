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



    int firstOrder;

    int secondOrder;

    int thirdOrder;

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
           if (Image2.sprite ==squareSprite)
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
               if (Image3.sprite ==squareSprite)
        {
            Image2.sprite = circleSprite;
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
