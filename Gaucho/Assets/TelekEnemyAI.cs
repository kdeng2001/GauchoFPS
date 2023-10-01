using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TelekEnemyAI : MonoBehaviour
{
    private EnemyAnimator anim;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public int damage = 10;
    public GameObject projectile;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        anim = GetComponent<EnemyAnimator>();
        player = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void FixedUpdate()
    {
        // check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }


        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            anim.SetTrigger("Walk Forward Fast");
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 5f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        anim.SetTrigger("Walk Forward Fast");
        agent.SetDestination(player.position);
    }


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void AttackPlayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // shooting
            // melee
            // any attack

            //rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            

            anim.SetTrigger("Projectile Attack 02");
            Debug.Log("ranged attack");
            alreadyAttacked = true;
            //DealDamage();
            Rigidbody rb = Instantiate(projectile, transform.position, transform.localRotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
            // call some function to deal damage to player
        }
    }

    private void ResetAttack()
    {
        Debug.Log("reset attack");
        alreadyAttacked = false;
    }

    //private void DealDamage()
    //{
    //    Debug.Log("deal damage");
    //    player.gameObject.GetComponent<playerStats>().TakeDamage(damage);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
