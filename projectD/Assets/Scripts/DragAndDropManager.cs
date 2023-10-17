using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject draggedObject;
    private Vector3 offset;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ���� ��ư�� Ŭ���� ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                // �巡���� ������Ʈ�� ���� (���̾ "Player"�� ��쿡��)
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
        }

        if (isDragging)
        {
            // ���콺�� ���� ������Ʈ �̵�
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 60)) + offset;
            draggedObject.transform.position = newPosition;

            if (Input.GetMouseButtonUp(0))
            {
                // ���콺 ���� ��ư�� ���� ��
                isDragging = false;
                draggedObject = null;
            }
        }
    }
}
