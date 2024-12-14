using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    public Transform [] syntypaikat;
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        placeMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void placeMonsters() {
        for (int i = 0; i < syntypaikat.Length; i++) {
            Transform kohta = syntypaikat[i];

            Instantiate(monster, kohta.position, monster.transform.rotation);
            Debug.Log(kohta.position);
        }
    }
}
