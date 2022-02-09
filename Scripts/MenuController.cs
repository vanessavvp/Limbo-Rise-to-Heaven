using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Constants 
    // - VOLUMEN: Music of the game
    // - BRIGHTNESS: Brightness of the game
    // - LEVEL: Name of the saved level
    private const string VOLUME = "20";
    private const string BRIGHTNESS = "80";
    private const string LEVEL = "saved_level";

    // Elements to be defined in the inspector window
    // - _text_volume: Volumen of the game in numbers
    // - _text_brightness: Brightness of the game in numbers
    // - _slider_volume: Bar to modify the volume of the game
    // - _slider_brightness: Bar to modify the brightness of the game
    // - _audio: AudioSource with the game music
    // - _not_save_game_panel: Panel showing if there is no saved game
    // - _new_game_level: Name of the next scene
    public Text _text_volume = null; 
    public Text _text_brightness = null;
    public Slider _slider_volume = null;
    public Slider _slider_brightness = null;
    public AudioSource _audio = null;
    public GameObject _not_save_game_panel = null;
    public string _new_game_level;
    
    // Variables 
    // - _level_to_load: Name of the next level to load
    private string _level_to_load;
    private Animator _animator;

    void Start() {
        _animator = GameObject.Find("Canvas").GetComponent<Animator>(); 
    }

    // This method loads the scene we passed
    public void NewGame() {
        _animator.Play("FadeOut");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_new_game_level);
    }

    // This method looks for the scene in the preference file and if it exits, load the scene
    public void LoadGamme() {
        if (PlayerPrefs.HasKey(LEVEL)) {
            _level_to_load = PlayerPrefs.GetString(LEVEL);
            SceneManager.LoadScene(_level_to_load);
        } else {
            _not_save_game_panel.SetActive(true);
        }
    }

    // This method close the game
    public void ExitGame() {
        Application.Quit();
    }

    // This method modify the volumen when the bar move
    public void SetVolume() {
       _text_volume.text = _slider_volume.value.ToString("0");
    }

    // This method modify the brightness when the bar move 
    public void SetBrightness() {
       _text_brightness.text = _slider_brightness.value.ToString("0");
    }

    // This method restore the default value in the graphics window
    public void DefaultGraphics() {
        _text_brightness.text = BRIGHTNESS;
        _slider_brightness.value = float.Parse(BRIGHTNESS);
    }

    // This method restore the default value in the sound window
    public void DefaultSound() {
        _text_volume.text = VOLUME;
        _slider_volume.value = float.Parse(VOLUME);
        _audio.volume = float.Parse(VOLUME) / 100;
    }

    // This method save the new volume of the game music
    public void ApplyVolume() {
        _audio.volume = _slider_volume.value / 100;
    }

    // This method saves all the changes made in the options window
    public void ApplyChanges() {
        ApplyVolume();
    }
}
