using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngineInternal;

public class PlayerScript : MonoBehaviour
{
    public GameObject tileManager;
    public List<Tile> hand;

    public LayerMask draggableObject;
    public GameObject selectedObject;

    private bool isOutlineOff = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedObject = null;
        GetHand();
        
    }

    // Update is called once per frame
    void Update()
    {
       ObjectGlow();
    }

   private void GetHand()
    {
        var theScript = tileManager.GetComponent<TileManager>();
        hand = theScript.gameObject.GetComponent<TileManager>().MakeHand();
        int y = -60;
        for (int x = 0; x < hand.Count; x++)
        {
            hand[x].transform.parent = this.transform;
            hand[x].transform.localPosition = Vector3.zero;
            hand[x].transform.localRotation = Quaternion.identity;

            hand[x].transform.localRotation *= Quaternion.Euler(-90, 0, 0);
            hand[x].transform.localPosition += new Vector3(y, -3.2f, 70);
            y += 9;
        }
    } //Gets hand from TileManager. Makes tiles Child of object the script is attached to and sets them based on Seat Orientation

    private void ObjectGlow()
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
                if (newOutline != null && hitObject.transform.parent == this.transform)
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