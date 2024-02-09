using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour
{
    //Importing the main drum patterns

    public AudioClip[] mainDrumPatterns;
    public AudioClip[] mainDrumFills;
    public AudioClip[] mainChords;
    public AudioSource source;

    public int songProgress;
    public int beatType;
    public int chordType;
    public float timePassed;

    public int[] drumTimings;
    public int[] chordTimings;

    public int[] fourChords;
    public bool[] doChordsEcho;
    public int[] chordProgression;

    //For spawning
    public GameObject noteBlock;
    public GameObject cam;
    public GameObject platforms;
    public Vector3 c_origin;
    public Vector3 origin;
    public Vector3 p_origin;
    public Vector3 offset;
    public int[] rotations;
    public int noteDirection;

    public int cameraRotation;

    //https://answers.unity.com/questions/1470684/how-to-change-position-of-gameobject.html

    //NOTE: Always start with a drum intro of 4 bars (start chords on beat 32 or 33, whichever one it is)




    // Start is called before the first frame update
    void Start()
    {
        //Also for spawning
        c_origin = new Vector3(250, 0, 0);
        origin = new Vector3(220, 0, 0);
        p_origin = new Vector3(0, 0, 0);
        offset = new Vector3(-30, 0, 0);

        rotations = new int[6];

        //should use a dictionary but eh.
        rotations[1] = 0;
        rotations[2] = -72;
        rotations[3] = -144;
        rotations[4] = 144;
        rotations[5] = 72;

        cameraRotation = 0;


        //3 second delay (not currently functional)
        timePassed = -3f;
        songProgress = 0;

        drumTimings = new int[2000];
        chordTimings = new int[2000];

        fourChords = new int[4];
        doChordsEcho = new bool[4];
        chordProgression = new int[8];

        source = gameObject.GetComponent<AudioSource>();


        //Create a randomized array for the drumming patterns.
        int index = 0;
        for (int l = 0; l < 8; l++)
        {
            int randomPattern = Random.Range(1, 8); //be sure to start with 1!
            for (int j = 0; j < 4; j++) {
                drumTimings[index] = randomPattern;
                index += 8;
            }

        }
        index = 32;
        //Create a randomized array for the chord patterns.
        for (int l = 0; l < 28; l++)
        {
            int randomChord = Random.Range(1, 6); //be sure to start with 1!
            for (int j = 0; j < 8; j++) {
                chordTimings[index] = randomChord;
                index += 1;
            }
            //Random.Range(1, 9)
        }

        
        
        //Spawn all the noteblocks
        for (int i = 0; i < 1000; i++)
        {
            if (chordTimings[i] != 0) {
                noteDirection = rotations[chordTimings[i]];
                Instantiate(noteBlock, origin + (i * offset), Quaternion.Euler(noteDirection , 0 , 0)); //last element is the rotation, which is assigned from the noteTimings array
            }
        }

        InvokeRepeating("NextBeat", 3.0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraRotation -= 144;
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraRotation -= 72;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cameraRotation += 72;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cameraRotation += 144;
        }
        cam.transform.rotation = Quaternion.Euler(0, -90, cameraRotation);
    }

    void NextBeat()
    {
        //TIP: Move the camera and the platforms, NOT the notes!
        //Then none of the notes have to be turned on and off, unless played.
        //Rigidbody instance = Instantiate(projectile);

        //instance.velocity = Random.insideUnitSphere * 5;

        //Moving the "player" forward each beat of the song
        cam.transform.position = c_origin + (songProgress * offset);
        platforms.transform.position = p_origin + (songProgress * offset);

        beatType = drumTimings[songProgress];
        chordType = chordTimings[songProgress]; //only a default value

        Debug.Log("Beat Type: " + beatType);
        Debug.Log("Time Passed: " + timePassed);

        Debug.Log("Beat called: " + beatType);

        songProgress += 1;

        //PlayOneShot is best, as it allows for overlapping sounds (which is what we want!)

        if (beatType != 0){
            source.clip = mainDrumPatterns[beatType];
            source.PlayOneShot(source.clip);
        }

        if (chordType != 0) {
            source.clip = mainChords[chordType];
            source.PlayOneShot(source.clip);
        }


        

    }
}
