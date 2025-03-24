using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<SCR_Level> levelList = new List<SCR_Level>();
    [SerializeField] GameObject levelButtonPrefab;
    [SerializeField] Transform grid;

    [SerializeField] Button exitButton;
    void Start()
    {
        exitButton.onClick.AddListener(ExitLevel);
        FillGrid();
    }

    void FillGrid()
    {
        foreach(Transform child in grid)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < levelList.Count; i++)
        {
            GameObject newButton = Instantiate(levelButtonPrefab,grid,false);
            LevelButton lButton = newButton.GetComponent<LevelButton>();
            lButton.SetButton(levelList[i].size, levelList[i].imagePuzzle, levelList[i].previewImage, i+1);
        }
    }

    void ExitLevel()
    {
        SceneManager.LoadScene("Menu");
    }
}
