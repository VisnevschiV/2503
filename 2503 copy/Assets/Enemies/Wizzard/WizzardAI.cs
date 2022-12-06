using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardAI : Enemy
{
    public gameMaster GM;
    public moveTo wizzard;
    public GameObject projectile;
    public Transform projectileSpawnPosition;
    public HP hp;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameMaster").GetComponent<gameMaster>();
        health =100;
        hp.Health = health;
        hp.cHealth = health;
        GM.wizzards_alive++;
    }

    // Update is called once per frame
    void Update()
    {
        if(wizzard.attackType>0){
            Instantiate(projectile, projectileSpawnPosition.position, Quaternion.Euler(-119,-90,90));
        }
        if(hp.cHealth<1){
            GM.wizzards_alive--;
            Destroy(gameObject);
        }
    }
}
