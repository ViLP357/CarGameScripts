using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pisteet : MonoBehaviour

{
    public static Pisteet instanssi;
    //public TMP_Text teksti;
    //public TMP_Text pakettiteksti;
    int pisteet;
    int haetutPaketit;
    private Portcontroller portscript;
    public Image pakettiMato;

    // Start is called before the first frame update
    void Start()
    {

        instanssi = this;
        pakettiMato.fillAmount = 0;

        //pakettiteksti.text = haetutPaketit.ToString();
        //teksti.text = pisteet.ToString();

    }
    void LateUpdate() {
        portscript = FindObjectOfType<Portcontroller>();
        //pakettiteksti.text = "Paketit: " + haetutPaketit.ToString() + "/" + portscript.pakettienMaara();
        //teksti.text = "Portit: " + pisteet.ToString() + "/" +  portscript.porttienMaara();
    }

    public void Lisää(int maara) {
        pisteet += maara;
        //teksti.text = "Portit: " + pisteet.ToString() + "/" +  portscript.porttienMaara();
    }
    public void PakettiHaettu(int maara) {
        haetutPaketit += maara;
        pakettiMato.fillAmount = (float)haetutPaketit / portscript.pakettienMaara();
        Debug.Log(portscript.pakettienMaara());
        Debug.Log(haetutPaketit);
        Debug.Log(haetutPaketit / portscript.pakettienMaara());
        //pakettiteksti.text = "Paketit: " + haetutPaketit.ToString() + "/" + portscript.pakettienMaara();
    }
    public int PisteidenMaara() {
        return pisteet;
    }
    public int PakettienMaara() {
        return haetutPaketit;
    }
}
