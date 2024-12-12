using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public LayerMask draggableObject;
    public GameObject selectedObject;

    bool isDragging;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDragging = false;
    }

 // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit,Mathf.Infinity, draggableObject))
            {
                selectedObject = hit.collider.gameObject;
                isDragging = true;
            }
        }

        if (isDragging == true)
        {
            Vector3 pos = mousePos();
            selectedObject.transform.position = pos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 97));
    }

}