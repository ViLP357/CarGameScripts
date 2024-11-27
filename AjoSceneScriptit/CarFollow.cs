using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFollow : MonoBehaviour

{
    public Transform player;
    Vector3 etäisyys;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("CarPlayer");
        etäisyys = transform.position - player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position.z - 5);
        transform.position = player.position +  etäisyys;
        
    }
}
