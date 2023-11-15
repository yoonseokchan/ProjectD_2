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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP1C1(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP1C2(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP1C3(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP1C4(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP2C1(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP2C2(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP2C3(Clone)")
                {
                    draggedObject = hit.collider.gameObject;
                    offset = draggedObject.transform.position - hit.point;
                    isDragging = true;
                }
            }
             if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;
                if(objectName =="MP2C4(Clone)")
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
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 61)) + offset;
            draggedObject.transform.position = newPosition;

            if (Input.GetMouseButtonUp(0))
            {
                // ���콺 ���� ��ư�� ���� ��
                isDragging = false;
                draggedObject = null;
                // A_Destroy 컴포넌트를 비활성화
                A_Destroy aDestroyComponent = draggedObject.GetComponent<A_Destroy>();
                if (aDestroyComponent != null)
                {
                    aDestroyComponent.enabled = false;
                }
            }
        }
    }
}
