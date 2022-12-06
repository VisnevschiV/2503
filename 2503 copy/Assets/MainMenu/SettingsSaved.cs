using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaved : MonoBehaviour
{
    public float music,effects,voice,sensitivity;
     public struct Saved_Slot
    {
        public string Name;
        public int wave, dmg, hp, ult;

        public Saved_Slot(string name, int w, int dmg, int hp, int ult){
            Name=name;
            wave=w;
            this.dmg =dmg;
            this.hp=hp;
            this.ult = ult;
        }
    }
    public Saved_Slot slot;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
      DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewSceneManage(){

    }
    public void Load(string name, int w, int dmg, int hp, int ult){
       slot = new Saved_Slot(name, w, dmg, hp, ult);
    }
}
