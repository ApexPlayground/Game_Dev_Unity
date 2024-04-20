using System.Collections;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;
    public bool aggro;
    public float aggroSpeed;
    public float aggroTime;
    public Transform playerLocation;
    private float initialSpeed;
    public float detectTime;

    // Patrol points
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    private Transform currentTarget;

    public enum AIBehaviour
    { 
        Idle, Patrol, DetectPlayer, ChasePlayer, AggroIdle 
    }

    public AIBehaviour aiBehaviour;
    private Coroutine currentCoroutine;

    void Start()
    {
        initialSpeed = speed;
        currentTarget = patrolPoint1;
    }

    void Update()
    {
        Vector2 directionToPlayer = playerLocation.position - transform.position;
        float horizontalDirection = Mathf.Sign(directionToPlayer.x);

        switch (aiBehaviour)
        {
            case AIBehaviour.Idle:
                speed = 0;
                break;
            case AIBehaviour.Patrol:
                PatrolBetweenPoints();
                break;
            case AIBehaviour.DetectPlayer:
            case AIBehaviour.AggroIdle:
                speed = 0;
                break;
            case AIBehaviour.ChasePlayer:
                speed = aggroSpeed;
                MoveTowardsPlayer(horizontalDirection);
                break;
        }
    }

    void PatrolBetweenPoints()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == patrolPoint1 ? patrolPoint2 : patrolPoint1;
        }
        float step = initialSpeed * Time.deltaTime; // Use initialSpeed for consistency
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
    }


    void MoveTowardsPlayer(float horizontalDirection)
    {
        transform.position += new Vector3(horizontalDirection * speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!aggro)
            {
                ChangeState(StartCoroutine(DetectTime()));
            }
            else
            {
                ChangeState(null, AIBehaviour.ChasePlayer);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeState(StartCoroutine(AggroTimer()));
        }
    }

    IEnumerator DetectTime()
    {
        aiBehaviour = AIBehaviour.DetectPlayer;
        yield return new WaitForSeconds(detectTime);
        aggro = true;
        aiBehaviour = AIBehaviour.ChasePlayer;
    }

    IEnumerator AggroTimer()
    {
        aiBehaviour = AIBehaviour.AggroIdle;
        yield return new WaitForSeconds(aggroTime);
        if (aiBehaviour == AIBehaviour.AggroIdle)
        {
            aiBehaviour = AIBehaviour.Idle;
            aggro = false;
        }
    }

    private void ChangeState(Coroutine newCoroutine, AIBehaviour? newBehaviour = null)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = newCoroutine;

        if (newBehaviour.HasValue)
        {
            aiBehaviour = newBehaviour.Value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to player
        }
    }
}
