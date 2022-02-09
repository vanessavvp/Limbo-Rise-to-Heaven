using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public Image blackScreen;
    public Image whiteScreen;
    public Image redScreen;
    public float fadeSpeed;
    public bool fadeToBlack, fadeFromBlack;
    public bool fadeToWhite, fadeFromWhite;
    private Coroutine currentFlashCoroutine = null;
    public Text healthText;
    public Image healthImage;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        if (fadeToBlack) {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f) {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack) {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f) {
                fadeFromBlack = false;
            }
        }

        if (fadeToWhite) {
            whiteScreen.color = new Color(whiteScreen.color.r, whiteScreen.color.g, whiteScreen.color.b, Mathf.MoveTowards(whiteScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (whiteScreen.color.a == 1f) {
                fadeToWhite = false;
            }
        }

        if (fadeFromWhite) {
            whiteScreen.color = new Color(whiteScreen.color.r, whiteScreen.color.g, whiteScreen.color.b, Mathf.MoveTowards(whiteScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (whiteScreen.color.a == 0f) {
                fadeFromWhite = false;
            }
        }

    }

    public void StartHurtingFlash(float secondsOfFlash) {
        float maxAlpha = 0.5f;
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);
        if (currentFlashCoroutine != null) {
            StopCoroutine(currentFlashCoroutine);
        }
        currentFlashCoroutine = StartCoroutine(Flash(secondsOfFlash, .5f));

    }

    IEnumerator Flash(float secondsOfFlash, float maxAlpha) {
        float flashInDuration = secondsOfFlash/2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime) {
            Color currentcolor = redScreen.color;
            currentcolor.a = Mathf.Lerp(0, maxAlpha, t / flashInDuration);
            redScreen.color = currentcolor;
            yield return null;
        }
        float flashOutDuration = secondsOfFlash/2;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime) {
            Color currentcolor = redScreen.color;
            currentcolor.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
            redScreen.color = currentcolor;
            yield return null;
        }

        redScreen.color = new Color(255, 0, 0, 0f);
        currentFlashCoroutine = null;
    }
}
