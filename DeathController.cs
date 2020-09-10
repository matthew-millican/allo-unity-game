using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathController : MonoBehaviour
{


    public Transform respawnPoint;


    SpriteRenderer renderer;
    public ParticleSystem DeathParticles;
    bool respawned;



    ShapeController shapeController;



    public float timer = 1.5f;


    public float counter = 1.5f;

    private IEnumerator coroutine;



    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        respawned = false;
        shapeController = GetComponent<ShapeController>();

    }


    void Update()
    {
        Reset();
    }



    public void Die(Transform currentPosition)
    {

        respawned = false;

        if (!DeathParticles.isPlaying)
        {
            Instantiate(DeathParticles, currentPosition.position, Quaternion.identity);
        }
        renderer.sprite = null;
        coroutine = Wait(0.5f);
        StartCoroutine (coroutine);
        respawned = true;
        //gameObject.transform.position = respawnPoint.position;
    }
    void Reset()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            respawned = false;
            counter = timer;
        }
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.transform.position = respawnPoint.position;
        shapeController.ReloadSprite();
        respawned = true;
    }



    public bool HasRespawned()
    {
        return respawned;
    }

}
