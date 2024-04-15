using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public float playerHealth;
    public float maxPlayerHealth;
    public float playerScore;
    public GameObject player;

    
    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = GameObject.FindWithTag("Start").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(float score)
    {
        playerScore += score;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }

    public void UpdateSpawnPoint(Transform newSpawnPoint)
    {
        playerSpawnPoint = newSpawnPoint;
    }

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = playerSpawnPoint.position;
    }
}
