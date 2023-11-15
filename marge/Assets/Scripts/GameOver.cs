using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // ���� ���� üũ�� �ֱ������� ȣ��Ǿ�� �� ���Դϴ�.
    void Update()
    {
        // Player1 ���̾ �ش��ϴ� ��� ������Ʈ�� ã���ϴ�.
        GameObject[] player1Objects = GameObject.FindGameObjectsWithTag("Player1");
        // Player2 ���̾ �ش��ϴ� ��� ������Ʈ�� ã���ϴ�.
        GameObject[] player2Objects = GameObject.FindGameObjectsWithTag("Player2");

        // ���� Player1 ���̾ �ش��ϴ� ������Ʈ�� ���ٸ�
        if (player1Objects.Length == 0)
        {
            // Player2 �¸� ���� ȣ���մϴ�.
            SceneManager.LoadScene("Player2WinScene");
        }
        // ���� Player2 ���̾ �ش��ϴ� ������Ʈ�� ���ٸ�
        if (player2Objects.Length == 0)
        {
            // Player2 �¸� ���� ȣ���մϴ�.
            SceneManager.LoadScene("Player1WinScene");
        }
    }
}
