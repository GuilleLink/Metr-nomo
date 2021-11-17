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

    public Text rythmText;
    public Text seedText;
    public Text scaleText;
    public Text acordesText;
    public Text nowPlayingChordText;

    public int bpm;
    public float soundsPerSecond;
    public int compassRythm;

    public InputField bpmInput;
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
    public AudioSource Ab5Sonido;
    public AudioSource A5Sonido;
    public AudioSource Bb5Sonido;
    public AudioSource B5Sonido;
    public AudioSource C5Sonido;
    public AudioSource Db5Sonido;
    public AudioSource D5Sonido;
    public AudioSource Eb5Sonido;
    public AudioSource E5Sonido;
    public AudioSource F5Sonido;
    public AudioSource Gb5Sonido;
    public AudioSource G5Sonido;

    private float soundPerSecond;
    private float soundPerSecondPiano;
    private float timeStamp;
    private float timeStamp2;
    private int beats;

    //Acordes
    private int nowPlayingChord;
    private int estructuraActual;
    private int nowPlayingStructure;

    public GameObject stopButton;
    public GameObject startButton;

    public bool playOn;
    private bool playingChords;

    List<String> notasHalfFull = new List<String>();
    List<AudioSource> SonidosNotas = new List<AudioSource>();

    List<String> notasAcordes = new List<String>();
    List<int> notasAcordesPlay = new List<int>();
    List<float> lengthChords = new List<float>();

    Dictionary<int, List<int>> EstructuraFiller = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> EstructuraMetrica = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> EstructuraRitmica = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> EstructuraAcordes = new Dictionary<int, List<int>>();
    Dictionary<int, List<float>> EstructuraLengthAcordes = new Dictionary<int, List<float>>();
    Dictionary<int, List<int>> EstructuraMelodia = new Dictionary<int, List<int>>();
    Dictionary<int, List<float>> EstructuraLengthMelodia = new Dictionary<int, List<float>>();

    List<int> estructuras = new List<int>();

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
        notasHalfFull.Add("Ab1");
        SonidosNotas.Add(Ab5Sonido);
        notasHalfFull.Add("A1");
        SonidosNotas.Add(A5Sonido);
        notasHalfFull.Add("Bb1");
        SonidosNotas.Add(Bb5Sonido);
        notasHalfFull.Add("B1");
        SonidosNotas.Add(B5Sonido);
        notasHalfFull.Add("C1");
        SonidosNotas.Add(C5Sonido);
        notasHalfFull.Add("Db1");
        SonidosNotas.Add(Db5Sonido);
        notasHalfFull.Add("D1");
        SonidosNotas.Add(D5Sonido);
        notasHalfFull.Add("Eb1");
        SonidosNotas.Add(Eb5Sonido);
        notasHalfFull.Add("E1");
        SonidosNotas.Add(E5Sonido);
        notasHalfFull.Add("F1");
        SonidosNotas.Add(F5Sonido);
        notasHalfFull.Add("Gb1");
        SonidosNotas.Add(Gb5Sonido);
        notasHalfFull.Add("G1");
        SonidosNotas.Add(G5Sonido);
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
        estructuraActual = 0;
        playingChords = false;
        rythmsBeat.Clear();
        metrica.Clear();
        filler.Clear();
        notasAcordesPlay.Clear();
        lengthChords.Clear();
        GenerateStructure();
    }

    void GenerateFiller()
    {
        rythmsBeat.Clear();
        metrica.Clear();
        filler.Clear();
        //Elige entre que sean negras, corcheas o semicorcheas
        int option = UnityEngine.Random.Range(0, 3);
        //negras
        //[1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0] 4/4
        //int optionAgrupacion = UnityEngine.Random.Range(3, 5);
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


        for (int i = 0; i < rythmsBeat.Count; i++)
        {
            if (rythmsBeat[i] == 0)
            {
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    filler.Add(1);
                }
                else
                {
                    filler.Add(0);
                }
            }
            else if (rythmsBeat[i] == 1)
            {
                filler.Add(0);
            }
        }
        //Se agregan al diccionario sus arreglos correspondientes
        EstructuraRitmica.Add(estructuraActual, rythmsBeat);
        EstructuraMetrica.Add(estructuraActual, metrica);
        EstructuraFiller.Add(estructuraActual, filler);
    }

    //Se genera la escla del acorde, de momento no permite el tema de sostenidos, solamente se aceptan bemoles y naturales de las teclas correspondientes.
    void GenerateScale()
    {
        List<int> NotesForChords = new List<int>();
        List<String> escala = new List<String>();        
        string noteInputTxt = notasHalfFull[UnityEngine.Random.Range(0, notasHalfFull.Count)];
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

    void calcularAcordesTocados()
    {
        //Jalar los acordes correspondientes para cada escala
        //Tonica: 1,3,6
        //Sub: 2,4
        //Dominante: 5,7
        String currentChord = "SubDominante";
        for (int i = 0; i < lengthChords.Count; i++)
        {
            String nota = "";
            String nota2 = "";
            String nota3 = "";
            //Si el acorde que se tiene previamente es tonica se da preferencia a subdominante
            if (currentChord == "Tonica") {
                int rand = UnityEngine.Random.Range(0, 4);                
                //Tonica
                if (rand == 0)
                {
                    //La primera Tonica
                    int randomTonica = UnityEngine.Random.Range(0, 3);
                    //1
                    if (randomTonica == 1)
                    {
                        nota = notasAcordes[0];
                        nota2 = notasAcordes[1];
                        nota3 = notasAcordes[2];
                    }
                    //3
                    else if (randomTonica == 2)
                    {
                        nota = notasAcordes[6];
                        nota2 = notasAcordes[7];
                        nota3 = notasAcordes[8];
                    }
                    //6
                    else
                    {
                        nota = notasAcordes[15];
                        nota2 = notasAcordes[16];
                        nota3 = notasAcordes[17];
                    }
                    currentChord = "Tonica";
                }
                //Subdominante
                else if (rand >= 1 && rand < 3)
                {
                    int randomSubDominante = UnityEngine.Random.Range(0, 3);
                    //2
                    if (randomSubDominante == 1)
                    {
                        nota = notasAcordes[3];
                        nota2 = notasAcordes[4];
                        nota3 = notasAcordes[5];
                    }
                    //4
                    else
                    {
                        nota = notasAcordes[9];
                        nota2 = notasAcordes[10];
                        nota3 = notasAcordes[11];
                    }
                    currentChord = "SubDominante";
                }
                //Dominante
                else
                {
                    //5
                    nota = notasAcordes[12];
                    nota2 = notasAcordes[13];
                    nota3 = notasAcordes[14];
                    currentChord = "Dominante";
                }
            }
            else if (currentChord == "SubDominante")
            {
                int rand = UnityEngine.Random.Range(0, 5);
                //Tonica
                if (rand < 2)
                {
                    //La primera Tonica
                    int randomTonica = UnityEngine.Random.Range(0, 3);
                    //1
                    if (randomTonica == 1)
                    {
                        nota = notasAcordes[0];
                        nota2 = notasAcordes[1];
                        nota3 = notasAcordes[2];
                    }
                    //3
                    else if (randomTonica == 2)
                    {
                        nota = notasAcordes[6];
                        nota2 = notasAcordes[7];
                        nota3 = notasAcordes[8];
                    }
                    //6
                    else
                    {
                        nota = notasAcordes[15];
                        nota2 = notasAcordes[16];
                        nota3 = notasAcordes[17];
                    }
                    currentChord = "Tonica";
                } 
                //Subdominante
                else if (rand == 2)
                {
                    int randomSubDominante = UnityEngine.Random.Range(0, 3);
                    //2
                    if (randomSubDominante == 1)
                    {
                        nota = notasAcordes[3];
                        nota2 = notasAcordes[4];
                        nota3 = notasAcordes[5];
                    }
                    //4
                    else
                    {
                        nota = notasAcordes[9];
                        nota2 = notasAcordes[10];
                        nota3 = notasAcordes[11];
                    }

                    currentChord = "SubDominante";
                }
                //Dominante
                else
                {
                    //5
                    nota = notasAcordes[12];
                    nota2 = notasAcordes[13];
                    nota3 = notasAcordes[14];
                    currentChord = "Dominante";
                }
            }else if (currentChord == "Dominante")
            {
                int rand = UnityEngine.Random.Range(0, 4);
                //Tonica
                if (rand == 0)
                {
                    //La primera Tonica
                    int randomTonica = UnityEngine.Random.Range(0, 3);
                    //1
                    if (randomTonica == 1)
                    {
                        nota = notasAcordes[0];
                        nota2 = notasAcordes[1];
                        nota3 = notasAcordes[2];
                    }
                    //3
                    else if (randomTonica == 2)
                    {
                        nota = notasAcordes[6];
                        nota2 = notasAcordes[7];
                        nota3 = notasAcordes[8];
                    }
                    //6
                    else
                    {
                        nota = notasAcordes[15];
                        nota2 = notasAcordes[16];
                        nota3 = notasAcordes[17];
                    }
                    currentChord = "Tonica";
                }
                //Subdominante
                else if (rand >= 1 && rand < 3)
                {
                    int randomSubDominante = UnityEngine.Random.Range(0, 3);
                    //2
                    if (randomSubDominante == 1)
                    {
                        nota = notasAcordes[3];
                        nota2 = notasAcordes[4];
                        nota3 = notasAcordes[5];
                    }
                    //4
                    else
                    {
                        nota = notasAcordes[9];
                        nota2 = notasAcordes[10];
                        nota3 = notasAcordes[11];
                    }
                    currentChord = "SubDominante";
                }
                //Dominante
                else
                {
                    //5
                    nota = notasAcordes[12];
                    nota2 = notasAcordes[13];
                    nota3 = notasAcordes[14];
                    currentChord = "Dominante";
                }
            }
            notasAcordesPlay.Add(notasHalfFull.IndexOf(nota));
            notasAcordesPlay.Add(notasHalfFull.IndexOf(nota2));
            notasAcordesPlay.Add(notasHalfFull.IndexOf(nota3));
        }
        //Se agregan los acordes al diccionario
        EstructuraAcordes.Add(estructuraActual, notasAcordesPlay);
    }

    void calcularDuracionAcordes()
    {
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
                    if (UnityEngine.Random.Range(0, 2) == 1)
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
        //Se agregan la longitud de los acordes a su estructura
        EstructuraLengthAcordes.Add(estructuraActual, lengthChords);
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
        for (int i = 0; i < 7; i++)
        {
            List<String> notasAcordesTemp = new List<String>();
            notasAcordes.Add(escala[i]);
            if (i + 2 > escala.Count)
            {
                notasAcordes.Add(escala[(i + 2) % escala.Count] + "1");
            }
            else
            {
                notasAcordes.Add(escala[(i + 2) % escala.Count]);
            }
            if (i + 4 > escala.Count)
            {
                notasAcordes.Add(escala[(i + 4) % escala.Count] + "1");
            }
            else
            {
                notasAcordes.Add(escala[(i + 4) % escala.Count]);
            }
            
            
            notasAcordesTemp.Add(notasAcordes[i*3]);
            notasAcordesTemp.Add(notasAcordes[(i*3)+1]);
            notasAcordesTemp.Add(notasAcordes[(i*3)+2]);

            String calidadAcorde;            
            calidadAcorde = calcularCalidad(notasAcordesTemp);
            acordes += ("Acorde: " + notasAcordes[i*3] + ": " + notasAcordes[i*3] + " " + notasAcordes[(i * 3) + 1] + " " + notasAcordes[(i * 3) + 2] + "\n - Calidad: " + calidadAcorde + "\n");
        }

        acordesText.text = acordes;
    }

    void GenerateStructure()
    {
        //Se genera la primera estructura en general #Estructura 0
        GenerateFiller();
        GenerateScale();
        //Se crean las siguientes 3 estructuras
        for (int i =0; i<3; i++)
        {
            int randomStructure = UnityEngine.Random.Range(0, 2);
            //Se genera una estructura diferente
            if(randomStructure == 0)
            {
                estructuraActual += 1;
                GenerateFiller();
                GenerateScale();
            }
            //Se usa una estructura ya existente
            else
            {
                int estructuraARepetir = UnityEngine.Random.Range(0,estructuras.Count);
                estructuras.Add(estructuraARepetir);
            }            
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
            soundPerSecond = (60.0f / (bpm * 4));
            soundPerSecondPiano = (60.0f / bpm);
            timeStamp += Time.deltaTime;
            timeStamp2 += Time.deltaTime;
            //Musica Bateria
            if (timeStamp >= soundPerSecond)
            {
                if (beats == filler.Count)
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
                beats += 1;
            }
            //Musica Acordes
            if (timeStamp2 <= (soundPerSecondPiano*lengthChords[nowPlayingChord])) {
                if(nowPlayingChord == lengthChords.Count-1)
                {
                    nowPlayingChord = 0;
                }
                if (playingChords == false)
                {
                    playingChords = true;
                    Debug.Log(notasAcordesPlay.Count);
                    Debug.Log(nowPlayingChord);
                    //nowPlayingChordText.text = (""+notasAcordesPlay[nowPlayingChord*3]);
                    //Debug.Log(notasAcordesPlay[nowPlayingChord*3]);
                    //Debug.Log(notasAcordesPlay[nowPlayingChord*3+1]);
                    //Debug.Log(notasAcordesPlay[nowPlayingChord*3+2]);
                    //SonidosNotas[notasAcordesPlay[nowPlayingChord*3] % SonidosNotas.Count].Play();
                    //SonidosNotas[notasAcordesPlay[(nowPlayingChord*3) + 1] % SonidosNotas.Count].Play();
                    //SonidosNotas[notasAcordesPlay[(nowPlayingChord*3) + 2] % SonidosNotas.Count].Play();
                }
            } else if(timeStamp2 > (soundPerSecondPiano * lengthChords[nowPlayingChord]))
            {

                //SonidosNotas[notasAcordesPlay[nowPlayingChord] % SonidosNotas.Count].Stop();
                //SonidosNotas[notasAcordesPlay[(nowPlayingChord * 3) + 1] % SonidosNotas.Count].Stop();
                //SonidosNotas[notasAcordesPlay[(nowPlayingChord * 3) + 2] % SonidosNotas.Count].Stop();
                playingChords = false;
                timeStamp2 = 0f;
                nowPlayingChord += 1;
            }
            
        }
        else
        {
            nowPlayingChordText.text = ("");
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
            playingChords = false;
        }
    }

}
