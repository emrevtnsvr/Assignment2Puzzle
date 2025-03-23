using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int rows = 3;
    [SerializeField] int columns = 3;

    [SerializeField] Transform puzzleContainer;

    int[,] grid;
    Vector2Int emptyTile;
    ImageSlicer imageSlicer;
    List<Tile> tiles = new List<Tile>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        imageSlicer = GetComponent<ImageSlicer>();

        imageSlicer.SliceImage(rows, columns);

        InitializeGrid();

        Invoke("DisableGridLayout", 0.5f);
    }

    void DisableGridLayout()
    {
        puzzleContainer.GetComponent<GridLayoutGroup>().enabled = false;
    }

   void InitializeGrid()
    {
        grid = new int [rows , columns];
        int number = 1;
        Tile[] tileObject = puzzleContainer.GetComponentsInChildren<Tile>();

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if(x==columns -1 && y == rows - 1)
                {
                    grid[x, y] = 0;
                    emptyTile = new Vector2Int(x, y);
                    continue;
                }
                grid[x, y] = number;
                int index = number - 1;
                
                if(index < tileObject.Length)
                {
                    tileObject[index].GetComponentInChildren<TMP_Text>().text = number.ToString();

                    tileObject[index].Init(new Vector2Int(x, y));

                    tiles.Add(tileObject[index]);
                }
                number++;
            }
        }
        PrintGrid();
    } 

    public void TryMoveTile(Tile tile)
    {
        if (IsAdjacent(tile.GridPosition))
        {
            StartCoroutine(SwapTiles(tile, false));
        }
    } 

    bool IsAdjacent(Vector2Int tilePos)
    {
        int dx = Mathf.Abs(tilePos.x - emptyTile.x);
        int dy = Mathf.Abs(tilePos.y - emptyTile.y);
        return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
    }

    IEnumerator SwapTiles(Tile tile, bool isShuffleMove)
    {
        Vector2Int previewsEmpty = emptyTile;
        emptyTile = tile.GridPosition;

        Vector2 targetPosition = GetPositionForGrid(previewsEmpty);

        if (isShuffleMove)
        {
            yield return StartCoroutine(tile.AnimateMove(targetPosition));
        }
        else
        {
            yield return StartCoroutine(tile.AnimateMove(targetPosition,0.001f));
        }

        tile.SetGridPos(previewsEmpty);

        SwapGridPosition(previewsEmpty,emptyTile);

        PrintGrid();

            yield return null;
    }

    Vector2 GetPositionForGrid(Vector2Int gridPos)
    {
       GridLayoutGroup gridLayout = puzzleContainer.GetComponent<GridLayoutGroup>();
       
       Vector2 cellSize = gridLayout.cellSize;
       Vector2 spacing = gridLayout.spacing;

       float startx = cellSize.x/2;
       float starty = -cellSize.y/2;

        float targetX = startx + gridPos.x *(cellSize.x + spacing.x);
        float targetY = starty - gridPos.y * (cellSize.y + spacing.y);
        return new Vector2(targetX, targetY);
    }

    void PrintGrid()
    {
        string s = "";
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x< columns; x++)
            {
                s += grid[x, y] + " ";
                if(x == columns - 1)
                {
                    s += "\n";
                }
            }
            Debug.Log("grid : \n" + s);
        }
    }

    void SwapGridPosition(Vector2Int first, Vector2Int second)
    {
        int temp = grid[first.x, first.y];
        grid[first.x, first.y] = grid[second.x, second.y];
        grid[second.x, second.y] = temp;
    }
}
