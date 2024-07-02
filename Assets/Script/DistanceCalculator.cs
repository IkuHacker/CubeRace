using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator : MonoBehaviour
{
    public Transform endOfLevel;
    public LayerMask obstacleLayer;
    public Slider slider;
    public Transform plateform;
    public float distanceToPortal;
    public static DistanceCalculator instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DistanceCalculator dans la scène");
            return;
        }

        instance = this;
    }


    public void Update()
    {
        // Calcul de la distance entre la position actuelle du joueur et la fin du niveau
        distanceToPortal = Vector3.Distance(transform.position, endOfLevel.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (endOfLevel.position - transform.position).normalized, out hit, Mathf.Infinity, obstacleLayer))
        {
            // Vérifiez si le rayon a touché un obstacle avant d'atteindre la fin du niveau
            if (hit.collider.CompareTag("Portal"))
            {
                // Utilisez la distance au portail comme référence pour ajuster la valeur du curseur
                float nbDivisible = plateform.localScale.z / 100;
                slider.value = 100 - distanceToPortal / nbDivisible;
                
            }
        }
    }
}
