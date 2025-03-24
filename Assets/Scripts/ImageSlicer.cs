using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ImageSlicer : MonoBehaviour
{
    [SerializeField] Texture2D sourceImage;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform puzzleContainer;
    GridLayoutGroup gridLayoutGroup;
    int tileWidth, tileHeight;

    //void Start()
    //{
    //    SliceImage(3, 3);
    //}

    public void SliceImage(int rows,int cols, Texture2D imageToSlice)
    {
        if(imageToSlice != null)
        {
            sourceImage = imageToSlice;
        }

        gridLayoutGroup = puzzleContainer.GetComponent<GridLayoutGroup>();

        float spacingX = gridLayoutGroup.spacing.x;
        float spacingY = gridLayoutGroup.spacing.y;

        tileWidth = Mathf.FloorToInt((sourceImage.width - spacingX * (cols-1))/cols);
        tileHeight = Mathf.FloorToInt((sourceImage.width - spacingY * (rows - 1)) / rows);

        for (int y = 0; y < rows; y++)
        {
            for(int x = 0; x < cols; x++)
            {
                int correctedY = (rows - 1) - y;
                if(x ==cols-1 && y == rows - 1)
                {
                    continue;
                }

                CreateTile(x,correctedY);
            }
        }
        AdjustGridCellSize(rows, cols);
    }

    void CreateTile(int x, int y)
    {
        Texture2D tileTexture = new Texture2D(tileWidth, tileHeight);
        tileTexture.SetPixels(sourceImage.GetPixels(x*tileWidth,y*tileHeight,tileWidth,tileHeight));
        tileTexture.Apply();

        Sprite tileSprite = Sprite.Create(tileTexture, new Rect(0, 0, tileWidth, tileHeight), new Vector2(0.5f, 0.5f));

        GameObject tile = Instantiate(tilePrefab, puzzleContainer);
        tile.name = x + "," + y;
        tile.GetComponent<Image>().sprite = tileSprite; 

    }

    void AdjustGridCellSize(int rows,  int cols)
    {
        RectTransform containerRect = puzzleContainer.GetComponent<RectTransform>();

        float spacingX = gridLayoutGroup.spacing.x;
        float spacingY = gridLayoutGroup.spacing.y;

        float availableWidth = containerRect.rect.width - (spacingX * (cols - 1));
        float availableHeight = containerRect.rect.height - (spacingY * (rows - 1));

        float cellWidth = availableWidth / cols;
        float cellHeight = availableHeight / rows;

        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
