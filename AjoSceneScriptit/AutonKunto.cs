using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AutonKunto : MonoBehaviour
{
    public static AutonKunto instanssi;
    //public TMP_Text conditionTeksti;

    //public TMP_Text fuelTeksti;
    public Image HealthBar;
    public float conditionLevel;

    public Image FuelBar;
    public float fuelLevel;
    private float edellinenAika;
    // Start is called before the first frame update

    void Start() {
        instanssi = this;
        //conditionTeksti.text = conditionLevel.ToString();
        //fuelTeksti.text = fuelLevel.ToString();
    }
    void Update() {
        if (Time.time - edellinenAika > 1f) {
            fuelLevel -= CarController.instanssi.PalautaNopeus() / 20.0f;
            //fuelTeksti.text = fuelLevel.ToString();
            FuelBar.fillAmount = fuelLevel / 100f;
            edellinenAika = Time.time;
            if (fuelLevel < 0) {
                ValikkoManager.instanssi.AvaaKuolemaValikko();
            }
        }
    }
    public void crashHit (float hit) {
        if (conditionLevel - hit > 0f) {
            conditionLevel -= hit;
        }
        else {
            Debug.Log("Health loppu");
            conditionLevel = 0;
            ValikkoManager.instanssi.AvaaKuolemaValikko();
        }
        //conditionTeksti.text = conditionLevel.ToString();
        HealthBar.fillAmount = conditionLevel / 100f;
    }
    public void healing (float heal) {
        if (fuelLevel < 100) {
            fuelLevel += heal;
            //conditionTeksti.text = conditionLevel.ToString();
            //HealthBar.fillAmount = conditionLevel / 100f;
            //fuelTeksti.text = fuelLevel.ToString();
            FuelBar.fillAmount = fuelLevel / 100f;
        }
    }

    public float ConditionNow() {
        return conditionLevel;
    }
    public float FuelNow() {
        return fuelLevel;
    }

    
}
