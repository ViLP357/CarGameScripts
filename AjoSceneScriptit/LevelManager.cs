using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    private Level1Manager level1script;
    private Level2Manager level2script;
    private Level3Manager level3script;
    public GameObject Reunat3;
    public GameObject Reunat1;

    public Sprite Level1Kartta;
    public Sprite Level2Kartta;
    public Sprite Level3Kartta;

    public Image Kartta;
    // Start is called before the first frame update
    void Start()
    {
    level1script = FindObjectOfType<Level1Manager>();
    level2script = FindObjectOfType<Level2Manager>();
    level3script = FindObjectOfType<Level3Manager>();

    //Sprite Level1Kartta = Resources.Load<Sprite>("karttaLevel1");
    //Sprite Level2Kartta = Resources.Load<Sprite>("karttaLevel2");
    //Sprite Level3Kartta = Resources.Load<Sprite>("karttaLevel3");

    //Debug.Log("Aktiivinen: " + Level1Kartta);

    if (PlayerPrefs.HasKey("currentLevel")){
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log("Current Level: " + currentLevel);
        
        if (currentLevel == 1) {
            level1script.presettings();
            Reunat1.SetActive(true);
            Reunat3.SetActive(false);
            Kartta.GetComponent<Image>().sprite = Level1Kartta;

        }
        else if (currentLevel == 2) {
            level2script.presettings();
            Reunat1.SetActive(false);
            Reunat3.SetActive(true);
            Kartta.GetComponent<Image>().sprite = Level2Kartta;
        }
        else if (currentLevel == 3) {
            level3script.presettings();
            Reunat1.SetActive(false);
            Reunat3.SetActive(true);
            Kartta.GetComponent<Image>().sprite = Level3Kartta;
        }
    }
    else {
        currentLevel = 1;
        Debug.Log("Nykyinen taso: 1");
        level1script.presettings();
        Kartta.GetComponent<Image>().sprite = Level1Kartta;
    }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
    public int returnCurrentLevel(int level) {
        return currentLevel;
    }
}
