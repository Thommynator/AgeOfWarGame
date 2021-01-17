using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public virtual void SetContent(ScriptableObject scriptableObject)
    {
        // implemented by sub-classes
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        GetComponent<RectTransform>().pivot = GetPivot(mousePosition);
        this.transform.position = mousePosition;
    }

    private Vector2 GetPivot(Vector2 mousePosition)
    {

        float mx = mousePosition.x;
        float my = mousePosition.y;

        float w = Screen.width;
        float h = Screen.height;

        float offset = 0.2f;

        if (my >= h / 2 && my <= h)
        {
            return new Vector2(mx / Screen.width, 1 + offset);
        }
        else
        {
            return new Vector2(mx / Screen.width, 0 - offset);
        }

    }


}
