using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotinAmpuminen : MonoBehaviour
{
    public LineRenderer[] tykit;
    public float ampumisLujuus = 50f;   //!!
    public float ampumisEtäisyys = 12f;   //!!
    public float ampumisNopeus = 1f;
    public float laserSäteenNopeus = 50f;
    float viimeAmpuminen;
    int ampumistenLukumäärä;
    float attackRange = 10f;
    public LayerMask whatIsPlayer;
    public Transform player;
    public bool playerInAttackRange;
    private AutonKunto autoscripti;
    //AudioSource ääni;
    //public AudioClip[] Ampumisäänet;
    //private Napinpainallus nappi;
    private VihollisenOhjaus vihollisenOhjaus;
    void Start() {
        vihollisenOhjaus = GetComponent<VihollisenOhjaus>();
        player = GameObject.Find("CarPlayer").transform;
        autoscripti = GameObject.FindObjectOfType<AutonKunto>();
        if (autoscripti == null) {
        Debug.LogError("AutonKunto-komponenttia ei löytynyt!");
        } else {
            Debug.Log("AutonKunto löydetty!");
        }
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
        //Debug.Log(Time.time - viimeAmpuminen);
        //Debug.Log(ampumisNopeus);
        if ( vihollisenOhjaus.onkoKuollut() == false && Time.time - viimeAmpuminen >= ampumisNopeus && playerInAttackRange) {
            //Debug.Log("ammutaan");
            Ammu();
        }
        SäteidenLento();
    }

    void Ammu() {
        //ääni.clip = Ampumisäänet[Random.Range(0, Ampumisäänet.Length)];
        //ääni.Play();
        Debug.Log("Ammutaan1");
        LineRenderer tykki = tykit[0];

        Vector3 suunta = (player.position - tykki.transform.position).normalized;
        Ray säde = new Ray(tykki.transform.position, suunta);

        RaycastHit osuma;
        Vector3 päätePiste;
        if (Physics.Raycast(säde, out osuma, ampumisEtäisyys)) {
            päätePiste = osuma.point;
            //Debug.Log("osuu");
            Debug.DrawRay(säde.origin, säde.direction * osuma.distance, Color.red, 1.0f);
            if (osuma.transform.CompareTag("Player")) {
                //Debug.Log("osiu pelaajaan");
                autoscripti.crashHit(1f);
            }
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
