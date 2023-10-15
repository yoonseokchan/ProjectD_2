using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
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
            //Ŀ����ġ�� ����������

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    // �巡���� ������Ʈ�� ����
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
        }

        if (isDragging)
        {
            // ���콺�� ���� ������Ʈ �̵�
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) + offset;
            draggedObject.transform.position = newPosition;
            //���콺 ��ư ���� �� �ڸ��� ������Ʈ�� �ִٸ� �巹������ ������Ʈ ������ ȣ���Ͽ� 
            //�� �ڸ��� �ִ� ������Ʈ ������� ������ ���ٸ� ���� �ܰ��� ������Ʈ ���� �ϴ� �ڵ� ���� �ؾ���

            if (Input.GetMouseButtonUp(0))
            {
                // ���콺 ���� ��ư�� ���� ��
                isDragging = false;
                draggedObject = null;
            }
        }
    }
}