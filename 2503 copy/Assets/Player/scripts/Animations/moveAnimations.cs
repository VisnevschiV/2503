using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAnimations : MonoBehaviour
{
    public int hit=0;
    private float lastAttackTime;
    public Animator animator;
    public ThirdPersonMovement player;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w")|| Input.GetKey("s")||Input.GetKey("d")||Input.GetKey("a")){
            animator.SetBool("Run", true);
           
                
        }else{
            animator.SetBool("Run", false);
        }
        if(Input.GetKey("space")){
            animator.SetBool("Jump",true);
        }else{
            animator.SetBool("Jump",false);
        }
        if(Input.GetMouseButtonDown(0)){
            if(hit<3){
                lastAttackTime=Time.time;
                hit++;
                animator.SetInteger("Rhit", hit);
            }
        }else{stopAttack();}
        if(Input.GetMouseButtonDown(1)){
            hit++;
            Debug.Log(hit);
            animator.SetInteger("Lhit", hit);
          
        }
    }

    void stopAttack(){
        if(Time.time - lastAttackTime> 0.8){
            hit=0;
            animator.SetInteger("Rhit", hit);
            animator.SetInteger("Lhit", hit);
            lastAttackTime=Time.time;
        }
    }
    void stopDash(){
        animator.SetBool("dash", false);
    }
}
