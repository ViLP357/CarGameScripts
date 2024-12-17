using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsTriggers : MonoBehaviour
{
    private float viimePortti;
    private float viimepaketti;
    private float lastCrash;
    private float lastHeal;
    private float voima;
    private Portcontroller portscript;

    private void Start() {
        portscript = GameObject.FindObjectOfType<Portcontroller>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "port") {
            if (Time.time - viimePortti > 0.5f) {
                //Debug.Log("Trigger");
                Pisteet.instanssi.Lisää(1);
                viimePortti = Time.time;

                Portcontroller.instanssi.Tuhoa(other);
            }
        }
        else if (other.transform.tag == "maali") {
            if (Pisteet.instanssi.PisteidenMaara() == portscript.porttienMaara() && Pisteet.instanssi.PakettienMaara() == portscript.pakettienMaara()) {
                //Portcontroller.instanssi.Voitto();
                portscript.Voitto();
                ValikkoManager.instanssi.AvaaVoittoValikko();
            }
        }
        else if (other.transform.tag == "pakettipaikka") {
            if (Time.time - viimepaketti > 0.5f) {
                Pisteet.instanssi.PakettiHaettu(1);
                viimepaketti = Time.time;
                //Debug.Log("pakettipaikka");
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "pakettipaikka") {
            Portcontroller.instanssi.Tuhoa(other);
        }
    }
    private void OnCollisionEnter (Collision collision) {
        if (collision.transform.tag == "seina") {
            voima = collision.relativeVelocity.magnitude;
            if (Time.time - lastCrash > 0.5f) {  
                Debug.Log(voima);
                if (voima > 4) {
                    //Debug.Log("Luja törmäys");
                    AutonKunto.instanssi.crashHit(Mathf.Round(voima) * 10);
                }
                lastCrash = Time.time;
            }            
        }
    }
    private void OnTriggerStay (Collider other) {
        if (other.transform.tag == "healstation") {
            //Debug.Log("vyöhykkeellä");
            if (Time.time - lastHeal > 1f) {
                AutonKunto.instanssi.healing(1);
                lastHeal = Time.time;
            }
        }
    }
}   
