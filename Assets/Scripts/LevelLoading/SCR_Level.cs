using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Sliding Puzzle/Level")]
public class SCR_Level : ScriptableObject
{
    public Vector2Int size = new Vector2Int(3,3);
    public Texture2D imagePuzzle;
    public Sprite previewImage;
}
