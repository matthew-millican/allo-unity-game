using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelManager : MonoBehaviour



{


    public StatController stats;


    public Animator animator;


    public Transform finishPoint;


    public GameObject player;


    public TextMeshProUGUI timeField;

    public TextMeshProUGUI deathField;

    public Canvas orderMenu;






    public int level;


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
            SaveGame();


        }


    }


    void SaveGame() {
        SaveController save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("C:/Users/Matthew/Desktop/level" + level + ".allo");
        bf.Serialize(file, save);
        file.Close();
    }



    private SaveController CreateSaveGameObject() {
        SaveController save = new SaveController();

        save.deaths = stats.getNumberOfDeaths();
        save.levelNumber = level;
        save.totalTime = stats.getTotalTime();

        return save;
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
