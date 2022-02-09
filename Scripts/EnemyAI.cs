using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject portal;
    public GameObject skullPlatform;

    //Movement
    public float walkSpeed = 2.0f;
    public float runSpeed = 8.0f;
    //Animation
    public Animator anim;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public Slider lifeBar;
    public float damageTaken = 20;
    public float health = 120;
    private float currentHealth;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float force = 1f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake() {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = health;
    }

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange  = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if (health <= 0) {
            portal.SetActive(true);
            skullPlatform.SetActive(false);
            Destroy(this.gameObject);
        }

    }

    private void Patroling() {
        agent.speed = walkSpeed;
        anim.SetBool("ChaseEnemy", false);
        anim.SetBool("Patrolling", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
            
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint() {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer() {
        agent.speed = runSpeed;
        anim.SetBool("Patrolling", false);
        agent.SetDestination(player.position);
        anim.SetBool("ChaseEnemy", true);
    }

    private void AttackPlayer() {
        anim.SetBool("ChaseEnemy", false);
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked) {

            // Attack animation
            anim.SetBool("Attack", true);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAtack), timeBetweenAttacks);
        }
    }

    private void ResetAtack() {
        anim.SetBool("Attack", false);
        alreadyAttacked = false;
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

    private void DestroyProjectile(Rigidbody rb) {
        Destroy(rb);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    private void Attack() {
        Vector3 position = transform.position;
        position.x += 2.0f;
        position.y += 4.0f;
        position.z += 1f;
        float distance = Vector3.Distance(position, player.position);
        Rigidbody rb = Instantiate(projectile, position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce((player.position - rb.position) * distance * force, ForceMode.Impulse);
        //rb.AddForce(transform.forward * distance * force, ForceMode.Impulse);
        rb.AddForce(transform.up * 1f, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "pickableObject") {
            health -= damageTaken;
            lifeBar.value = (100 * health / currentHealth) / 100 ;
        }
    }
}
