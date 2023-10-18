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
            // 마우스 왼쪽 버튼을 클릭할 때
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                // 드래그할 오브젝트를 선택 (레이어가 "Player"인 경우에만)
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
            // 마우스를 따라 오브젝트 이동
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 60)) + offset;
            draggedObject.transform.position = newPosition;

            if (Input.GetMouseButtonUp(0))
            {
                // 마우스 왼쪽 버튼을 놓을 때
                isDragging = false;
                draggedObject = null;
            }
        }
    }
}
