using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taustamusiikki : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource ääni;

    public AudioClip taustamusiikki;
    void Start()
    {
        ääni = GetComponent<AudioSource>();
        ääni.clip = taustamusiikki;
        ääni.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
