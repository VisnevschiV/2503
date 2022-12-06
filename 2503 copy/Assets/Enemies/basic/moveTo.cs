using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrol
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //attack 
    public float attackTime;
    public bool canAttack;
    private float lastTimeAttack;
    public bool attacked;
    public int attackType=0;

    //states

    public float attackRange, sightRange;
    public bool playerInSightRange,playerInAttackRange;
    public gameMaster GM;
    public bool AllowedToAttack=true;

    //for animation
    public bool isMooving=false;

    public AudioSource myVoice;
    void Start(){
        GM = GameObject.Find("GameMaster").GetComponent<gameMaster>();
        lastTimeAttack=0;
        canAttack=true;
    }
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerObj").transform;
    }
    void Update(){
        myVoice.volume = GM.effects_slider.value/100;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        attackType=0;
        if (playerInSightRange && !playerInAttackRange && AllowedToAttack) {
            GoToPlayer();
        }else
        if (!playerInSightRange && !playerInAttackRange){
            GoAround();
        }else
        if (playerInAttackRange){
            if(canAttack){
                Attack();
            }
        }
    }

    void GoToPlayer(){
        isMooving=true;
        agent.SetDestination(player.position);
    }
    void GoTO(Transform position){
        isMooving=true;
        agent.SetDestination(position.position); 
    }
    //Patrol
    void GoAround(){
        isMooving=true;
        if (!walkPointSet) {
            SearchWalkPoint();
        }
        
        if (walkPointSet){
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f){
            isMooving=false;
            Debug.Log("im here");
            wait(2);
            walkPointSet = false;
            
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        Debug.Log("new point");
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        
        walkPointSet = true;
    }
    
    //Attack
    private void Attack(){
        isMooving=false;
        //stop from mooving towards player
        agent.SetDestination(transform.position);
        attackType=1;
        canAttack = false;
        Invoke("stopAttack", 1);
        Invoke("canAttackF",attackTime);
    }    
    
    
    void stopAttack(){
        attackType=0;
    }
    void canAttackF(){
        canAttack =true;
    }
     IEnumerator wait(int seconds)
    {
        
        Debug.Log("i wait");
        yield return new WaitForSeconds(seconds);

    }
}