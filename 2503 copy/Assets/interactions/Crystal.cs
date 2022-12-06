using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float sightRange;
    public LayerMask whatIsPlayer;
    public bool playerInSightRange;

    public GameObject PressF;
    public HP player;
    // Start is called before the first frame update
    void Start()
    {
        PressF.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if(playerInSightRange){
            PressF.SetActive(true);
            if(Input.GetKey("f")){
                Heal();
            }            
        }else{
            PressF.SetActive(false);
        }
    }


    void Heal(){
        if(player.cHealth>90){
            player.cHealth=100;
        }else{
            player.cHealth = player.cHealth+10;
        }
        PressF.SetActive(false);
        Destroy(gameObject);
    }
}
