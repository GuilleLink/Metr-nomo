using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BPMSounds : MonoBehaviour
{
    public int bpm;
    public float soundsPerSecond;
    public int compassRythm;

    public InputField bpmInput;
    public InputField rythmInput;

    public AudioSource first;
    public AudioSource normal;
    public AudioSource corcheas;

    private float soundPerSecond;
    private float timeStamp;
    private int beats;
    private int count;

    public GameObject stopButton;
    public GameObject startButton;
    public GameObject corcheasOnButton;
    public GameObject corcheasOff;

    public bool corcheasOn;
    public bool playOn;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        beats = 0;
        bpm = 60;
        compassRythm = 4;
        corcheasOn = false;
        playOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        int InputRythmInt = int.Parse(rythmInput.text);
        compassRythm = InputRythmInt;
        int InputBpmInt = int.Parse(bpmInput.text);
        bpm = InputBpmInt;

        if (playOn) {
            soundPerSecond = (60.0f / bpm);
            timeStamp += Time.deltaTime;

            Debug.Log(timeStamp);
            Debug.Log(soundPerSecond);
            if (timeStamp >= soundPerSecond)
            {
                beats += 1;
                if (beats == compassRythm)
                {
                    beats = 0;
                    normal.Play();
                    count = 1;
                }
                else if (beats == 1)
                {
                    first.Play();
                    count = 1;
                }
                else
                {
                    normal.Play();
                    count = 1;
                }
                timeStamp = 0.0f;
            }
            else if (timeStamp >= (soundPerSecond / 2) && count == 1)
            {
                if (corcheasOn == true)
                {
                    corcheas.Play();
                    count = 0;
                }
            }
        }
        //Revision corcheas
        //Si hago click en corcheas
        if (corcheasOff.activeSelf)
        {
            //corcheasOff.SetActive(true);
            //corcheasOnButton.SetActive(false);
            corcheasOn = true;
        }
        else
        {
            //corcheasOnButton.SetActive(true);
            //corcheasOff.SetActive(false);
            corcheasOn = false;
        }
        //Revision On
        //Si hago click en play
        if (stopButton.activeSelf)
        {            
            //stopButton.SetActive(true);
            //startButton.SetActive(false);
            playOn = true;
        }
        else
        {
            //startButton.SetActive(true);
            //stopButton.SetActive(false);
            playOn = false;
        }
    }

}
