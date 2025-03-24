using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    Vector2Int size;
    Texture2D puzzleImage;
    [SerializeField] Image previewImage;
    [SerializeField] TMP_Text numberText;

    public void OnPointerClick(PointerEventData eventData)
    {
       LevelData.levelSize = size;
       LevelData.imageToPuzzle = puzzleImage;

       SceneManager.LoadScene("Game");


    }

    public void SetButton(Vector2Int levelSize, Texture2D _puzzleImage, Sprite previewSprite, int levelNumber)
    {
        size = levelSize;
        puzzleImage = _puzzleImage;
        previewImage.sprite = previewSprite;
        numberText.text = levelNumber.ToString();
    }


}
