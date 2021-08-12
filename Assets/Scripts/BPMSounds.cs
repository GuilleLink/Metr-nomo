using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BPMSounds : MonoBehaviour
{
    private int Seed;

    public Button generateSeedButton;
    public Button generateScale;

    public Text seedText;
    public Text scaleText;

    public int bpm;
    public float soundsPerSecond;
    public int compassRythm;

    public InputField bpmInput;
    public InputField rythmInput;
    public InputField noteInput;

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

    List<String> notasHalfFull = new List<String>();

    // Start is called before the first frame update
    void Start()
    {
        Button seedbtn = generateSeedButton.GetComponent<Button>();
        seedbtn.onClick.AddListener(GenerateSeed);        
        Button scalebtn = generateScale.GetComponent<Button>();
        scalebtn.onClick.AddListener(GenerateScale);
        count = 0;
        beats = 0;
        bpm = 60;
        compassRythm = 4;
        corcheasOn = false;
        playOn = false;
        Seed = 123;
        //Definicion de las notas        
        notasHalfFull.Add("Ab");
        notasHalfFull.Add("A");
        notasHalfFull.Add("Bb");
        notasHalfFull.Add("B");
        notasHalfFull.Add("C");
        notasHalfFull.Add("Db");
        notasHalfFull.Add("D");
        notasHalfFull.Add("Eb");
        notasHalfFull.Add("E");
        notasHalfFull.Add("F");
        notasHalfFull.Add("Gb");
        notasHalfFull.Add("G");
    }

    public void GenerateSeed()
    {
        Seed = UnityEngine.Random.Range(0, 500);
        seedText = seedText.GetComponent<Text>();
        seedText.text = ("" + Seed);
    }

    public void RandomNumber()
    {
        UnityEngine.Random.seed = Seed;
        Debug.Log(UnityEngine.Random.value);
    }

    //Se genera la escla del acorde, de momento no permite el tema de sostenidos, solamente se aceptan bemoles y naturales de las teclas correspondientes.
    void GenerateScale()
    {
        String noteInputTxt = (noteInput.text);
        List<String> escala = new List<String>();        
        scaleText = scaleText.GetComponent<Text>();
        int index = 0;
        if(notasHalfFull.Contains(noteInputTxt))
        {
            index = notasHalfFull.IndexOf(noteInputTxt);
        }
        else
        {

        }
        escala.Add(noteInputTxt);
        for(int i = 1; i < 7; i++)
        {
            if(i==3)
            {
                index += 1;
            }
            else
            {
                index += 2;
            }
            escala.Add(notasHalfFull[index%notasHalfFull.Count]);
        }
        String scaleNotes = "";
        int counter = 0;
        foreach(String notas in escala)
        {
            counter++;
            if (escala.Count > counter) 
            {
                scaleNotes += notas.ToString() + ", ";
            }
            else
            {
                scaleNotes += notas.ToString();
            }
        }
        scaleText.text = (""+scaleNotes);
    }

    void PlayBeats()
    {
        int InputRythmInt = int.Parse(rythmInput.text);
        compassRythm = InputRythmInt;
        int InputBpmInt = int.Parse(bpmInput.text);
        bpm = InputBpmInt;
    }

    // Update is called once per frame
    void Update()
    {
        if (playOn) {
            PlayBeats();
            soundPerSecond = (60.0f / bpm);
            timeStamp += Time.deltaTime;
            RandomNumber();
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
