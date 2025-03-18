using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class Portcontroller : MonoBehaviour
{
    
    public static Portcontroller instanssi;
    public Transform[] porttienPaikat;
    public Transform[] pakettienPaikat;
    public GameObject portti;
    public GameObject stopPlace;

    public List<GameObject> porttiObjektit = new List<GameObject>();
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
            //Instantiate(portti, kohta.position, portti.transform.rotation);

            
            GameObject uusiPortti = Instantiate(portti, kohta.position, kohta.rotation);
            TextMeshPro textmesh = uusiPortti.GetComponentInChildren<TextMeshPro>();
            textmesh.text = (i + 1).ToString();
            if (i != 0) {
                uusiPortti.SetActive(false);
            }
            porttiObjektit.Add(uusiPortti);
        }
    }
    private void placeStopPlaces() {
        for (int i = 0; i < pakettienPaikat.Length; i++) {
            Transform kohta = pakettienPaikat[i];
            Instantiate(stopPlace, kohta.position, kohta.rotation);
            //Instantiate(stopPlace, kohta.position, kohta.rotation);
        }   
    }
    public void NaytaSeuraava(int numero) {
        if (numero < porttienPaikat.Length) {
            porttiObjektit[numero].SetActive(true);
        }
    }

    public int porttienMaara () {
        return porttienPaikat.Length;
    }
    public int pakettienMaara() {
        return pakettienPaikat.Length;
    }

}
