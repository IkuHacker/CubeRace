using UnityEngine;

public class PickUpSphera : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player")) 
        {
            SpheraGenerator.instance.spheraCoint++;
            Destroy(gameObject);
        }
        else 
        {
            Destroy(gameObject);

        }


    }
}
