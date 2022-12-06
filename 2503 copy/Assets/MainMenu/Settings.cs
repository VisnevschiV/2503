using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Slider music,effects,voice,sensitivity;
    public AudioSource music_vol;
    public SettingsSaved SS;
    // Start is called before the first frame update
    void Start()
    {
      music.value=100;
      effects.value=100;
      voice.value=100;
      sensitivity.value=0.5f;
    }
  
    // Update is called once per frame
    void Update()
    {
      music_vol.volume = music.value/100;
    }
   
   public void SaveSettings(){
      SS.music = music.value;
      SS.effects = effects.value;
      SS.voice = voice.value;
      SS.sensitivity = sensitivity.value;
   }
}
