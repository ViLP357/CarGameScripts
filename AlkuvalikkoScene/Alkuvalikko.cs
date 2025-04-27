using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System;

public class Alkuvalikko : MonoBehaviour
{
    public GameObject tasoValikko;
    public GameObject leaderboard;

    public PlayableDirector playableDirector;
    public GameObject sarjakuva;

    public float alkuaika;
    //private LevelManager levelscript;
    void Start()
    {

        tasoValikko.SetActive(false);
        leaderboard.SetActive(false);

        sarjakuva.SetActive(false);
        
        alkuaika = Time.time;
        Play();

        //PlayerPrefs.DeleteKey("BestTimeLevel1");
        //PlayerPrefs.DeleteKey("BestTimeLevel2");
        //PlayerPrefs.DeleteKey("BestTimeLevel3");
        //levelscript = GameObject.FindObjectOfType<LevelManager>();
        //if (levelscript == null)
        //{
        //    Debug.LogError("LevelManager-objektia ei löytynyt scenestä!");
        //}
    }
    void Update()
    {
        
        //Debug.Log(Time.time);
        //Debug.Log(alkuaika);
        //Debug.Log(Time.time - alkuaika);
        //Debug.Log(Time.time - alkuaika > 5f);
        if (Time.time - alkuaika > 48.5f) {
            //Debug.Log("Suljetaan");
            sarjakuva.SetActive(false);
        }  
    }

    public void Play() {
        sarjakuva.SetActive(true);
        playableDirector.Play();
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

    public void LeaderboardinNakyyys() {
        leaderboard.SetActive(!leaderboard.activeInHierarchy);
    }
    public void PelaaTaso1() {
        PlayerPrefs.SetInt("currentLevel", 1);
        SceneManager.LoadScene("AjoScene");
    }
    public void PelaaTaso2() {
        PlayerPrefs.SetInt("currentLevel", 2);
        SceneManager.LoadScene("AjoScene");
    }

    public void PelaaTaso3() {
        PlayerPrefs.SetInt("currentLevel", 3);
        Debug.Log(PlayerPrefs.GetInt("currentLevel"));
        SceneManager.LoadScene("AjoScene");
    }
}
