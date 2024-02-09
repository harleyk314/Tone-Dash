using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{

    public GameObject noteBlock;
    public Vector3 origin;
    public Vector3 offset;
    public int[] rotations;



    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(250, 0, 0);
        offset = new Vector3(5, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
