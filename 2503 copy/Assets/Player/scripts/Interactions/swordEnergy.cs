using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordEnergy : MonoBehaviour
{
    public Vector3 enemy;
    public FocusPosition focus;
    public Attacks player;
    // Start is called before the first frame update
    void Start()
    {
        focus = GameObject.Find("myAim").GetComponent<FocusPosition>();
        enemy = GameObject.Find(focus.hitColliders[0].name).transform.position;
        player = GameObject.Find("PlayerObj").GetComponent<Attacks>();
        Invoke("DestroyMe", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy, 48 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision){
       if(collision.gameObject.tag=="Enemy"){
            collision.gameObject.GetComponent<HP>().cHealth-=player.dmg;
            Destroy(gameObject);
        }
    }

    void DestroyMe(){
        Destroy(gameObject);
    }
}
