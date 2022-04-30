using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private Animator animator;

    private PlayerStats playerStats;

    private bool isHostile;
    
    //Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States
    [SerializeField] float sightRange, attackRange;
    [SerializeField]private bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        isHostile = false;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(!isHostile) Patrolling();
        if (isHostile)
        {
            agent.speed = 5;
            agent.acceleration = 5;
            ChasePlayer();
        }
        
        if(playerInSightRange && playerInAttackRange && isHostile) AttackPlayer();
        
    }

    private void Patrolling()
    {
        animator.SetBool("isWalking", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalk = transform.position - walkPoint;
        
        //WalkPoint reached
        if (distanceToWalk.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        animator.SetBool("isWalking",false);
        agent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            RaycastHit hit;
            animator.SetTrigger("Attack");
            alreadyAttacked = true;

            if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, attackRange))
            {
                float damage = UnityEngine.Random.Range(5, 10);
                playerStats.DrainHealth(damage);
                print(damage);
            }
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void BecomeHostile()
    {
        isHostile = true;
    }
    
}
