using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDistanceManager : MonoBehaviour
{

    public bool isGameOver = false;
    public float distance = 0;
    public GameObject newHighScore;
    public Transform plateform;

    public float bestDistance;

    public static ScoreDistanceManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ScoreDistanceManager dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        newHighScore.SetActive(false);
        bestDistance = PlayerPrefs.GetFloat("BestDistance", 0);
    }

    private void Update()
    {

        float valeurArrondie = Mathf.Round(DistanceCalculator.instance.distanceToPortal);
        distance = plateform.localScale.z - valeurArrondie;

       
    }
    public void GameOver()
    {
        isGameOver = true;

        if (distance > bestDistance)
        {
            bestDistance = distance;
            newHighScore.SetActive(true);
            PlayerPrefs.SetFloat("BestDistance", bestDistance);
            PlayerPrefs.Save();
        }
    }
}
