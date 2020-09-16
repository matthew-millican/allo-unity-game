using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class LevelManager : MonoBehaviour



{


    public StatController stats;


    public Animator animator;


    public Transform finishPoint;


    public GameObject player;


    public TextMeshProUGUI timeField;

    public TextMeshProUGUI deathField;

    public Canvas orderMenu;


    bool finished;

    void Start()
    {
        finished = false;
    }



    void Update()
    {
        if (player.transform.position.x >= finishPoint.position.x)
        {
            FadeOut();
            finished = true;
            timeField.text = "Total Time: " + Convert.ToString(Math.Round(stats.getTotalTime(), 2));
            deathField.text = "Total Deaths: " + Convert.ToString(stats.getNumberOfDeaths());
            orderMenu.enabled = false;
        }


    }



    public bool getFinished()
    {
        return finished;
    }

    public void FadeOut()
    {
        animator.SetTrigger("Fade_out");
    }


}
