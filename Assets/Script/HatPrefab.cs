using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatPrefab : MonoBehaviour
{
    public Vector3 position;
    public Vector3 scale;

    void Start()
    {
        transform.position = position;
        transform.localScale = scale;

    }


}
