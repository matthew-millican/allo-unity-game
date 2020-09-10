using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Transform respawnPoint;


    public GameObject playerPrefab;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void Respawn()
    {
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}
