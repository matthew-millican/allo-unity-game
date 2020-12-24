using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player))]
public class YellowController : MonoBehaviour
{


    Player player;


    bool completed;

    public ParticleSystem DeathParticles;




    public Transform yellowUnlocked;


    public AudioSource audio;

    bool playing;

    bool instantiated;


    public float timer;

    float currentTimer;

    public NewOrderMenu canvasController;

    public OrderMenu oldController;

    ShapeController shapeController;
    // Start is called before the first frame update
    void Start()
    {

        currentTimer = timer;

        player = GetComponent<Player>();

        shapeController = GetComponent<ShapeController>();
        playing = false;
        instantiated = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x >= yellowUnlocked.position.x) {   
        currentTimer -= Time.deltaTime;
            if (!playing) {
                player.SetTimeScale(0.2f);

                if (!instantiated && currentTimer <= 0) {
                     Instantiate(DeathParticles, transform.position, Quaternion.identity);

                     if (PlayerPrefs.GetInt("playSounds") == 1)
                     {
                         audio.Play();
                     }
                     instantiated = true;
                     canvasController.active = true;
                     oldController.active = false;
                     shapeController.Unlocked();
                     shapeController.setOrder(4);
                     shapeController.UpdateSprites();
                }
            }
        }

        if (instantiated) 
        {
            timer -= Time.deltaTime;
            if (timer <= 0) 
            {
                player.SetTimeScale(1f);
            }
        }
        
    }
}
