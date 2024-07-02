using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int obstacleCount;
    public float maxTotalSizeX = 8f;
    public float posZ;
    public float spaceBetweenObstacle;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            float sizeX1 = Random.Range(1f, maxTotalSizeX - 1.5f);
            float gap = maxTotalSizeX - sizeX1;
            float sizeX2 = Random.Range(2f, gap);

            float posX1 = (10.0f - sizeX1) / 2.0f;
            float posX2 = -(10.0f - sizeX2) / 2.0f;

            int floor = Random.Range(1, 3);

            CreateObstacle(posX1, sizeX1, floor, "Obstacle" + i, posZ);
            CreateObstacle(posX2, sizeX2, floor, "Obstacle" + i, posZ);

            posZ += spaceBetweenObstacle;
        }
    }

    void CreateObstacle(float posX, float sizeX, float floor, string parentName, float posZ)
    {
        GameObject parent = new GameObject(parentName);
        GameObject cube1 = Instantiate(cubePrefab, new Vector3(posX, floor, posZ), Quaternion.identity);
        cube1.transform.localScale = new Vector3(sizeX, 1, 1);
        cube1.transform.parent = parent.transform;

        if (floor - 1 == 1)
        {
            GameObject bar1 = Instantiate(cubePrefab, new Vector3(0, 1, posZ), Quaternion.identity);
            bar1.transform.localScale = new Vector3(10, 1, 1);
            bar1.transform.parent = parent.transform;
        }
    }
}
