using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Image backgroundImage;

    // 타일의 숫자와 시각적 요소를 설정하는 함수
    public void SetValue(int value)
    {
        numberText.text = value.ToString();
        
        // [꿀팁] 숫자에 따라 타일 색상을 변경하는 로직을 여기에 추가할 수 있다!
        if (value == 2) backgroundImage.color = Color.white;
        else if (value == 4) backgroundImage.color = new Color(1f, 0.9f, 0.7f); // 베이지색
    }
}