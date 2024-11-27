using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public GameObject car;
    public GameObject levelinPortit;
    public Transform lahtopaikka;

    // Start is called before the first frame update

    private void placeCar() {
        car.transform.position = lahtopaikka.position;
        //Instantiate(car, lahtopaikka.position, lahtopaikka.rotation);
    }
    public void presettings() {
        Debug.Log("Presettings toimii");
        placeCar();
        levelinPortit.SetActive(true);
    }
}
