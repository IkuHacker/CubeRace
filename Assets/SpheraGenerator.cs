using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheraGenerator : MonoBehaviour
{
    public int spheraCoint;
    public GameObject spheraPrefab;
    public List<float> eachPosObstacleZ = new List<float>();

    public static SpheraGenerator instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SpheraGenerator dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            eachPosObstacleZ.Add(ObstacleGenretor.instance.posZ + (20f * i));
            float Xpos = Random.Range(-4, 4);
            Instantiate(spheraPrefab, new Vector3(Xpos, 1, eachPosObstacleZ[i] + 1), Quaternion.identity);
            Instantiate(spheraPrefab, new Vector3(Xpos, 2, eachPosObstacleZ[i]), Quaternion.identity);


        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
