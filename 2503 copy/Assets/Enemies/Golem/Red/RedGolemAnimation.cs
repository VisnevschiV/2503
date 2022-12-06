using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGolemAnimation : MonoBehaviour
{
    public moveTo golem;
    public Animator animator;
    private float lastAttack;
    public AudioSource roar;

    // Start is called before the first frame update
    void Awake(){
        animator.SetBool("Spawn",true);
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walk", golem.isMooving);
        if(golem.attackType>0){
            animator.SetBool("attack", true);
            if(!roar.isPlaying){
                roar.Play(0);
            }
        }else{
            roar.Pause();
            animator.SetBool("attack", false);
        }
    }






    IEnumerator wait(int seconds)
    {
        
        Debug.Log("i wait");
        yield return new WaitForSeconds(seconds);

    }


}
