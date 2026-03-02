using UnityEngine;
using System.Collections.Generic; // 리스트(List) 사용을 위해 필수

public class GridManager : MonoBehaviour
{
    // [데이터 레이어] 16개의 우편함(기억 장치)
    private int[,] grid = new int[4, 4];
    
    // [설계 레이어] 한 칸의 크기와 간격 (좌표 매핑용)
    [SerializeField] private float cellSize = 1.2f;

    void Start()
    {
        // 1. 무대 초기화 (모든 칸을 0으로)
        InitializeGrid();

        // 2. 게임 시작 시 타일 2개 랜덤 생성
        SpawnTile();
        SpawnTile();

        // [테스트] 좌표 매핑 확인 로그
        Debug.Log($"[0,0] 주소의 월드 좌표: {GetWorldPosition(0, 0)}");
        Debug.Log($"[3,3] 주소의 월드 좌표: {GetWorldPosition(3, 3)}");
    }
    
    // 모든 칸을 0(빈칸)으로 비우는 초기화 함수
    private void InitializeGrid()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                grid[r, c] = 0; // '0'은 데이터가 없는 상태를 의미함
            }
        }
    }
    
    // [랜덤 스폰] 비어 있는 칸 중 하나를 골라 2 또는 4를 생성
    private void SpawnTile()
    {
        // 1. 비어 있는 칸(값이 0인 곳)의 주소를 담을 리스트 준비
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        // 2. 2차원 배열 전수 조사하여 빈칸 주소 수집
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                if (grid[r, c] == 0)
                {
                    // 빈칸의 주소(행, 열)를 리스트에 추가
                    emptyCells.Add(new Vector2Int(r, c));
                }
            }
        }

        // 3. 빈칸이 있다면 랜덤하게 하나 골라 숫자 채우기
        if (emptyCells.Count > 0)
        {
            // 리스트에서 무작위 인덱스 선택
            int randomIndex = Random.Range(0, emptyCells.Count);
            Vector2Int chosenCell = emptyCells[randomIndex];

            // 90% 확률로 2, 10% 확률로 4 결정
            int spawnValue = Random.value < 0.9f ? 2 : 4;
            
            // 데이터 업데이트: 배열의 x는 row, y는 col 역할을 수행
            grid[chosenCell.x, chosenCell.y] = spawnValue;

            Debug.Log($"[{chosenCell.x}, {chosenCell.y}] 위치에 {spawnValue} 생성!");
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