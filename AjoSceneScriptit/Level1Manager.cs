using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public GameObject car;
    public GameObject levelinPortit;
    public Transform lahtopaikka;
    public GameObject levelinTehtavaTeksti;

    // Start is called before the first frame update

    private void placeCar() {
        car.transform.position = lahtopaikka.position;
        car.transform.rotation = lahtopaikka.rotation;
        //Instantiate(car, lahtopaikka.position, lahtopaikka.rotation);
    }
    public void presettings() {
        //Debug.Log("Presettings toimii");
        placeCar();
        levelinPortit.SetActive(true);
        levelinTehtavaTeksti.SetActive(true);
    }
}
