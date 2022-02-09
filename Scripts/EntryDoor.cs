using System; 
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Windows.Speech;
using UnityEngine.InputSystem;

public class EntryDoor : MonoBehaviour
{
    private Animator animator;
    private float distance;
    private bool isOpen;
    public GameObject player;

    void Start()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        // Puede ser Gamepad.all[0]
        //if ((Gamepad.all[1].buttonWest.isPressed) && (!isOpen) && (distance <= 2f))
        if (Gamepad.all.Count != 0) {
            if ((Gamepad.current.buttonWest.isPressed) && (!isOpen) && (distance <= 2f))
            {
                Debug.Log("DOOR OPEN");
                animator.SetBool("character_nearby", true);
                isOpen = true;
            } 
            // Esto es por si se quiere implementar que se cierre al pulsar cuadrado también
            else if ((Gamepad.current.buttonWest.isPressed)  && (isOpen)) {
                animator.SetBool("character_nearby", false);
                isOpen = false;
            }
        }
    }
}

/*public class EntryDoor : MonoBehaviour
{
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    private Animator animator;
    private float distance;
    private bool isOpen;
    public GameObject player;

    void Start()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        animator = GetComponent<Animator>();
        isOpen = false;

        actions.Add("Open", OpenDoor);
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if ((distance <= 2f) && (!isOpen) && (keywordRecognizer == null))
        {
            Debug.Log("You are close enough to say OPEN");
            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
            keywordRecognizer.Start();
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", speech.text, speech.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", speech.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", speech.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
        actions[speech.text].Invoke();
    }

    private void OpenDoor()
    {
        
        animator.SetBool("character_nearby", true);
        isOpen = true;
        Debug.Log("Door open");
        OnDestroy();
    }

    void OnDestroy()
    {
        if (keywordRecognizer  != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}*/


