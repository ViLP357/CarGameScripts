using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portcontroller : MonoBehaviour
{
    
    public static Portcontroller instanssi;
    public Transform[] porttienPaikat;
    public Transform[] pakettienPaikat;
    public GameObject portti;
    public GameObject stopPlace;
    void Start() {
        instanssi = this;
        placePorts();
        placeStopPlaces();
    }
    public void Voitto() {
        Debug.Log("Peli läpäisty");
    }
    public void Tuhoa(Collider other) {
        Destroy(other.gameObject, 0.5f);
    }
    private void placePorts() {
        for (int i = 0; i < porttienPaikat.Length; i++) {
            Transform kohta = porttienPaikat[i];

            Instantiate(portti, kohta.position, portti.transform.rotation);
        }
    }
    private void placeStopPlaces() {
        for (int i = 0; i < pakettienPaikat.Length; i++) {
            Transform kohta = pakettienPaikat[i];
            Instantiate(stopPlace, kohta.position, stopPlace.transform.rotation);
        }
    }

    public int porttienMaara () {
        return porttienPaikat.Length;
    }
    public int pakettienMaara() {
        return pakettienPaikat.Length;
    }

}
