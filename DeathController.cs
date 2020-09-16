using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ShapeController))]
public class DeathController : MonoBehaviour
{


    public Transform[] respawnPoints;


    Transform currentRespawnPoint;


    SpriteRenderer renderer;
    public ParticleSystem DeathParticles;
    bool respawned;



    ShapeController shapeController;



    public float timer = 1.5f;


    public float counter = 1.5f;

    private IEnumerator coroutine;


    List<Transform> passedCheckpoints = new List<Transform>();


    public StatController statController;



    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        respawned = false;
        shapeController = GetComponent<ShapeController>();

    }


    void Update()
    {


        GetRespawnPoint();
        Reset();
    }



    void GetRespawnPoint()
    {

        for (int i = 0; i < respawnPoints.Length; i++)
        {

            Debug.Log(respawnPoints[i].position.x);

            if (respawnPoints[i].position.x <= gameObject.transform.position.x && !passedCheckpoints.Contains(respawnPoints[i]))
            {
                currentRespawnPoint = respawnPoints[i];
                if (i > 0)
                {
                    passedCheckpoints.Add(respawnPoints[i-1]);
                }

                
            }
        }
    }



    public void Die(Transform currentPosition)
    {

        shapeController.setDeath(true);

        respawned = false;

        if (!DeathParticles.isPlaying)
        {
            Instantiate(DeathParticles, currentPosition.position, Quaternion.identity);
        }
        renderer.sprite = null;
        coroutine = Wait(1f);
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
        gameObject.transform.position = currentRespawnPoint.position;
        shapeController.ReloadSprite();
        statController.isKilled();
        respawned = true;
        shapeController.setDeath(false);
    }



    public bool HasRespawned()
    {
        return respawned;
    }

}
