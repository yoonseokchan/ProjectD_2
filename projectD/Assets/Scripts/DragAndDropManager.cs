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
            // 마우스 왼쪽 버튼을 클릭할 때
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //커서위치에 뭔가있으면

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    // 드래그할 오브젝트를 선택
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
        }

        if (isDragging)
        {
            // 마우스를 따라 오브젝트 이동
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) + offset;
            draggedObject.transform.position = newPosition;
            //마우스 버튼 업시 그 자리에 오브젝트가 있다면 드레그중인 오브젝트 프리펩 호출하여 
            //그 자리에 있는 오브젝트 프리펩과 대조후 같다면 다음 단계의 오브젝트 출현 하는 코드 구현 해야함

            if (Input.GetMouseButtonUp(0))
            {
                // 마우스 왼쪽 버튼을 놓을 때
                isDragging = false;
                draggedObject = null;
            }
        }
    }
}