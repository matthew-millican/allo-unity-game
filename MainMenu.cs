using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    public AudioSource click;

    public AudioSource backgroundMusic;

    bool playMusic = true;


    public Button musicButton;


    void Start()
    {
        PlayerPrefs.SetInt("playSounds", 1);
    }




    void Awake() {
        DontDestroyOnLoad(click);
        DontDestroyOnLoad(backgroundMusic);
    }

    public void PlayGame ()
    {

        if (PlayerPrefs.GetInt("playSounds") == 1)
        {
            click.Play();
        }
        SceneManager.LoadScene("LoadMenu");
        ReloadButton();


    }


    public void QuitGame ()
    {
        click.Play();
        Application.Quit();
    }



    public void PlayMusic() 
    {
        if (playMusic)
        {
            playMusic = false;
            PlayerPrefs.SetInt("playSounds", 0);
            backgroundMusic.Stop();
        }
        else
        {
            {
                playMusic = true;
                PlayerPrefs.SetInt("playSounds", 1);
                if (!backgroundMusic.isPlaying) {
                    backgroundMusic.Play();
                }

            }
        }
        ReloadButton();
    }

    void ReloadButton()
    {
        ColorBlock colors = musicButton.colors;
        if (playMusic)
        {
            colors.normalColor = Color.green;
            colors.highlightedColor = Color.red;
        }
        else 
        {
            colors.normalColor = Color.red;
            colors.highlightedColor = Color.green;
        }

        musicButton.colors = colors;

    }
}
