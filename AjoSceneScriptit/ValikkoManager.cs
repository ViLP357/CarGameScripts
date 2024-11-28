using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ValikkoManager : MonoBehaviour
{
    public static ValikkoManager instanssi;
    public GameObject PysaytysPanel;
    public GameObject kuolemaPanel;
    public GameObject voittoPanel;
    public GameObject tehtavaPanel;
    public GameObject varkikkoValikko;
    public GameObject varikkoNappi;
    public Aika aikascripti;
    private string TimeSavePlace;
    private bool alkuAvaus;
    //public TMP_Text aikateksti;
    void Start() {
        PysaytysPanel.SetActive(false);
        kuolemaPanel.SetActive(false);
        voittoPanel.SetActive(false);
        tehtavaPanel.SetActive(true);
        varkikkoValikko.SetActive(false);
        instanssi = this;
        aikascripti = Object.FindObjectOfType<Aika>();
        Time.timeScale = 0;
        alkuAvaus = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            bool avaa = !PysaytysPanel.activeInHierarchy;
            PysaytysPanel.SetActive(avaa);
            Time.timeScale = avaa ? 0 : 1;
        }
    }

    public void Jatka() {
        PysaytysPanel.SetActive(false);
        kuolemaPanel.SetActive(false);
        
        Time.timeScale = 1;
    }

    public void AloitaAlusta() {
        SceneManager.LoadScene("AjoScene");
        Time.timeScale = 1;
    }
    public void Home() {
        SceneManager.LoadScene("Alkuvalikko");
        Time.timeScale = 1;
    }

    public void AvaaKuolemaValikko () {
        kuolemaPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void AvaaTehtavaPanel() {
        tehtavaPanel.SetActive(true);
    }
    public void SuljeTehtavaPanel() {
        tehtavaPanel.SetActive(false);
        if (alkuAvaus == true) {
            Time.timeScale = 1;
            alkuAvaus = false;
            varikkoNappi.SetActive(false);
            varkikkoValikko.SetActive(false);
        }
    }
    public void AvaaVoittoValikko () {
        voittoPanel.SetActive(true);
        Time.timeScale = 0;
        
        TimeSavePlace = "BestTimeLevel" + PlayerPrefs.GetInt("currentLevel").ToString();

        Debug.Log(aikascripti.KulunutAikaFloat());
        Debug.Log(TimeSavePlace);


        if (PlayerPrefs.GetFloat(TimeSavePlace) > aikascripti.KulunutAikaFloat()) {
            PlayerPrefs.SetFloat(TimeSavePlace, aikascripti.KulunutAikaFloat());
            Debug.Log("Aika tallennettu");
            Debug.Log(" " + TimeSavePlace +  aikascripti.KulunutAikaFloat());
        }
        else {
            Debug.Log("No nyt ei tallennettu");
        }
    }
    public void  AvaaTaiSuljeVarikko() {
        varkikkoValikko.SetActive(!varkikkoValikko.activeInHierarchy);
    }
}
