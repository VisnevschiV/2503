using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class gameMaster : MonoBehaviour
{

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
    // here are the main level controlls
    public int waveLvl;
    public Text waveCount;
    private int golems_hidden,wizzards_hidden;
    public int wizzards_alive,golems_alive;
    
    public List<Transform> spawnPositions;
    public SettingsSaved SS;
// here are the variables needed for AI to predict player's behaivour
    public GameObject[] predictions;
    public bool[] predictions_ocupied;
    public int golems_in_sight,wizzard_in_sight;

// here are the canvas manipulation variables

    public GameObject pause_menu, in_Game_UI, upgrade_menu, save_menu;
    private bool pause;
    public AudioSource Music;
    public Slider music_slider, effects_slider;
    public Saved_Slot s1,s2,s3;
    public GameObject[] savings;
    

// here are all the objects we will use
    public GameObject Golem;
    public GameObject Wizzard;
    public GameObject Player;
    public GameObject DeathBG;

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

        golems_in_sight =0;
        wizzard_in_sight=0;
    }
    void Awake(){
        SS = GameObject.Find("SettingsSaver").GetComponent<SettingsSaved>();
        waveLvl=SS.slot.wave;
        music_slider.value=SS.music;
        effects_slider.value=SS.effects;
        Debug.Log(SS.music);
        waveCount.text = waveLvl.ToString();
        pause_menu.SetActive(false);
        upgrade_menu.SetActive(false);
        save_menu.SetActive(false);
        in_Game_UI.SetActive(true);
        pause=false;
        FirstRoom();
    }
    // Update is called once per frame
    void Update()
    {
        Music.volume = music_slider.value/100;
        if(golems_hidden>0){
           CreateGolem();
        }
        if(wizzards_alive==0 && wizzards_hidden>0){
            CreateWizzard();
        }
        if(Player.GetComponent<HP>().cHealth==0){
            EndGame();
        }
        if(golems_hidden==0 && wizzards_hidden==0 && golems_alive==0 && wizzards_alive==0){
            WaveClear();
        }
        if(Input.GetKeyDown("1")){
            if(pause){
                UnPause();
            }else{
                Pause();
            }
        }
    }

    
    void CreateGolem(){
        int location = Random.Range(0,4);
        Instantiate(Golem, spawnPositions[location].position,Quaternion.Euler(-119,-90,90));
        golems_hidden--;
    }
    void CreateWizzard(){
        int location = Random.Range(0,4);
        Instantiate(Wizzard, spawnPositions[location].position,Quaternion.Euler(-119,-90,90));
        wizzards_hidden--;
    }
    public void FirstRoom(){
        Debug.Log("button Pressed");
        DeathBG.SetActive(false);
        Player.GetComponent<HP>().cHealth=100;
        Cursor.lockState = CursorLockMode.Locked;
        golems_alive=0;
        golems_hidden = 4*waveLvl;
        wizzards_alive = 0;
        wizzards_hidden = 2*waveLvl;
        CreateGolem();
        CreateWizzard();
        Time.timeScale =1f;
    }
    void EndGame(){
        Cursor.lockState = CursorLockMode.Confined;
        DeathBG.SetActive(true);
        Time.timeScale =0.1f;
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach( var clone in clones ){
            Destroy(clone);
        }
    }

    void Pause(){
        Time.timeScale =0f;
        Cursor.lockState = CursorLockMode.Confined;
        in_Game_UI.SetActive(false);
        save_menu.SetActive(false);
        pause_menu.SetActive(true);
        pause=true;
    }
    public void UnPause(){
        Time.timeScale =1f;
        Cursor.lockState = CursorLockMode.Locked;
        in_Game_UI.SetActive(true);
        pause_menu.SetActive(false);
         save_menu.SetActive(false);
        pause=false;

    }
    void WaveClear(){
        waveLvl++;
        waveCount.text = waveLvl.ToString();
        Time.timeScale=0f;
        golems_hidden=waveLvl*2;
        wizzards_hidden= waveLvl*2;
        Upgrade();
    }
    void Upgrade(){
        in_Game_UI.SetActive(false);
        pause_menu.SetActive(false);
        save_menu.SetActive(false);
        upgrade_menu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void UpgradeComplete(){
        in_Game_UI.SetActive(true);
        pause_menu.SetActive(false);
         save_menu.SetActive(false);
        upgrade_menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale =1f;
    }

    public void Main_Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
    }

    public void Save_Menu(){
        in_Game_UI.SetActive(false);
        pause_menu.SetActive(false);
        save_menu.SetActive(true);
        upgrade_menu.SetActive(false);
    }
    public void Save_Game(int i){
        SS.slot.wave =waveLvl;
        Debug.Log("HI:"+ SS.slot.Name +" you are at the  wave nr "+ SS.slot.wave +" you have the next stats: " + SS.slot.dmg+SS.slot.hp+SS.slot.ult);
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/Game_Save.txt");
        string line = SS.slot.Name +" "+SS.slot.wave+" "+SS.slot.dmg+" "+ SS.slot.hp+" "+SS.slot.ult;
        lines[i] = line;
        Debug.Log(lines[i]);
        System.IO.File.WriteAllLines("Assets/Resources/Game_Save.txt",lines);
    }
    
}
