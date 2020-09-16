using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatController : MonoBehaviour
{


    public LevelManager manager;

    float totalTime;

    float timeBetweenDeaths = 4f;


    float currentTime;


    int numberOfDeaths;


    void Start()
    {
        numberOfDeaths = 0;
    }

    // Update is called once per frame
    void Update()
    {


        bool finished = manager.getFinished();
        currentTime += Time.deltaTime;


        if (!finished)
        {
            totalTime += Time.deltaTime;
        }


        
        
    }



    public void isKilled()
    {
        if (currentTime >= timeBetweenDeaths)
        {
            numberOfDeaths++;
            currentTime = 0f;
        }
    }


    public float getTotalTime()
    {
        return totalTime;
    }

    public int getNumberOfDeaths()
    {
        return numberOfDeaths;
    }
}
