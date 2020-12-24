using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class LoadMenuController : MonoBehaviour
{



    public GameObject[] levels;

    public Material white;


    public Material red;

    int currentLevel;

    int maxLevel = 14;


    public TextMeshProUGUI levelField;

    public TextMeshProUGUI deathField;
    
    public TextMeshProUGUI timeField;

    public TextMeshProUGUI achievementField;


    public AudioSource backClick;

    public AudioSource playClick;

    public AudioSource moveLevelClick;






    void Start() {
        currentLevel = 0;
        levels[0].GetComponent<MeshRenderer>().material = red;
        levelField.text = Convert.ToString("level " + (currentLevel+1));


        DisplayText();

    }

    void Awake() {
        DontDestroyOnLoad(backClick);
        DontDestroyOnLoad(playClick);
    }


    void DisplayText() {
        timeField.text = "time: ";
        deathField.text = "deaths: ";
        

        String levelName = "level" + (currentLevel + 1);
        float time = PlayerPrefs.GetFloat(levelName + "Time");
        int deaths = PlayerPrefs.GetInt(levelName + "Deaths");
        int achievements = PlayerPrefs.GetInt(levelName + "A");

        if (currentLevel>=5) {
            timeField.text = Convert.ToString("more levels to come in the future!");
            deathField.text =Convert.ToString("");
        }
        else {
        timeField.text = "time: " + Convert.ToString(time);
        deathField.text = "deaths: " + Convert.ToString(deaths);
        achievementField.text = "achievements: " + Convert.ToString(achievements);
        }
    }

    public void MoveForward () 
    {
        if (currentLevel == 13) {
            currentLevel = 0;
            levels[13].GetComponent<MeshRenderer>().material = white;
            levels[currentLevel].GetComponent<MeshRenderer>().material = red;

        } 
        else {
            currentLevel++;
            levels[currentLevel-1].GetComponent<MeshRenderer>().material = white;
            levels[currentLevel].GetComponent<MeshRenderer>().material = red;
        }

        if (PlayerPrefs.GetInt("playSounds") == 1) {
            moveLevelClick.Play();
        }

        levelField.text = Convert.ToString("Level " + (currentLevel+1));

        DisplayText();

    }



    public void MoveBackward () 
    {
        if (currentLevel == 0) {
            currentLevel = 13;
            levels[0].GetComponent<MeshRenderer>().material = white;
            levels[currentLevel].GetComponent<MeshRenderer>().material = red;

        } 
        else {
            currentLevel--;
            levels[currentLevel+1].GetComponent<MeshRenderer>().material = white;
            levels[currentLevel].GetComponent<MeshRenderer>().material = red;
        }
       
        if (PlayerPrefs.GetInt("playSounds") == 1) {
            moveLevelClick.Play();
        }


        levelField.text = Convert.ToString("Level " + (currentLevel + 1));
        DisplayText();
    }



    public void Back() {


            if (PlayerPrefs.GetInt("playSounds") == 1) {
            backClick.Play();
        }
        SceneManager.LoadScene("MainMenu");

        
        
    }

    public void Play() {

            if (PlayerPrefs.GetInt("playSounds") == 1) {
            playClick.Play();
        }


        if (currentLevel < 5) {

            SceneManager.LoadScene("Level" + (currentLevel + 1));
        }
    }
}
