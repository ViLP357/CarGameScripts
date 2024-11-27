using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Aika : MonoBehaviour {

    public TMP_Text teksti;
    public TMP_Text voittoaikateksti;
    public static Aika instanssi;
    int viimeAika;
    int nykyinenAika;
    string muunnettuAika;
    
// Update is called once per frame
    void Start()
    {
        instanssi = this;
    }

    void Update() {
        int nykyinenAika = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        if (nykyinenAika != viimeAika) 
        {
            muunnettuAika = muunnaTekstiksi(nykyinenAika);
            teksti.text = muunnettuAika;
            voittoaikateksti.text = muunnettuAika;
            viimeAika = nykyinenAika;
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
    public string KulunutAika() {
        return muunnaTekstiksi(viimeAika);
    }
    public float KulunutAikaFloat () {
        return viimeAika;
    }
}
