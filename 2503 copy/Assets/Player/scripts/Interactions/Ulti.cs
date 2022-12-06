using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulti : MonoBehaviour
{
    public float timer=0f;
    public float growTime =6f;
    public float maxSize =200f;
    

    public bool ismaxSize = false;

    public Attacks player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj").GetComponent<Attacks>();
        StartCoroutine(Grow());
        Invoke("DestroyMe", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Grow(){
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(maxSize,maxSize,10);
        do{
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer/ growTime);
            timer+=Time.deltaTime;
            yield return null;
        }while(timer<growTime);
        ismaxSize =true;
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
