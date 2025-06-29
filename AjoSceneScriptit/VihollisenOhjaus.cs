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
    [SerializeField] private bool kuollut;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    Animator animaattori;
    public VihollisenOhjaus instanssi;

    AudioSource ääni;
    public AudioClip kavelyÄäni;

    float edellinen = 0f;
    private void Awake() {
        player = GameObject.Find("CarPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
        animaattori = GetComponentInChildren<Animator>();
        animaattori.SetBool("seisoo", true);
        instanssi = this;
        ääni = GetComponentInChildren<AudioSource>();
    }

    private void Update() {
        if (!kuollut) {
            bool previousSight = playerInSightRange;
            bool previousAttack = playerInAttackRange;

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            //if (previousSight != playerInSightRange || previousAttack != playerInAttackRange) {
                //Debug.Log($"Havainto muuttui: Näkyvissä: {playerInSightRange}, Hyökkäysetäisyydellä: {playerInAttackRange}");
        //}

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        
        }
    }
    private void Patroling() {
        //Debug.Log("Partioidaan");
        //animator.CrossFade("Walk", 0.2f); // Siirtyminen "Walk"-animaatioon 0.2 sekunnissa

        animaattori.SetBool("seisoo", true);
        animaattori.SetBool("kavelee", false);
        animaattori.SetBool("tahtaa", false);
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
        //Debug.Log("etsitaan");
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
            //Debug.Log(walkPointSet);
        }
        else {
            //Debug.Log("Raycast maassa: " + Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround));
            randomZ = Random.Range(-walkPointRange, walkPointRange);
            randomX = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        }
    }
    private void ChasePlayer() {
        //Debug.Log("jahdataan");
        //ääni.clip = kavelyÄäni;
        if (Time.time - edellinen >= 2.2f) {
            ääni.PlayOneShot(kavelyÄäni, 5f);
            edellinen = Time.time;
        }
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animaattori.SetBool("tahtaa", false);
        animaattori.SetBool("kavelee", true);
    }
    private void AttackPlayer() {
        //Debug.Log("Hyökkäys käynnissä...");
        animaattori.SetBool("seisoo", false);
        animaattori.SetBool("kavelee", false);
        animaattori.SetBool("tahtaa", true);
        agent.isStopped = true;
        transform.LookAt(player);
    }
    public void Kuole() {
        animaattori.SetBool("kuolee", true);
        agent.isStopped = true;
        //agent.velocity = Vector3.zero;
        //agent.ResetPath();
        //agent.SetDestination(transform.position);
        kuollut = true;
    }
    public bool onkoKuollut() {
        if (kuollut) {
        return true;
        } else {
            return false;
        }
    }
    //private void ResetAttack() {
    //    alreadyAttacked = false;
    //}
}