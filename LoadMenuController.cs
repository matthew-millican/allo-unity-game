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


    SaveController[] saves = new SaveController[14];


    public GameObject[] levels;

    public Material white;


    public Material red;

    int currentLevel;

    int maxLevel = 14;


    public TextMeshProUGUI levelField;

    public TextMeshProUGUI deathField;
    
    public TextMeshProUGUI timeField;


    public AudioSource backClick;

    public AudioSource playClick;

    public AudioSource moveLevelClick;


    void Start() {
        currentLevel = 0;
        levels[0].GetComponent<MeshRenderer>().material = red;
        levelField.text = Convert.ToString("Level " + (currentLevel+1));


        for (int i = 0; i < saves.Length; i++) {
            if (File.Exists("C:/Users/Matthew/Desktop/level" + (i+1) + ".allo")) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("C:/Users/Matthew/Desktop/level" + (i+1) + ".allo", FileMode.Open);
                saves[i] = (SaveController)bf.Deserialize(file);
                file.Close();
        }
        }
        DisplayText();

    }

    void Awake() {
        DontDestroyOnLoad(backClick);
        DontDestroyOnLoad(playClick);
    }



    void DisplayText() {
        Debug.Log(saves[0].totalTime);
        timeField.text = "Time: ";
        deathField.text = "Deaths: ";

        if (currentLevel>=5) {
            timeField.text = "More levels to come in the future!";
            deathField ="";
        }
        else {
        timeField.text = "Time: " + Convert.ToString(saves[currentLevel].totalTime);
        deathField.text = "Deaths: " + Convert.ToString(saves[currentLevel].deaths);
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

        moveLevelClick.Play();

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
        moveLevelClick.Play();

        levelField.text = Convert.ToString("Level " + (currentLevel + 1));
        DisplayText();
    }



    public void Back() {

        backClick.Play();
        SceneManager.LoadScene("MainMenu");

        
        
    }

    public void Play() {
        playClick.Play();

        if (currentLevel < 5) {

            SceneManager.LoadScene("Level" + (currentLevel + 1));
        }
    }
}
