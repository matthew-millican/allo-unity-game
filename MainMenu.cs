using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public AudioSource click;

    public AudioSource backgroundMusic;




    void Awake() {
        DontDestroyOnLoad(click);
        DontDestroyOnLoad(backgroundMusic);
    }

    public void PlayGame ()
    {


        click.Play();
        SceneManager.LoadScene("LoadMenu");



    }


    public void QuitGame ()
    {
        click.Play();
        Application.Quit();
    }
}
