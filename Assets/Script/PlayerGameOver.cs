using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Paramètres du joueur")]
    public GameObject parent;
    public GameObject player;
    public GameObject cameara;
    [Header("GameOver Object")]
    public GameObject GameOverPanel;
    public GameObject GameCompletePanel;
    public Text scoreGameOver;
    public Text highScoreGameOver;
    public Text scoreGameComplete;
    public Text highScoreGameComplete;
    public Text DistanceGameOver;
    public Text highDistanceGameOver;
    public Text coinCount;
    public Text coinCountComplete;

    [Header("Paramètres du cube")]
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    [Header("Parametre d'explosion")]
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    [Header("Materiel")]
    public Transform hatPoint;

    public bool isEndlessRunner;

    float cubesPivotDistance;
    Vector3 cubesPivot;
    Renderer rend;
    void Start()
    {
        Renderer playerRend = player.GetComponent<Renderer>();
        playerRend.enabled = true;
        playerRend.sharedMaterial = StateNameController.currentMaterialEquiped ;

        GameObject hatObject = Instantiate(StateNameController.currentHatEquiped, hatPoint.position, Quaternion.identity);
        hatObject.transform.parent = hatPoint;
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StateNameController.spheraCount += SpheraGenerator.instance.spheraCoint;

            if (isEndlessRunner)
            {
                ScoreDistanceManager.instance.GameOver();
                SavedSystem.instance.SaveDataCoin();

            }
            else
            {
                ScoreManager.instance.GameOver();
                SavedSystem.instance.SaveDataCoin();
            }

           
            parent.transform.position = player.transform.position;
            cameara.transform.parent = parent.transform;
            explode();
            GameOverPanel.SetActive(true);

            if (isEndlessRunner) 
            {

                
                DistanceGameOver.text = "DISTANCE:  " + ScoreDistanceManager.instance.distance + "m";
                highDistanceGameOver.text = ScoreDistanceManager.instance.bestDistance.ToString();

            }
            else
            {
                scoreGameOver.text = "SCORE: " + ScoreManager.instance.score;
                highScoreGameOver.text = ScoreManager.instance.bestScore.ToString();
            }


            coinCount.text = "SPHERA: " + SpheraGenerator.instance.spheraCoint.ToString();
            
        }

        if (collision.gameObject.CompareTag("Portal"))
        {
            StartCoroutine(SpeedPause());
            ScoreDistanceManager.instance.GameOver();
            DistanceGameOver.text = "DISTANCE:  " + ScoreDistanceManager.instance.distance + "m";
            highDistanceGameOver.text = ScoreDistanceManager.instance.bestDistance.ToString();
            GameCompletePanel.SetActive(true);
            coinCountComplete.text = "SPHERA: " + SpheraGenerator.instance.spheraCoint.ToString();
            StateNameController.spheraCount += SpheraGenerator.instance.spheraCoint;
            SavedSystem.instance.SaveDataCoin();


        }
    }

    public void explode()
    {
        //make object disappear
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
           
            }
        }

    }

    void createPiece(int x, int y, int z)
    {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        rend = piece.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = StateNameController.currentMaterialEquiped;
    }

    IEnumerator SpeedPause() 
    {
        for (int i = 0; i < 100f; i++)
        {
            yield return new WaitForSeconds(0.05f);
            Time.timeScale = Time.timeScale - 0.01f;
        }
    }

}
