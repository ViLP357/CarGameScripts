using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    private Level1Manager level1script;
    private Level2Manager level2script;
    // Start is called before the first frame update
    void Start()
    {
    level1script = GameObject.FindObjectOfType<Level1Manager>();
    level2script = GameObject.FindObjectOfType<Level2Manager>();

    if (PlayerPrefs.HasKey("currentLevel")){
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log("Current Level: " + currentLevel);
        
        if (currentLevel == 1) {
            level1script.presettings();
        }
        if (currentLevel == 2) {
            level2script.presettings();
        }
    }
    else {
        currentLevel = 1;
        Debug.Log("Nykyinen taso: 1");
        level1script.presettings();
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int returnCurrentLevel(int level) {
        return currentLevel;
    }
}
