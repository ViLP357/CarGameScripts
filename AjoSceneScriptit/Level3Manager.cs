using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public GameObject car;
    public GameObject valot;
    public GameObject levelinPortit;
    public Transform lahtopaikka;
    public GameObject levelinTehtavaTeksti;
    private void placeCar() {
        car.transform.position = lahtopaikka.position;
        car.transform.rotation = lahtopaikka.rotation;
    }
    public void presettings() {
        //Debug.Log("Level 3 käynnissä");
        placeCar();
        levelinPortit.SetActive(true);
        levelinTehtavaTeksti.SetActive(true);
        valot.SetActive(true);
    }
}
