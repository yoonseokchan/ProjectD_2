using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        // ESC Ű�� ������ ������ ó������ ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
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
        // ���⿡ ������ ó������ �����ϴ� ������ �߰��ϼ���
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // 0�� ù ��° ���� ���� �ε���
    }

    void QuitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}

