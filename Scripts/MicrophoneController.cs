using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.PyroParticles;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MicrophoneController : MonoBehaviour
{
    AudioSource source;
    float[] samples;
    float dbVal;
    float maxDbVal = -15f;
    GameObject[] fireZones;
    Vector3 scaleChange;
    public Slider dbSlider;
    // Start is called before the first frame update
    void Start() {
        fireZones = GameObject.FindGameObjectsWithTag("Fire");
        samples = new float[1024];
        source = GetComponent<AudioSource>();
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        #endif
        source.clip = Microphone.Start(null, true, 2, 48000);
        scaleChange = new Vector3(1.0f, 2.5f, 1.0f);
    }

    void Update() {
        getDB();
    }

    public void getDB() {
        source.Play();
        source.GetOutputData(samples, 0);
        float sum = 0f;
        for (int i = 0; i < 1024; i++) {
            sum += (samples[i] * samples[i]);
        }
        float rmsVal = Mathf.Sqrt(sum / 1024);
        dbVal = 20 * Mathf.Log10(rmsVal / 0.1f);
        Debug.Log(dbVal);
        if (dbVal < -50) {
            dbVal = -50;
        }
        
        if (dbVal > -15f) {
            dbVal = -15;
            foreach (GameObject fireZone in fireZones) {
                if (fireZone.activeSelf) {
                    fireZone.transform.localScale = scaleChange;
                    DigitalRuby.PyroParticles.FireLightScript.instance.SetIntensityModifier(7f);
                }
            }
        } else {
            foreach (GameObject fireZone in fireZones) {
                if (fireZone.activeSelf) {
                    fireZone.transform.localScale = new Vector3(1.0f, 1f, 1.0f);
                    DigitalRuby.PyroParticles.FireLightScript.instance.SetIntensityModifier(2f);
                }
            }
        }
        Debug.Log("dbVal% " + dbVal.ToString() + "%");
        dbSlider.value = dbVal;
    }
}

