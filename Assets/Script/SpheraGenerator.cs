using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheraGenerator : MonoBehaviour
{
    public int spheraCoint;
    public GameObject spheraPrefab;
    public Transform player;
    private int spheraCount;
    public int seed;

    private ObstacleGenretor obstacleGenretor;

    private List<GameObject> spheraList = new List<GameObject>();
    public int maxSpheraList;

    public static SpheraGenerator instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SpheraGenerator dans la scène");
            return;
        }

        instance = this;

        obstacleGenretor = ObstacleGenretor.instance;
    }
    void Start()
    {

        spheraCount = (int)(ObstacleGenretor.instance.obstacleCount * 1.5f);

        seed = StateNameController.seeds;

        if (obstacleGenretor.isEndlessRunner)
        {
            GenerateSphera(seed);
        }
        else
        {
            StartCoroutine(GenerateInfiniteSphera());

        }
       
       



    }
    public void GenerateSphera(int spheraSeed) 
    {
        Random.InitState(spheraSeed);
        float posZ = player.position.z + 30;
        for (int i = 0; i < spheraCount; i++)
        {
            int spheraCount = Random.Range(1, 3);
            for (int index = 0; index < spheraCount; index++)
            {
                int spheraGap = Random.Range(1, 30);

                posZ += spheraGap;

                float Xpos = Random.Range(-4, 4);

                Instantiate(spheraPrefab, new Vector3(Xpos, 1, posZ + index), Quaternion.identity);

            }

        }
    }
    public IEnumerator GenerateInfiniteSphera()
    {
        float posZ = player.position.z;

        while (true)
        {
            
            for (int i = 0; i < 50; i++)
            {
                int spheraCount = Random.Range(1, 7);
                for (int index = 0; index < spheraCount; index++)
                {
                    int spheraGap = Random.Range(1, 30);

                    posZ += spheraGap;

                    float Xpos = Random.Range(-4, 4);

                    spheraList.Add(Instantiate(spheraPrefab, new Vector3(Xpos, 1, posZ + index), Quaternion.identity));

                }

            }

            if (spheraList.Count > maxSpheraList)
            {
                for (int i = 0; i < 500; i++)
                {
                    Destroy(spheraList[i]);
                    spheraList.RemoveAt(i);
                }

            }

            yield return new WaitForSeconds(60f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
