using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        // ESC 키를 누르면 게임을 처음부터 시작
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    RestartGame();
                }
            }
        }
    }

    void RestartGame()
    {
        // 여기에 게임을 처음부터 시작하는 로직을 추가하세요
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // 0은 첫 번째 씬의 빌드 인덱스
    }

    void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}

