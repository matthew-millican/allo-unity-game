using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourScript : MonoBehaviour
{



    public float waitTime;


    public float timer = 4f;


    float currentTimer;


    public Material firstMaterial;

    public Material SecondMaterial;
    

    int currentIndex;

    public AudioSource change;


    void Start() {
        if (waitTime != 0) {
            currentTimer = waitTime;
        }
        else {
            currentTimer = timer;
        }
        currentIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        bool canContinue = checkWaitTime();
        if (canContinue) {
        if (currentTimer <= 0) {

            change.Play();
            if (currentIndex == 1) {
                currentIndex++;
                GetComponent<MeshRenderer>().material = SecondMaterial;
            }
            else if (currentIndex == 2) {
                currentIndex--;
                GetComponent<MeshRenderer>().material = firstMaterial;
            }

            currentTimer = timer;
        }
        }

        


        
        
    }


    bool checkWaitTime() {
        if (waitTime != 0) {
            if (currentTimer <= 0) {
                waitTime = 0;
                currentTimer = timer;
            }
            else return false;
        }
        return true;
    }

}
