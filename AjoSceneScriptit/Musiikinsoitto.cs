using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musiikinsoitto : MonoBehaviour
{
    AudioSource ääni;
    public Slider säädin;
    public Slider volumeSäädin;
    public AudioClip[] taustamusiikit;
    // Start is called before the first frame update
    void Start()
    {
     ääni = GetComponentInChildren<AudioSource>();
     ääni.clip = taustamusiikit[0];
     ääni.Play();
    }

    // Update is called once per frame

    public void VaihdaKanavaa() {
        ääni.clip = taustamusiikit[(int)säädin.value];
        ääni.Play();
    }

    public void SaadaVolume() {
        ääni.volume = volumeSäädin.value;
    }

}
