using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Letter : MonoBehaviour
{
    public GameObject letterDisplay;
    private AudioSource audioSource;
    private GameObject[] rocks; 
    public GameObject letter;
    private bool isOpen = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        rocks = GameObject.FindGameObjectsWithTag("entry");
    }

    void Update() {
        if (Gamepad.all.Count != 0) {
            if (Gamepad.current.buttonWest.isPressed && isOpen) {
                isOpen = false;
                letterDisplay.SetActive(false);
                audioSource.Play();
                foreach (GameObject rock in rocks) {
                    Destroy(rock);
                }
            }

            if (Gamepad.current.buttonWest.isPressed && letter != null) {
                letterDisplay.SetActive(true);
                isOpen = true;
            } 
        } else {
            if (Input.GetKeyDown(KeyCode.E) && isOpen) {
                isOpen = false;
                letterDisplay.SetActive(false);
                audioSource.Play();
                foreach (GameObject rock in rocks) {
                    Destroy(rock);
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && letter != null) {
                letterDisplay.SetActive(true);
                isOpen = true;
            } 
        }

    }

    public void OpenLetter() {
        if (this.gameObject.tag == "Letter") {
            letter = this.gameObject;
        }
    }

    public void CloseLetter() {
        if (this.gameObject.tag == "Letter") {
            letter = null;
        }
    }
}
