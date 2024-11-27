using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Alkuvalikko : MonoBehaviour
{
    public GameObject tasoValikko;
    //private LevelManager levelscript;
    void Start()
    {
        tasoValikko.SetActive(false);
        //levelscript = GameObject.FindObjectOfType<LevelManager>();
        //if (levelscript == null)
        //{
        //    Debug.LogError("LevelManager-objektia ei löytynyt scenestä!");
        //}
    }
    public void Lopeta() {
        Application.Quit();
    }
    public void AvaaTasoValikko() {
        tasoValikko.SetActive(true);
    }
    public void SuljeTasoValikko(){
        tasoValikko.SetActive(false);
    }
    public void PelaaTaso1() {
        PlayerPrefs.SetInt("currentLevel", 1);
        SceneManager.LoadScene("AjoScene");
    }
    public void PelaaTaso2() {
        PlayerPrefs.SetInt("currentLevel", 2);
        SceneManager.LoadScene("AjoScene");
    }

}
