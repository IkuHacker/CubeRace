using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "Hat/New hat")]
public class HatData : ScriptableObject
{
    public string nameHat;
    public int priceHat;
    public Sprite hatSprite;
    public GameObject hatPrefab;
}
