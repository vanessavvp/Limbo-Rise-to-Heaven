using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class OptionsController : MonoBehaviour
{
    public GameObject _menu_panel = null;
    public GameObject _inventary = null;
    const string SCENE = "MainMenu";
    private Animator _animator;
    public bool _menu_is_enable = false;
    
    void Start() {
        _animator = GameObject.Find("Panel").GetComponent<Animator>(); 
    }

    void Update()
    {
        // Menu
        if (Input.GetKeyDown(KeyCode.JoystickButton7)) {
            if (_menu_panel.activeSelf) {
                _menu_panel.SetActive(false);
                _menu_is_enable = false;
            } else {
                _inventary.SetActive(false);
                _menu_panel.SetActive(true);
                _menu_is_enable = true;
            }
        }

        // Inventary
        if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown("i")) {
            if (_inventary.activeSelf) {
                _inventary.SetActive(false);
                _menu_is_enable = false;
            } else {
                _menu_panel.SetActive(false);
                _inventary.SetActive(true);
                _menu_is_enable = true;
            }
        }
    }
    
    public void MainMenuScene() {
        _animator.Play("FadeOut-2");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SCENE);
    }
}
