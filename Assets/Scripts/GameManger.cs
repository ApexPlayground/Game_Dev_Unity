using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{

    public float playerHealth;
    public float maxPlayerHealth;
    public float playerScore;

    
    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
