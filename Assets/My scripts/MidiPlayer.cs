// ------------------------------------------------------------------------
// Load a MIDI file and Play
// this script is provided in this folder: 
// Assets\MidiPlayer\Demo\FreeDemos\Script\TheSimplestMidiPlayer.cs
// ------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

namespace DemoMPTK
{
    /// <summary>
    /// This demo is able to play a MIDI file only by script.\n
    /// There is nothing to create in the Unity editor, just add this script to a GameObject in your scene and run!
    /// </summary>
    public class MidiPlayer : MonoBehaviour
    {
        // MidiPlayerGlobal is a singleton: only one instance can be created. Making static to have only one reference.

        MidiFilePlayer midiFilePlayer;

        private void Awake()
        {
            Debug.Log("Awake: dynamically add MidiFilePlayer component");

            // MidiPlayerGlobal is a singleton: only one instance can be created. 
            if (MidiPlayerGlobal.Instance == null)
                gameObject.AddComponent<MidiPlayerGlobal>();

            // When running, this component will be added to this gameObject. Set essential parameters.
            midiFilePlayer = gameObject.AddComponent<MidiFilePlayer>();
            midiFilePlayer.MPTK_CorePlayer = true;
            midiFilePlayer.MPTK_DirectSendToPlayer = true;
        }

        public void Start()
        {
            Debug.Log("Start: select a MIDI file from the MPTK DB and play");

            // Select a MIDI from the MIDI DB (with exact name)
            midiFilePlayer.MPTK_MidiName = "Blossum midi";
            // Play the MIDI file
            midiFilePlayer.MPTK_Play();
        }
    }
}
