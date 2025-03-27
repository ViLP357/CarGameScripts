using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ennatysajat : MonoBehaviour
{
    public TMP_Text EnnatysaikaLevel1;
    public TMP_Text EnnatysaikaLevel2;
    public TMP_Text EnnatysaikaLevel3;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("BestTimeLevel1")){
            //Debug.Log("Aika löytynyt");
            EnnatysaikaLevel1.text = muunnaTekstiksi((int)PlayerPrefs.GetFloat("BestTimeLevel1"));
        }
        else {
            Debug.Log("ei aikaa 1");
            EnnatysaikaLevel1.text = "--:--";
        }
        if (PlayerPrefs.HasKey("BestTimeLevel2")){
            //Debug.Log("Aika löytynyt");
            EnnatysaikaLevel2.text = muunnaTekstiksi((int)PlayerPrefs.GetFloat("BestTimeLevel2"));
        }
        else {
            //Debug.Log("ei aikaa 2");
            EnnatysaikaLevel2.text = "--:--";
        }
        if (PlayerPrefs.HasKey("BestTimeLevel3")){
            //Debug.Log("Aika löytynyt");
            EnnatysaikaLevel3.text = muunnaTekstiksi((int)PlayerPrefs.GetFloat("BestTimeLevel3"));
        }
        else {
            Debug.Log("ei aikaa 3");
            EnnatysaikaLevel3.text = "--:--";
        }
    }
    private string muunnaTekstiksi(int aika) {
        int tunnit = aika / 3600;
        int minuutit = (aika % 3600) / 60;
        int sekunnit = aika % 60;
        if (tunnit > 0) 
            return string.Format("{0:D2}:{1:D2}:{2:D2}", tunnit, minuutit, sekunnit);
    return string.Format("{0:D2}:{1:D2}", minuutit, sekunnit);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
