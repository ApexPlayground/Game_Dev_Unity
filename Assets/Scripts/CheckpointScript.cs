using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public GameManger gameManager;

    private static readonly int CheckpointActivated = Animator.StringToHash("CheckpointActivated");

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateSpawnPoint(transform);
            GetComponent<Animator>().SetTrigger(CheckpointActivated);
        }
    }
}
