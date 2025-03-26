using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musiikinsoitto : MonoBehaviour
{
    AudioSource ääni;
    public AudioClip[] taustamusiikit;
    // Start is called before the first frame update
    void Start()
    {
     ääni = GetComponentInChildren<AudioSource>();
     ääni.clip = taustamusiikit[0];
     ääni.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
