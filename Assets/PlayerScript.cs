using UnityEngine;
using UnityEngine.UIElements;
using UnityEngineInternal;

public class PlayerScript : MonoBehaviour
{
    public LayerMask draggableObject;
    public GameObject selectedObject;

    private bool isOutlineOff = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedObject = null;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, draggableObject))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (isOutlineOff || hitObject != selectedObject)
            {
                // Turn off the outline for the previously selected object, if any
                if (selectedObject != null)
                {
                    var outline = selectedObject.GetComponent<Outline>();
                    if (outline != null)
                    { outline.enabled = false; }
                }

                // Update the selected object
                selectedObject = hitObject;

                // Enable the outline for the currently selected object
                var newOutline = selectedObject.GetComponent<Outline>();
                if (newOutline != null)
                { newOutline.enabled = true; }

                isOutlineOff = false; // Outline is now on
            }
        }
        else if (!isOutlineOff)
        {
            // Turn off the outline for the previously selected object
            if (selectedObject != null)
            {
                var outline = selectedObject.GetComponent<Outline>();
                if (outline != null)
                    outline.enabled = false;

                selectedObject = null; // No object is selected now
            }

            isOutlineOff = true; // Outline is now off
        }
    }
}
