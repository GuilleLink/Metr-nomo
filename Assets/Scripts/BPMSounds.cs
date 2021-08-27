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

    public Text rythmText;
    public Text seedText;
    public Text scaleText;

    public int bpm;
    public float soundsPerSecond;
    public int compassRythm;

    public InputField bpmInput;
    public InputField noteInput;
    public InputField seedInput;

    public AudioSource metricaSonido;
    public AudioSource ritmoSonido;
    public AudioSource fillerSonido;

    private float soundPerSecond;
    private float timeStamp;
    private int beats;
    private int count;

    public GameObject stopButton;
    public GameObject startButton;

    public bool playOn;

    List<String> notasHalfFull = new List<String>();
    List<int> rythms = new List<int>();
    List<int> rythmsBeat = new List<int>();
    List<int> metrica = new List<int>();
    List<int> filler = new List<int>();

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
        playOn = false;
        Seed = 123;
        //Seed
        seedText = seedText.GetComponent<Text>();
        seedText.text = ("" + Seed);
        //Definicion de los ritmos
        rythms.Add(3); // 3/4
        rythms.Add(4); // 4/4
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
        //Seeds solo de 0 a 9999
        //Seed = UnityEngine.Random.Range(0, 10000);        
        seedText = seedText.GetComponent<Text>();
        seedText.text = ("" + Seed);
        UnityEngine.Random.seed = int.Parse(seedText.text);
        compassRythm = rythms[UnityEngine.Random.Range(0, rythms.Count)];
        rythmText = rythmText.GetComponent<Text>();
        rythmText.text = ("" + compassRythm);
        beats = 0;
        count = 0;
        rythmsBeat.Clear();
        metrica.Clear();
        filler.Clear();
        GenerateFiller();
    }

    void GenerateFiller()
    {

        int option = UnityEngine.Random.RandomRange(0, 3);
        //negras
        //[1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0] 4/4
        if (option == 0)
        {
            for(int i = 0; i < compassRythm; i++)
            {
                rythmsBeat.Add(1);
                for (int j = 0; j < 3; j++)
                {
                    rythmsBeat.Add(0);
                }
            }
        }
        //corcheas
        //[1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0] 4/4
        else if (option == 1)
        {
            for (int i = 0; i < compassRythm*2; i++)
            {
                rythmsBeat.Add(1);
                rythmsBeat.Add(0);
            }
        }
        //semicorcheas
        //[1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0] 4/4
        else
        {
            for (int i = 0; i < compassRythm*4; i++)
            {
                rythmsBeat.Add(1);
            }
        }


        //Se llena como las negras porque se marca cada tiempo
        for (int i = 0; i < compassRythm; i++)
        {
            metrica.Add(1);
            for (int j = 0; j < 3; j++)
            {
                metrica.Add(0);
            }
        }

        for (int i =0; i< rythmsBeat.Count; i++)
        {
            if (rythmsBeat[i] == 0)
            {
                if(UnityEngine.Random.RandomRange(0, 2) == 1)
                {
                    filler.Add(1);
                } else
                {
                    filler.Add(0);
                }
            } else if (rythmsBeat[i] == 1)
            {
                filler.Add(0);
            }
        }
    }

    //Se genera la escla del acorde, de momento no permite el tema de sostenidos, solamente se aceptan bemoles y naturales de las teclas correspondientes.
    void GenerateScale()
    {
        String noteInputTxt = (noteInput.text);
        List<String> escala = new List<String>();
        scaleText = scaleText.GetComponent<Text>();
        int index = 0;
        if (notasHalfFull.Contains(noteInputTxt))
        {
            index = notasHalfFull.IndexOf(noteInputTxt);
        }
        else
        {

        }
        escala.Add(noteInputTxt);
        for (int i = 1; i < 7; i++)
        {
            if (i == 3)
            {
                index += 1;
            }
            else
            {
                index += 2;
            }
            escala.Add(notasHalfFull[index % notasHalfFull.Count]);
        }
        String scaleNotes = "";
        int counter = 0;
        foreach (String notas in escala)
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
        calcularAcordes(escala);
        scaleText.text = ("" + scaleNotes);
    }

    String calcularCalidad(List<String> notasAcordes)
    {
        int distance1 = 0;
        int distance2 = 0;
        List<int> posiciones = new List<int>();
        foreach(String nota in notasAcordes)
        {
            posiciones.Add(notasHalfFull.IndexOf(nota));
        }
        if(posiciones[0]> posiciones[1])
        {
            posiciones[1] = posiciones[1] + notasHalfFull.Count;
        }
        if (posiciones[1] > posiciones[2])
        {
            posiciones[2] = posiciones[2] + notasHalfFull.Count;
        }
        distance1 = posiciones[1] - posiciones[0];
        distance2 = posiciones[2] - posiciones[1];
        if (distance1 == 4 && distance2 == 4)
        {
            return "Aumentado";
        }
        else if (distance1==3 && distance2==3)
        {
            return "Disminuido";
        }
        else if(distance1 != distance2)
        {
            if (distance1 == 3)
            {
                return "Menor";
            }
            else if (distance1 == 4)
            {
                return "Mayor";
            }
        }
        return "";
    }

    void calcularAcordes(List<String> escala)
    {
        for (int i = 0; i < 7; i++)
        {
            List<String> notasAcordes = new List<String>();
            notasAcordes.Add(escala[i]);
            notasAcordes.Add(escala[(i + 2) % escala.Count]);
            notasAcordes.Add(escala[(i + 4) % escala.Count]);
            String calidadAcorde;
            calidadAcorde = calcularCalidad(notasAcordes);
            Debug.Log("El acorde de " + notasAcordes[0] + ": " + notasAcordes[0] + " " + notasAcordes[1] + " " + notasAcordes[2] + " -------- Calidad de acorde: " + calidadAcorde);
        }
    }

    void PlayBeats()
    {
        int InputBpmInt = int.Parse(bpmInput.text);
        bpm = InputBpmInt;
    }

    // Update is called once per frame
    void Update()
    {
        if (playOn) {
            PlayBeats();
            soundPerSecond = (60.0f / (bpm*4));
            timeStamp += Time.deltaTime;
            if (timeStamp >= soundPerSecond)
            {
                beats += 1;
                if (beats == compassRythm*4)
                {
                    beats = 0;
                }
                if (filler[beats] == 1)
                {
                    fillerSonido.Play();
                }
                if (metrica[beats] == 1)
                {
                    metricaSonido.Play();
                }
                if (rythmsBeat[beats] == 1)
                {
                    ritmoSonido.Play();
                }
                timeStamp = 0.0f;
            }
        }
        else
        {
            beats = 0;
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
