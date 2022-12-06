using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGolemAI : Enemy
{
    public moveTo golem;
    public gameMaster GM;
    public moveTo mT;
    public HP hp;
    public bool apple;
   
    void Start(){
        GM = GameObject.Find("GameMaster").GetComponent<gameMaster>();
        apple =false;
        health = 100;
        hp.Health = health;
        hp.cHealth = health;
        GM.golems_alive++;
        mT.AllowedToAttack = false;
    }
    // Update is called once per frame
    void Update()
    {   
       // if(!mT.playerInSightRange && !mT.playerInAttackRange && GM.golems_in_sight>2){
       //     GoTO(GM.GetFreePos());
       // }else 
        if(mT.playerInSightRange){
            if(!apple && GM.golems_in_sight<2){
               apple=true;
               mT.AllowedToAttack = apple;
               GM.golems_in_sight++;
            }
        }else{
            if(apple){
                apple=false;
                mT.AllowedToAttack =apple;
                GM.golems_in_sight--;
            }
        }
        if(hp.cHealth<1){
            GM.golems_alive--;
            Destroy(gameObject);
        }
    }

    void GoTO(Transform position){
        mT.isMooving=true;
        while (!mT.playerInAttackRange)
        {
            //agent.SetDestination(position.position); 
        }
    } 
}
