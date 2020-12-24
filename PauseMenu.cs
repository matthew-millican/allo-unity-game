using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{



    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
        
    }

    void Resume()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.SetTimeScale(1f);

        

    }

    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.SetTimeScale(0f);

    }



    public void ResumeClicked() 
    {
        Resume();
    }

    public void Restart() 
    {
        Resume();
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         isPaused = false;
    }

    public void Quit() 
    {
        Resume();
        SceneManager.LoadScene("LoadMenu");
    }
}
