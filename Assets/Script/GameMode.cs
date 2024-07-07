using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public void EndlessRunner()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void InfiniteRunner()
    {
        SceneManager.LoadScene("InfiniteLevel");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
