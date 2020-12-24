using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour



{


    public StatController stats;


    public Animator animator;


    public Transform finishPoint;


    public GameObject player;


    public TextMeshProUGUI timeField;

    public TextMeshProUGUI deathField;

    public TextMeshProUGUI achievementField;

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
            timeField.text = "time: " + Convert.ToString(Math.Round(stats.getTotalTime(), 2));
            deathField.text = "deaths: " + Convert.ToString(stats.getNumberOfDeaths());
            achievementField.text = "achievements: " + Convert.ToString(stats.getAchievementsUnlocked());
            orderMenu.enabled = false;
            SaveGame();
        }


    }


    void SaveGame() {
        String levelName = "level" + Convert.ToString(level);

        float time = PlayerPrefs.GetFloat(levelName + "Time");
        int deaths = PlayerPrefs.GetInt(levelName + "Deaths");
        int achievements = PlayerPrefs.GetInt(levelName + "A");

        if (stats.getTotalTime() > time) 
        {
            PlayerPrefs.SetFloat(levelName + "Time", (float) (Math.Round(stats.getTotalTime(), 2)));
        }
        if (stats.getNumberOfDeaths() < deaths) 
        {
            PlayerPrefs.SetInt(levelName + "Deaths", stats.getNumberOfDeaths());
        }

        if (stats.getAchievementsUnlocked() > achievements) 
        {
            PlayerPrefs.SetInt(levelName + "A", stats.getAchievementsUnlocked());
        }
        PlayerPrefs.Save();
    }






    public bool getFinished()
    {
        return finished;
    }

    public void FadeOut()
    {
        animator.SetTrigger("Fade_out");
    }


    public void MenuButton()
    {
        SceneManager.LoadScene("LoadMenu");
    }

    public void ReplayButton()
    {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


}
