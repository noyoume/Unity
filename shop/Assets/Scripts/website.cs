using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url = "https://www.naver.com"; // 여러분의 웹사이트 URL로 변경하세요

    void OnMouseDown()
    {
        // 마우스 클릭 시 URL 열기
        Application.OpenURL(url);
    }
}