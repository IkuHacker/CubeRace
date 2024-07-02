using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Assurez-vous d'attacher un objet Text depuis l'interface Unity
    public bool isGameOver = false;
    public int score = 0;
    public float scoreSpeed = 0.05f;
    public GameObject newHighScore;
    public GameObject newHighScoreComplete;

    public int bestScore;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ScoreManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        // Charger le meilleur score depuis PlayerPrefs
        newHighScore.SetActive(false);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);



        // Commence à mettre à jour le score seulement si le jeu n'est pas terminé
        if (!isGameOver)
        {
            InvokeRepeating("UpdateScore", 0.0f, scoreSpeed);
        }
    }

    void UpdateScore()
    {
        score++;
        // Met à jour le texte affichant le score
        scoreText.text = score.ToString();
    }

    // Appelé lorsque le jeu est terminé (Game Over)
    public void GameOver()
    {
        isGameOver = true;
        // Arrête la répétition de l'invocation
        CancelInvoke("UpdateScore");

        // Mettre à jour le meilleur score s'il est dépassé
        if (score > bestScore)
        {
            bestScore = score;
            newHighScore.SetActive(true);
            newHighScoreComplete.SetActive(true);
            // Enregistrer le nouveau meilleur score dans PlayerPrefs
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // Il est important de sauvegarder les PlayerPrefs après chaque modification
        }
    }
}
