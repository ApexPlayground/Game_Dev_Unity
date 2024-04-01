using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    public GameManger gameManger;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManger = GameObject.Find("GameManager").GetComponent<GameManger>();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameManger.playerScore;
        healthText.text = "Health: " + gameManger.playerHealth;

    }
}
