using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    private Level1Manager level1script;
    private Level2Manager level2script;
    private Level3Manager level3script;
    // Start is called before the first frame update
    void Start()
    {
    level1script = FindObjectOfType<Level1Manager>();
    level2script = FindObjectOfType<Level2Manager>();
    level3script = FindObjectOfType<Level3Manager>();

    if (PlayerPrefs.HasKey("currentLevel")){
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log("Current Level: " + currentLevel);
        
        if (currentLevel == 1) {
            level1script.presettings();
        }
        else if (currentLevel == 2) {
            level2script.presettings();
        }
        else if (currentLevel == 3) {
            level3script.presettings();
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
