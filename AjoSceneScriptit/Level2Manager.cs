using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public GameObject car;
    public GameObject levelinPortit;
    public Transform lahtopaikka;
    private void placeCar() {
        car.transform.position = lahtopaikka.position;
    }
    public void presettings() {
        Debug.Log("Level 2 käynnissä");
        placeCar();
        levelinPortit.SetActive(true);
    }
}
