using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotinAmpuminen : MonoBehaviour
{
    public LineRenderer[] tykit;
    public float ampumisLujuus = 50f;
    public float ampumisEtäisyys = 12f;
    public float ampumisNopeus = 0.25f;
    public float laserSäteenNopeus = 50f;
    float viimeAmpuminen;
    int ampumistenLukumäärä;
    float attackRange = 10f;
    public LayerMask whatIsPlayer;
    public Transform player;
    public bool playerInAttackRange;
    //AudioSource ääni;
    //public AudioClip[] Ampumisäänet;
    //private Napinpainallus nappi;

    void Start() {
        player = GameObject.Find("CarPlayer").transform;
        //ääni = GetComponent<AudioSource>();
        //nappi = GameObject.FindObjectOfType<Napinpainallus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) {
            return;
        }
        //if (Input.GetMouseButton(0) && Time.time - viimeAmpuminen >= ampumisNopeus) {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange && Time.time - viimeAmpuminen >= ampumisNopeus) {
            Debug.Log("ammutaan");
            Ammu();
        }
        SäteidenLento();
    }

    void Ammu() {
        //ääni.clip = Ampumisäänet[Random.Range(0, Ampumisäänet.Length)];
        //ääni.Play();
        //LineRenderer tykki = tykit[++ampumistenLukumäärä % 2];
        LineRenderer tykki = tykit[0];
        Ray säde = new Ray(tykki.transform.position, tykki.transform.forward);
        RaycastHit osuma;
        Vector3 päätePiste;
        if (Physics.Raycast(säde, out osuma, ampumisEtäisyys)) {
            päätePiste = osuma.point;
            Debug.Log("osuu");
            Debug.DrawRay(säde.origin, säde.direction * osuma.distance, Color.red, 1.0f);
            if (osuma.transform.CompareTag("Player")) {
                Debug.Log("osiu pelaajaan");
                //osuma.transform.GetComponent<AutonKunto>().Elämäpisteet -= ampumisLujuus; 
            }
            //if (osuma.transform.CompareTag("Maalitaulu")) {
            //    osuma.transform.GetComponent<Napinpainallus>().Ammuttu();
            //    Debug.Log("Maaliin osuttiin");
            //}
        }
        else {
            päätePiste = säde.origin + säde.direction * ampumisEtäisyys;
        }
        tykki.SetPositions(new Vector3[] { säde.origin, päätePiste });
        tykki.enabled = true;
        viimeAmpuminen = Time.time;
    }
    void SäteidenLento() {
        foreach (LineRenderer tykki in tykit) {
            if (!tykki.enabled)
                continue;
            Vector3[] pisteet = new Vector3[2];
            tykki.GetPositions(pisteet);
            pisteet[1] = Vector3.MoveTowards(pisteet[1], pisteet[0], Time.deltaTime * laserSäteenNopeus);
            tykki.SetPositions(pisteet);
            if (Vector3.Distance(pisteet[0], pisteet[1]) < 1f) {
                tykki.enabled = false;
            }

        }
    }
}
