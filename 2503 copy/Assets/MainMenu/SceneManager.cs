using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public Canvas main,loadGame,newGame, settings;
    public RawImage selected;
    public SettingsSaved SS;
    public InputField Game_name;
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
    public Saved_Slot s1,s2,s3;
    public GameObject[] savings;
    // Start is called before the first frame update
    void Start()
    {
        string[] lines = System.IO.File.ReadAllLines ("Assets/Resources/Game_Save.txt");
        Debug.Log(lines.Length);
        string[] splitArray = (lines[0].Split(' '));
        s1 = new Saved_Slot(splitArray[0],int.Parse(splitArray[1]),int.Parse(splitArray[2]),int.Parse(splitArray[3]),int.Parse(splitArray[4]));
        splitArray = (lines[1].Split(' '));
        s2 = new Saved_Slot(splitArray[0],int.Parse(splitArray[1]),int.Parse(splitArray[2]),int.Parse(splitArray[3]),int.Parse(splitArray[4]));
        splitArray = (lines[2].Split(' '));
        s3 = new Saved_Slot(splitArray[0],int.Parse(splitArray[1]),int.Parse(splitArray[2]),int.Parse(splitArray[3]),int.Parse(splitArray[4]));
        savings[0].GetComponentInChildren<Text>().text = s1.Name;
        savings[1].GetComponentInChildren<Text>().text = s2.Name;
        savings[2].GetComponentInChildren<Text>().text = s3.Name;
       Debug.Log(s1.ult);
        Home();
    }
    void Awake(){
        Home();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NG(){
        main.enabled = false;
        newGame.enabled =true;
    }
    public void LG(){
        main.enabled = false;
        loadGame.enabled =true;
    }
    public void Settings(){
        main.enabled = false;
        settings.enabled =true;
    }
    public void Home(){
        settings.enabled = false;
        loadGame.enabled = false;
        newGame.enabled = false;
        main.enabled=true;
    }
    public void NGHover(){
        selected.enabled =false;
    }
    public void StartGame(){
        if(Game_name.text!="" || loadGame.enabled){ 
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameRoom", LoadSceneMode.Single);
        }else{
            Debug.Log("Insert Name");
        }
    }
    public void NewGame(){
        if(Game_name.text!=" "){
            SS.Load(Game_name.text,1, 0, 0,0);
        }
    }
    public void ContinueGame(int i){
        if(i==1){
            SS.Load(s1.Name,s1.wave, s1.dmg, s1.hp,s1.ult);
        }else if (i==2){
            SS.Load(s2.Name,s2.wave, s2.dmg, s2.hp,s2.ult);
        }else if (i==3){
            SS.Load(s3.Name,s3.wave, s3.dmg, s3.hp,s3.ult);
        }
    }
    public void Quit(){
        Application.Quit();
    }
    
}
