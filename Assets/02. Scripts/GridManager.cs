using UnityEngine;

public class GridManager : MonoBehaviour
{
    // [데이터 레이어] 16개의 우편함(기억 장치)
    private int[,] grid = new int[4, 4];
    
    // [설계 레이어] 한 칸의 크기와 간격 (좌표 매핑용)
    [SerializeField] private float cellSize = 1.2f;

    void Start()
    {
        InitializeGrid();
        Debug.Log($"[0,0] 주소의 월드 좌표: {GetWorldPosition(0, 0)}");
        Debug.Log($"[3,3] 주소의 월드 좌표: {GetWorldPosition(3, 3)}");
    }
    
    private void InitializeGrid()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                grid[r, c] = 0; // '0'은 빈칸을 의미함
            }
        }
    }
    
    // 주소를 입력하면 물리적 위치를 알려주는 통역사 함수
    public Vector3 GetWorldPosition(int row, int col)
    {
        // row 0이 가장 위쪽(높은 Y값)에 있어야 하므로 (3 - row) 연산을 사용
        float x = col * cellSize;
        float y = (3 - row) * cellSize; 
    
        return new Vector3(x, y, 0);
    }
}
