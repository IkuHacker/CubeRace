using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skin", menuName = "Skin/New skin")]
public class SkinData : ScriptableObject
{
    public int priceSkin;
    public Sprite skinSprite;
    public Material skinMaterial;
}
