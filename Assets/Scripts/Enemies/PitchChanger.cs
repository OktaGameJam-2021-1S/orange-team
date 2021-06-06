using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchChanger : MonoBehaviour
{
    public AudioSource Audio;

    private void OnEnable()
    {
        var clip = Audio.clip;
        Audio.pitch = Random.Range(.95f, 1.05f);
        Audio.Play();
    }

}
