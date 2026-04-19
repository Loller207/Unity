using System.Collections;
using UnityEngine;

public class PigeonMove : MonoBehaviour
{
    [Header("Limiti Area di Camminata")]
    public float rangeMovimento = 4f;

    [Header("Confini del Mondo (World Bounds)")]
    // Centro: (-168, 0, -37.7) | Range: X +/- 19, Z +/- 10
    private float minX = -168f - 19f; // -187
    private float maxX = -168f + 19f; // -149
    private float minZ = -37.7f - 10f; // -47.7
    private float maxZ = -37.7f + 10f; // -27.7

    [Header("Parametri Fisici")]
    public float velocitaCamminata = 2f;
    public float velocitaRotazione = 10f;
    public float tempoAttesaMin = 1f;
    public float tempoAttesaMax = 3f;

    private Vector3 targetPunto;
    private bool staCamminando = false;

    void Start()
    {
        // Posiziona il piccione a terra (Y=0) e cerca la prima destinazione
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        ScegliNuovaDestinazione();
    }

    void Update()
    {
        if (staCamminando)
        {
            MuoviPiccione();
        }
    }

    void MuoviPiccione()
    {
        Vector3 direzione = (targetPunto - transform.position).normalized;
        if (direzione != Vector3.zero)
        {
            Quaternion rotazioneTarget = Quaternion.LookRotation(new Vector3(direzione.x, 0, direzione.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotazioneTarget, velocitaRotazione * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPunto, velocitaCamminata * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPunto) < 0.1f)
        {
            StartCoroutine(PausaERicomincia());
        }
    }

    void ScegliNuovaDestinazione()
    {
        // 1. Calcola uno spostamento casuale rispetto alla posizione attuale
        float randX = Random.Range(-rangeMovimento, rangeMovimento);
        float randZ = Random.Range(-rangeMovimento, rangeMovimento);

        float nuovaX = transform.position.x + randX;
        float nuovaZ = transform.position.z + randZ;

        // 2. APPLICA I LIMITI (CLAMPS)
        // Questo assicura che il punto scelto sia SEMPRE dentro i bordi del mondo
        nuovaX = Mathf.Clamp(nuovaX, minX, maxX);
        nuovaZ = Mathf.Clamp(nuovaZ, minZ, maxZ);

        targetPunto = new Vector3(nuovaX, transform.position.y, nuovaZ);
        staCamminando = true;
    }

    IEnumerator PausaERicomincia()
    {
        staCamminando = false;
        float tempoPausa = Random.Range(tempoAttesaMin, tempoAttesaMax);
        yield return new WaitForSeconds(tempoPausa);
        ScegliNuovaDestinazione();
    }
}