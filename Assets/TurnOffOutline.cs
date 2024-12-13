using UnityEngine;

public class TurnOffOutline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
;
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
