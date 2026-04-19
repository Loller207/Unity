using UnityEngine;

public class Proximity : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Materiali")]
    [SerializeField] private Material materiale0;
    [SerializeField] private Material materiale1;
    [SerializeField] private Material materiale2;
    [SerializeField] private Material materiale3;
    [SerializeField] private Material materiale4;

    [Header("Oggetti Forza")]
    [SerializeField] private GameObject forza1;
    [SerializeField] private GameObject forza2;
    [SerializeField] private GameObject forza3;
    [SerializeField] private GameObject forza4;

    // Cache dei renderer per ottimizzare le prestazioni
    private Renderer rend1, rend2, rend3, rend4;
    private GameObject[] pidgeons;

    private void Awake()
    {
        pidgeons = GameObject.FindGameObjectsWithTag("fake");

        // Inizializziamo i riferimenti ai renderer
        rend1 = forza1.GetComponent<Renderer>();
        rend2 = forza2.GetComponent<Renderer>();
        rend3 = forza3.GetComponent<Renderer>();
        rend4 = forza4.GetComponent<Renderer>();
    }

    void Update()
    {
        GameObject target = Nearest(pidgeons, player);

        if (target != null && player != null)
        {
            // Calcolo distanza (ignorando l'altezza Y come nel tuo codice originale)
            float dist = Distance(player.transform.position, target.transform.position) / 7f;
            Recolor(dist);
        }
        else
        {
            // Se non ci sono più target validi, resettiamo i colori a materiale0
            ResetColors();
        }
    }

    private void Recolor(float distance)
    {
        // Prima resettiamo tutto al colore base
        ResetColors();

        // Applichiamo i materiali in base alla vicinanza (Cascata)
        if (distance <= 4) rend1.material = materiale1;
        if (distance <= 3) rend2.material = materiale2;
        if (distance <= 2) rend3.material = materiale3;
        if (distance <= 1) rend4.material = materiale4;
    }

    private void ResetColors()
    {
        rend1.material = materiale0;
        rend2.material = materiale0;
        rend3.material = materiale0;
        rend4.material = materiale0;
    }

    private GameObject Nearest(GameObject[] group, GameObject centre)
    {
        if (group == null || group.Length == 0) return null;

        GameObject nearestCandidate = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = centre.transform.position;

        foreach (GameObject candidate in group)
        {
            // --- MODIFICA CRUCIALE ---
            // Se l'oggetto è stato distrutto, sarà 'null'. Lo saltiamo.
            if (candidate == null) continue;

            float distTarget = (candidate.transform.position - currentPos).sqrMagnitude;

            if (distTarget < minDistance)
            {
                minDistance = distTarget;
                nearestCandidate = candidate;
            }
        }

        return nearestCandidate;
    }

    private float Distance(Vector3 centre, Vector3 target)
    {
        // Calcola la distanza piatta (solo X e Z)
        return Vector2.Distance(new Vector2(centre.x, centre.z), new Vector2(target.x, target.z));
    }
}