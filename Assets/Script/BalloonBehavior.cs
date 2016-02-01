using UnityEngine;
using System.Collections;


public class BalloonBehavior : MonoBehaviour {
    private AudioSource audio;
    public float sensitivity = 100;
    public float loudness = 0;


    // Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();

        audio.clip = Microphone.Start(null, true, 10, 44100);
        audio.loop = true; // Set the AudioClip to loop
        audio.mute = true; // Mute the sound, we don't want the player to hear it
        while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)) { } // Wait until the recording has started
        audio.Play(); // Play the audio source!
    }
	
	// Update is called once per frame
	void Update () {
	    loudness = GetAveragedVolume() * sensitivity;

        Debug.Log(loudness);
    }

    float GetAveragedVolume(){ 
        float[] data = new float[256];
        float a = 0;
        audio.GetOutputData(data,0);
        foreach(float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a/256;
    }
}
