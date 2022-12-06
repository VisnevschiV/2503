using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardAnimations : MonoBehaviour
{
    public moveTo wizzard;
    public Animator animator;
    public AudioSource spell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walk",wizzard.isMooving);
        if(wizzard.attackType>0){
            animator.SetBool("Projectile", true);
            if(!spell.isPlaying){
                spell.Play(0);
            }
        }else{
            animator.SetBool("Projectile",false);
        }
    }
}
