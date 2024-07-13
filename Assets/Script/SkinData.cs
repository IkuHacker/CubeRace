using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skin", menuName = "Skin/New skin")]
public class SkinData : ScriptableObject
{
    public string nameSkin;
    public int priceSkin;
    public Sprite skinSprite;
    public Material skinMaterial;
}
