using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFollow : MonoBehaviour

{
    private TransparentObject _fader;
    public Transform player;
    Vector3 etäisyys;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject player = GameObject.Find("CarPlayer");
        etäisyys = transform.position - player.position;
        
    }

    // Update is called once per frame
    void Update() {
    transform.position = player.position + etäisyys;

    if (player != null) {
        Vector3 dir = player.transform.position - transform.position;
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider == null) {
                // Ei osumia, nollaa edellinen _fader
                if (_fader != null) {
                    _fader.DoFade = false;
                    _fader = null; // Nollaa viite

                }
                return;

            }
            if (hit.collider.gameObject == player) {
                // Jos pelaaja on säteen tiellä, nollaa edellinen _fader
                if (_fader != null) {
                    _fader.DoFade = false;
                    _fader = null; // Nollaa viite
         
                }
            }
            else {
                // Osuma ei ole pelaaja, tee objektista läpinäkyvä
                if (_fader != null && _fader != hit.collider.gameObject.GetComponent<TransparentObject>()) {
                    // Jos edellinen _fader on eri objekti, palauta sen näkyvyys
                    _fader.DoFade = false;
                }
                _fader = hit.collider.gameObject.GetComponent<TransparentObject>();
                // yläpuolella löydetään oikea objekti
                if (_fader != null) {
                    _fader.DoFade = true;
    
                }
                else {
                    Debug.Log("ei osu4");
                    Debug.Log($"Raycast hit object: {hit.collider.gameObject.name}");

                    //
                }
            }
        } else {
            // Ei osumia, nollaa edellinen _fader
            if (_fader != null) {
                _fader.DoFade = false;
                _fader = null; // Nollaa viite
  
            }
        }
    }
}

}
