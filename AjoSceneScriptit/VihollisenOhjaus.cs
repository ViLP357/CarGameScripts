using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VihollisenOhjaus : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Par´trolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
 
    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake() {
        player = GameObject.Find("CarPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private void Patroling() {
        //Debug.Log("Partioidaan");

        if (!walkPointSet) SearchWalkPoint();

        //Debug.Log(walkPointSet);
        
        if (walkPointSet) {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
            Debug.Log(walkPointSet);
        }
        else {
            //Debug.Log("Raycast maassa: " + Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround));
        }
        
    }
    private void ChasePlayer() {
        //Debug.Log("jahdataan");
        agent.SetDestination(player.position);

    }
    private void AttackPlayer() {
        //Debug.Log("Voisi hyökätä");
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked) {
            //hyökkäysfunctioita tännä
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack() {
        alreadyAttacked = false;
    }
}