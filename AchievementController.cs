using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{


    SpriteRenderer renderer;


    public ParticleSystem DeathParticles;


    const float timer = 2f;


    float counter;


    public StatController stats;

    bool animPlayed = false;




    bool isCollected;
    // Start is called before the first frame update
    void Start()
    {

        renderer = GetComponent<SpriteRenderer>();
        isCollected = false;
        
    }

    // Update is called once per frame
    void Update()
    {


        counter+= Time.deltaTime;


        if (isCollected)
        {
            Die();
        }
        
    }

    void Die()
    {

        if (counter >= timer && !animPlayed)
        {
            Instantiate (DeathParticles, gameObject.transform.position, Quaternion.identity);
            stats.unlockAchievement();
            counter = 0;
            animPlayed = true;
        }
        renderer.sprite = null;
        isCollected = false;
    }




    public void collected()
    {
        isCollected = true;
    }
}
