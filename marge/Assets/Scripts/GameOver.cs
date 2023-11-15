using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // 게임 오버 체크는 주기적으로 호출되어야 할 것입니다.
    void Update()
    {
        // Player1 레이어에 해당하는 모든 오브젝트를 찾습니다.
        GameObject[] player1Objects = GameObject.FindGameObjectsWithTag("Player1");
        // Player2 레이어에 해당하는 모든 오브젝트를 찾습니다.
        GameObject[] player2Objects = GameObject.FindGameObjectsWithTag("Player2");

        // 만약 Player1 레이어에 해당하는 오브젝트가 없다면
        if (player1Objects.Length == 0)
        {
            // Player2 승리 씬을 호출합니다.
            SceneManager.LoadScene("Player2WinScene");
        }
        // 만약 Player2 레이어에 해당하는 오브젝트가 없다면
        if (player2Objects.Length == 0)
        {
            // Player2 승리 씬을 호출합니다.
            SceneManager.LoadScene("Player1WinScene");
        }
    }
}
