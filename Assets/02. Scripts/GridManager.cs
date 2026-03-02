using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    private int[,] grid = new int[4, 4];
    [SerializeField] private float cellSize = 1.2f;

    void Start()
    {
        InitializeGrid();
        SpawnTile();
        SpawnTile();
    }

    void Update()
    {
        bool moved = false;

        // 입력 감지 레이어
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) moved = MoveUp();
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) moved = MoveDown();
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) moved = MoveLeft();
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) moved = MoveRight();

        // 변화가 있을 때만 스폰
        if (moved)
        {
            SpawnTile();
            PrintGrid(); 
        }
    }

    // --- 알고리즘 레이어 (상하좌우 메서드) ---

    public bool MoveUp()
    {
        bool isChanged = false;
        for (int c = 0; c < 4; c++)
        {
            List<int> line = new List<int>();
            for (int r = 0; r < 4; r++) if (grid[r, c] != 0) line.Add(grid[r, c]);

            for (int i = 0; i < line.Count - 1; i++)
            {
                if (line[i] == line[i + 1])
                {
                    line[i] *= 2;
                    line.RemoveAt(i + 1);
                    isChanged = true;
                }
            }

            for (int r = 0; r < 4; r++)
            {
                int newValue = (r < line.Count) ? line[r] : 0;
                if (grid[r, c] != newValue) { grid[r, c] = newValue; isChanged = true; }
            }
        }
        return isChanged;
    }

    public bool MoveDown()
    {
        bool isChanged = false;
        for (int c = 0; c < 4; c++)
        {
            List<int> line = new List<int>();
            for (int r = 3; r >= 0; r--) if (grid[r, c] != 0) line.Add(grid[r, c]);

            for (int i = 0; i < line.Count - 1; i++)
            {
                if (line[i] == line[i + 1])
                {
                    line[i] *= 2;
                    line.RemoveAt(i + 1);
                    isChanged = true;
                }
            }

            for (int r = 0; r < 4; r++)
            {
                int newValue = (r < line.Count) ? line[r] : 0;
                if (grid[3 - r, c] != newValue) { grid[3 - r, c] = newValue; isChanged = true; }
            }
        }
        return isChanged;
    }

    public bool MoveLeft()
    {
        bool isChanged = false;
        for (int r = 0; r < 4; r++)
        {
            List<int> line = new List<int>();
            for (int c = 0; c < 4; c++) if (grid[r, c] != 0) line.Add(grid[r, c]);

            for (int i = 0; i < line.Count - 1; i++)
            {
                if (line[i] == line[i + 1])
                {
                    line[i] *= 2;
                    line.RemoveAt(i + 1);
                    isChanged = true;
                }
            }

            for (int c = 0; c < 4; c++)
            {
                int newValue = (c < line.Count) ? line[c] : 0;
                if (grid[r, c] != newValue) { grid[r, c] = newValue; isChanged = true; }
            }
        }
        return isChanged;
    }

    public bool MoveRight()
    {
        bool isChanged = false;
        for (int r = 0; r < 4; r++)
        {
            List<int> line = new List<int>();
            for (int c = 3; c >= 0; c--) if (grid[r, c] != 0) line.Add(grid[r, c]);

            for (int i = 0; i < line.Count - 1; i++)
            {
                if (line[i] == line[i + 1])
                {
                    line[i] *= 2;
                    line.RemoveAt(i + 1);
                    isChanged = true;
                }
            }

            for (int r_idx = 0; r_idx < 4; r_idx++)
            {
                int newValue = (r_idx < line.Count) ? line[r_idx] : 0;
                if (grid[r, 3 - r_idx] != newValue) { grid[r, 3 - r_idx] = newValue; isChanged = true; }
            }
        }
        return isChanged;
    }

    // --- 유틸리티 레이어 (초기화, 스폰, 로그) ---

    private void InitializeGrid()
    {
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                grid[r, c] = 0;
    }

    private void SpawnTile()
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                if (grid[r, c] == 0) emptyCells.Add(new Vector2Int(r, c));

        if (emptyCells.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyCells.Count);
            Vector2Int chosenCell = emptyCells[randomIndex];
            grid[chosenCell.x, chosenCell.y] = Random.value < 0.9f ? 2 : 4;
        }
    }

    private void PrintGrid()
    {
        string board = "현재 보드 상태:\n";
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++) board += grid[r, c] + " ";
            board += "\n";
        }
        Debug.Log(board);
    }

    public Vector3 GetWorldPosition(int row, int col)
    {
        return new Vector3(col * cellSize, (3 - row) * cellSize, 0);
    }
}