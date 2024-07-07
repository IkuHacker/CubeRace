using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObstacleGenretor : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject plateformPrefab;
    public GameObject plateformInitial;
    public GameObject distanecCalculatorObject;
    public Transform player;
    public Transform plateformTransform;
    public Transform playerSpawn;

    private int obstacleCount;
    public float maxTotalSizeX = 8f;
    private int countboucle;
    private float posZ;
    public float spaceBetweenObstacle;
    public int seed;

    public bool isEndlessRunner;

    private List<GameObject> plateformList = new List<GameObject>();
    public int maxPlateformCount;

    private List<GameObject> obstacleList = new List<GameObject>();
    public int maxObstacleCount;

    void Start()
    {
        seed = StateNameController.seeds;
        if (isEndlessRunner) 
        {
            player.position = playerSpawn.position;
            posZ = player.position.z + 20f;
            plateformInitial.SetActive(true);
            GenerateObstacles(seed);
            distanecCalculatorObject.SetActive(true);
        }
        else 
        {
            posZ = player.position.z + 20f;
            plateformInitial.SetActive(false);
            StartCoroutine(GenerateInfinitePlateform());
            StartCoroutine(GenerateInfiniteObstacle());
            distanecCalculatorObject.SetActive(false);

        }



    }

   
    public IEnumerator GenerateInfinitePlateform() 
    {

        while (true) 
        {
            countboucle++;
            float zposition = countboucle * plateformTransform.localScale.z;
            plateformList.Add(Instantiate(plateformPrefab, new Vector3(0f, 0f, zposition), Quaternion.identity));
            if(plateformList.Count > maxPlateformCount) 
            {
                Destroy(plateformList[0]);
                plateformList.RemoveAt(0);
            }
            yield return new WaitForSeconds(60f);
        }
        
    }



    public IEnumerator GenerateInfiniteObstacle()
    {

        while (true)
        {

            for (int i = 0; i < 100; i++)
            {
                float sizeX1 = Random.Range(1f, maxTotalSizeX - 1.5f);
                float gap = maxTotalSizeX - sizeX1;
                float sizeX2 = Random.Range(2f, gap);

                float posX1 = (10.0f - sizeX1) / 2.0f;
                float posX2 = -(10.0f - sizeX2) / 2.0f;

                int floor = Random.Range(0, 3);

                obstacleList.Add(CreateInfiniteObstacle(posX1, sizeX1, posX2, sizeX2, floor, "Obstacle" + i, posZ));

                posZ += spaceBetweenObstacle;
            }

            if (obstacleList.Count > maxObstacleCount)
            {
                for (int i = 0; i < 500; i++)
                {
                    Destroy(obstacleList[i]);
                    obstacleList.RemoveAt(i);
                }

            }


            yield return new WaitForSeconds(60f);
        }

    }

    void GenerateObstacles(int obstacleSeed)
    {
        obstacleCount = Mathf.RoundToInt((plateformTransform.localScale.z - 60f) / 20f);
        Random.InitState(obstacleSeed);
        for (int i = 0; i < obstacleCount; i++)
        {
            float sizeX1 = Random.Range(1f, maxTotalSizeX - 1.5f);
            float gap = maxTotalSizeX - sizeX1;
            float sizeX2 = Random.Range(2f, gap);

            float posX1 = (10.0f - sizeX1) / 2.0f;
            float posX2 = -(10.0f - sizeX2) / 2.0f;

            int floor = Random.Range(0, 3);

            CreateObstacle(posX1, sizeX1, posX2, sizeX2, floor, "Obstacle" + i, posZ);

            posZ += spaceBetweenObstacle;
        }
    }

    void CreateObstacle(float posX, float sizeX, float posX1, float sizeX1,  float floor, string parentName, float posZ)
    {

        if (floor == 1)
        {
            GameObject parent = new GameObject(parentName);
            GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, floor, posZ), Quaternion.identity);
            cube1.transform.localScale = new Vector3(sizeX, 1, 1);
            cube1.transform.parent = parent.transform;

            GameObject cube2 = Instantiate(cubePrefab, new Vector3(posX1, floor, posZ), Quaternion.identity);
            cube2.transform.localScale = new Vector3(sizeX1, 1, 1);
            cube2.transform.parent = parent.transform;


        }
        else if(floor == 2) 
        {
            GameObject parent = new GameObject(parentName);
            GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, floor, posZ), Quaternion.identity);
            cube1.transform.localScale = new Vector3(sizeX, 1, 1);
            cube1.transform.parent = parent.transform;

            GameObject cube2 = Instantiate(cubePrefab, new Vector3(posX1, floor, posZ), Quaternion.identity);
            cube2.transform.localScale = new Vector3(sizeX1, 1, 1);
            cube2.transform.parent = parent.transform;

            GameObject bar1 = Instantiate(cubePrefab, new Vector3(0, 1, posZ), Quaternion.identity);
            bar1.transform.localScale = new Vector3(10, 1, 1);
            bar1.transform.parent = parent.transform;
        }
        else if (floor == 0) 
        {
            GameObject parent = new GameObject(parentName);
            GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, 1, posZ), Quaternion.identity);
            cube1.transform.localScale = new Vector3(sizeX, 1, 1);
            cube1.transform.parent = parent.transform;

            GameObject cube2 = Instantiate(cubePrefab, new Vector3(posX1, 1, posZ), Quaternion.identity);
            cube2.transform.localScale = new Vector3(sizeX1, 1, 1);
            cube2.transform.parent = parent.transform;

            GameObject bar1 = Instantiate(cubePrefab, new Vector3(0, 2, posZ), Quaternion.identity);
            bar1.transform.localScale = new Vector3(10, 1, 1);
            bar1.transform.parent = parent.transform;
        }
    }

    GameObject CreateInfiniteObstacle(float posX, float sizeX, float posX1, float sizeX1, float floor, string parentName, float posZ)
    {
        GameObject parent = new GameObject(parentName);
        if (floor == 1)
        {
            GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, floor, posZ), Quaternion.identity);
            cube1.transform.localScale = new Vector3(sizeX, 1, 1);
            cube1.transform.parent = parent.transform;

            GameObject cube2 = Instantiate(cubePrefab, new Vector3(posX1, floor, posZ), Quaternion.identity);
            cube2.transform.localScale = new Vector3(sizeX1, 1, 1);
            cube2.transform.parent = parent.transform;


        }
        else if (floor == 2)
        {
            GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, floor, posZ), Quaternion.identity);
            cube1.transform.localScale = new Vector3(sizeX, 1, 1);
            cube1.transform.parent = parent.transform;

            GameObject cube2 = Instantiate(cubePrefab, new Vector3(posX1, floor, posZ), Quaternion.identity);
            cube2.transform.localScale = new Vector3(sizeX1, 1, 1);
            cube2.transform.parent = parent.transform;

            GameObject bar1 = Instantiate(cubePrefab, new Vector3(0, 1, posZ), Quaternion.identity);
            bar1.transform.localScale = new Vector3(10, 1, 1);
            bar1.transform.parent = parent.transform;
        }
        else if (floor == 0)
        {
            GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, 1, posZ), Quaternion.identity);
            cube1.transform.localScale = new Vector3(sizeX, 1, 1);
            cube1.transform.parent = parent.transform;

            GameObject cube2 = Instantiate(cubePrefab, new Vector3(posX1, 1, posZ), Quaternion.identity);
            cube2.transform.localScale = new Vector3(sizeX1, 1, 1);
            cube2.transform.parent = parent.transform;

            GameObject bar1 = Instantiate(cubePrefab, new Vector3(0, 2, posZ), Quaternion.identity);
            bar1.transform.localScale = new Vector3(10, 1, 1);
            bar1.transform.parent = parent.transform;
        }
        return parent;
    }
}
