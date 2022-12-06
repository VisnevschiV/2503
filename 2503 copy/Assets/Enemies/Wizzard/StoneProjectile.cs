using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Transform player;
    private bool  animationFinished;
    void Start()
    {
        animationFinished = false;
        animator.SetBool("Spawn", true);
        player = GameObject.Find("PlayerObj").transform;
        Invoke("GO", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(animationFinished){
            transform.position = Vector3.MoveTowards(transform.position, player.position, 20 * Time.deltaTime);
        }
    }
    void GO(){
        animationFinished = true;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerObj" || other.gameObject.name == "Cube"){
             Destroy(gameObject);
        }
    }
}
