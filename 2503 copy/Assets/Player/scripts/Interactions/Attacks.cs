using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public int hp=100;
    public int dmg=100;
    public GameObject player;
    public GameObject hit1pref;
    public GameObject hit3pref;
    public Transform projectileSpawnPosition;
    public Animator playerAnimator;
    public HP statusBar;
    private bool hit1Sent=false, hit2Sent = false, hit3Sent=false;
    public bool ultAvailable=false;
    public SettingsSaved SS;
    void Start()
    {
        statusBar.Health=hp;
        statusBar.cHealth=hp;
    }
    void Awake(){
        SS = GameObject.Find("SettingsSaver").GetComponent<SettingsSaved>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("hit1")){
            if(playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.7 && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime< 0.8 && !hit1Sent){
                Instantiate(hit1pref, projectileSpawnPosition.position, Quaternion.Euler(-119,-90,90));
                hit1Sent = true;
            }
        }else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("hit2")){
            if(playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.7 && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime< 0.8 && !hit2Sent){
                Instantiate(hit1pref, projectileSpawnPosition.position, Quaternion.Euler(-119,-90,90));
                hit2Sent = true;
            }
        }else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("hit3")&&ultAvailable){
            if(playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.7 && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime< 0.8 && !hit3Sent){
                Instantiate(hit3pref, player.transform.position, Quaternion.Euler(90,-90,90));
                hit3Sent = true;
            }
        }
        else{
            hit1Sent = false;
            hit2Sent = false;
            hit3Sent =false;
        }
        
    }
    private void OnTriggerEnter(Collider other){
        statusBar.cHealth-=10;
    }
    public void LoadStats(){
        hp = hp+(20*SS.slot.hp);
        dmg = dmg+(10*SS.slot.dmg);
        if(SS.slot.ult>0){
            UltUpgrade();
        }
    }
    public void HealthUpgrade(){
        hp=hp+20;
        statusBar.Health = hp;
        statusBar.cHealth =hp;
        statusBar.slider.maxValue = hp;
        SS.slot.hp++;
    }
    public void DmgUpgrade(){
        dmg=dmg+10;
        SS.slot.dmg++;
    }
    public void UltUpgrade(){
        if(!ultAvailable){
            ultAvailable=true;
        }
        SS.slot.ult++;
    }
}
