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
    public Text acordesText;

    public int bpm;
    public float soundsPerSecond;
    public int compassRythm;

    public InputField bpmInput;
    public InputField noteInput;
    public InputField seedInput;

    public AudioSource metricaSonido;
    public AudioSource ritmoSonido;
    public AudioSource fillerSonido;

    //Piano Sounds
    public AudioSource Ab4Sonido;
    public AudioSource A4Sonido;
    public AudioSource Bb4Sonido;
    public AudioSource B4Sonido;
    public AudioSource C4Sonido;
    public AudioSource Db4Sonido;
    public AudioSource D4Sonido;
    public AudioSource Eb4Sonido;
    public AudioSource E4Sonido;
    public AudioSource F4Sonido;
    public AudioSource Gb4Sonido;
    public AudioSource G4Sonido;

    private float soundPerSecond;
    private float timeStamp;
    private int beats;

    //Acordes
    private int nowPlayingChord;

    public GameObject stopButton;
    public GameObject startButton;

    public bool playOn;

    List<String> notasHalfFull = new List<String>();
    List<AudioSource> SonidosNotas = new List<AudioSource>();

    List<String> notasAcordes = new List<String>();
    List<int> notasAcordesPlay = new List<int>();
    List<float> lengthChords = new List<float>();

    List<int> rythms = new List<int>();

    List<int> rythmsBeat = new List<int>();
    List<int> metrica = new List<int>();
    List<int> filler = new List<int>();

    List<int> calidades = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        Button seedbtn = generateSeedButton.GetComponent<Button>();
        seedbtn.onClick.AddListener(GenerateSeed);
        Button scalebtn = generateScale.GetComponent<Button>();
        scalebtn.onClick.AddListener(GenerateScale);
        beats = 0;
        nowPlayingChord = 0;
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
        SonidosNotas.Add(Ab4Sonido);
        notasHalfFull.Add("A");
        SonidosNotas.Add(A4Sonido);
        notasHalfFull.Add("Bb");
        SonidosNotas.Add(Bb4Sonido);
        notasHalfFull.Add("B");
        SonidosNotas.Add(B4Sonido);
        notasHalfFull.Add("C");
        SonidosNotas.Add(C4Sonido);
        notasHalfFull.Add("Db");
        SonidosNotas.Add(Db4Sonido);
        notasHalfFull.Add("D");
        SonidosNotas.Add(D4Sonido);
        notasHalfFull.Add("Eb");
        SonidosNotas.Add(Eb4Sonido);
        notasHalfFull.Add("E");
        SonidosNotas.Add(E4Sonido);
        notasHalfFull.Add("F");
        SonidosNotas.Add(F4Sonido);
        notasHalfFull.Add("Gb");
        SonidosNotas.Add(Gb4Sonido);
        notasHalfFull.Add("G");
        SonidosNotas.Add(G4Sonido);
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
        rythmsBeat.Clear();
        metrica.Clear();
        filler.Clear();
        GenerateFiller();
    }

    void GenerateFiller()
    {
        //Elige entre que sean negras, corcheas o semicorcheas
        int option = UnityEngine.Random.RandomRange(0, 3);
        //negras
        //[1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0] 4/4
        int optionAgrupacion = UnityEngine.Random.RandomRange(3, 5);
        //3 o 4 
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
        //[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1] 4/4
        else
        {
            for (int i = 0; i < compassRythm*4; i++)
            {
                rythmsBeat.Add(1);
            }
        }

        //Es como el metronomo
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
        List<int> NotesForChords = new List<int>();
        String noteInputTxt = (noteInput.text);
        List<String> escala = new List<String>();
        scaleText = scaleText.GetComponent<Text>();
        int index = 0;
        if (notasHalfFull.Contains(noteInputTxt))
        {
            //Toma la nota que se encuentre en el HalfIndex
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
        //Jalar los acordes correspondientes para cada escala
        //Tonica: 1,3,6
        //Sub: 2,4
        //Dominante: 5,7
        calcularAcordes(escala);
        calcularDuracionAcordes();
        calcularAcordesTocados();
        scaleText.text = ("" + scaleNotes);
    }

    void GenerateProgression(List<String> escala)
    {
        
    }

    //1. Dividir los 8 compases en según si sonará 4 compases, 2 compases, 1 compas o medio compas (minima subdivision)
    //
    //   8 Compases 8
    //                                  8               8                   8
    //   Sub dividir (random) Ej. 1: 4,2,2  Ej. 2: 2,1,1,2,2   Ej. 3: 1/2, 2, 1/2, 2, 1, 2
    //                    [1      ,2  ,3  ]        
    //                    [1,1,1,1,2,2,3,3]  [1,1,2,3,4,4,5,5]
    //   Para subdividir "Reglas" 4 probabilidad = 25%  (NO se subdivide)      Random(1,4)
    //                            2 probabilidad = 33%  (Se subdivide 1 vez)   Random(1,3)
    //                            1 probabilidad = 50%  (Se subdivide 2 veces) Random(1,2)
    //                            1/2 probabilidad = 100% (Se subdivide 3 veces)
    //
    //2. Asignar el array segun cuantos compases se estara ocupando por acorde
    //   
    //   [4,2,2]
    //   [1,1,1,1,2,2,3,3]
    //
    //3. Random entre el tipo de acorde que saldra (con favoritismos 2/3)
    //   
    //   Tonica, Sub y Dom
    //   2/5     1/5    2/5   Random(1,5)  1,2 Tonica, 3 Sub, 4,5 Dom
    //  
    //   1/4     2/4   1/4   
    //
    //   Current ChordType = 1   if(currentChordType ==1 || currentChordType ==3) {Random(1,4)} else{Random(1,5)}
    //   Duracion de Acorde
    //   [2,1,1,2,2]
    //   Tipos de Acorde
    //   [1,2,1,2,3]
    //   
    //
    //4. Random entre CUAL acorde se usara de ese tipo
    //    
    //   [6,4,1,4,5]
    //

    void calcularAcordesTocados()
    {
        String currentChord = "SubDominante";
        for (int i = 0; i < lengthChords.Count; i++)
        {   
            if(currentChord == "Tonica") {
                if (UnityEngine.Random.Range(0, 4) < 3)
                {
                    for (int j = 0; j<3; j++)
                    {
                        notasAcordesPlay.Add(j);
                    }
                }
            }else if (currentChord == "SubDominante")
            {
                int rand = UnityEngine.Random.Range(0, 5);
                //Tonica
                if (rand < 2)
                {
                    //La primera Tonica
                    if (UnityEngine.Random.Range(0, 2) == 1)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            notasAcordesPlay.Add(j);
                        }
                    }
                    
                    else
                    {

                    }

                    
                } 
                //Subdominante
                else if (rand == 2)
                {

                }
                //Dominante
                else
                {

                }
            }else if (currentChord == "Dominante")
            {
                if (UnityEngine.Random.Range(0, 4) < 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        notasAcordesPlay.Add(j);
                    }
                }
            }
        }
    }

    void calcularDuracionAcordes()
    {
        lengthChords.Clear();
        float compasesRestantes = 8f;
        //El siguiente acorde va a durar 4 compases
        //25% de probabilidad de que dure 4 compases
        while (compasesRestantes > 0.0f)
        {
            if (UnityEngine.Random.Range(0, 4) == 3)
            {
                if (compasesRestantes - 4 >= 0)
                {
                    compasesRestantes -= 4.0f;
                    lengthChords.Add(4f);
                }

            }
            else
            {
                //33% de probabilidad de que se quede en 2 compases
                if (UnityEngine.Random.Range(0, 3) == 2)
                {
                    if (compasesRestantes - 2 >= 0)
                    {
                        compasesRestantes -= 2.0f;
                        lengthChords.Add(2f);
                    }
                }
                else
                {
                    //50% de probabilidad que se quede durando 1 compas
                    if (UnityEngine.Random.RandomRange(0, 2) == 1)
                    {
                        if (compasesRestantes - 1 >= 0)
                        {
                            compasesRestantes -= 1.0f;
                            lengthChords.Add(1f);
                        }
                    }
                    //La division minima dura medio compas
                    else
                    {
                        if (compasesRestantes - 0.5f >= 0)
                        {
                            compasesRestantes -= 0.5f;
                            lengthChords.Add(0.5f);
                        }
                    }
                }
            }
        }        
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
        notasAcordes.Clear();
        String acordes = "";
        bool highPitch1 = false;
        bool highPitch2 = false;
        for (int i = 0; i < 7; i++)
        {            
            notasAcordes.Add(escala[i]);
            if (i + 2 > escala.Count)
            {
                highPitch1 = true;
            }
            if (i + 4 > escala.Count)
            {
                highPitch2 = true;
            }
            notasAcordes.Add(escala[(i + 2) % escala.Count]);
            notasAcordes.Add(escala[(i + 4) % escala.Count]);
            String calidadAcorde;
            
            calidadAcorde = calcularCalidad(notasAcordes);
            acordes += ("Acorde: " + notasAcordes[0] + ": " + notasAcordes[0] + " " + notasAcordes[1] + " " + notasAcordes[2] + "\n - Calidad: " + calidadAcorde + "\n");
        }
        acordesText.text = acordes;
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
            soundPerSecond = (60.0f / (bpm * 4));
            timeStamp += Time.deltaTime;
            if (timeStamp >= soundPerSecond)
            {
                if (beats == compassRythm * 4)
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

                //Sonidos acordes

                timeStamp = 0.0f;
                beats += 1;
            }
            if (timeStamp >= soundPerSecond*lengthChords[nowPlayingChord]) { 
                if(nowPlayingChord == lengthChords.Count)
                {
                    nowPlayingChord = 0;
                }
                SonidosNotas[notasAcordesPlay[nowPlayingChord]].Play();
                SonidosNotas[notasAcordesPlay[nowPlayingChord + 1]].Play();
                SonidosNotas[notasAcordesPlay[nowPlayingChord + 2]].Play();
                nowPlayingChord += 1;
            }
        }
        else
        {
            beats = 0;
            nowPlayingChord = 0;
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
