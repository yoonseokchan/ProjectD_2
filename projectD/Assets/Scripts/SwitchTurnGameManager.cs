using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class SwitchTurnGameManager : MonoBehaviour
{
    public CinemachineVirtualCamera player1Camera;
    public CinemachineVirtualCamera player2Camera;
    public Canvas player1Canvas;
    public Canvas player2Canvas;

    private bool isPlayer1Turn = true;

    void Start()
    {
        InvokeRepeating("SwitchPlayerTurn", 30f, 30f); // 5초마다 SwitchPlayerTurn 호출
    }

    public void SwitchPlayerTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;

        // 시네머신 가상 카메라 전환
        if (isPlayer1Turn)
        {
            player1Camera.Priority = 1;
            player2Camera.Priority = 0;
            player1Canvas.gameObject.SetActive(true);
            player2Canvas.gameObject.SetActive(false);
        }
        else
        {
            player1Camera.Priority = 0;
            player2Camera.Priority = 1;
            player1Canvas.gameObject.SetActive(false);
            player2Canvas.gameObject.SetActive(true);
        }

        // 추가적으로 필요한 로직이 있다면 여기에 추가하세요.
    }
}
