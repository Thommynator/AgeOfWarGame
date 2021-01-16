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

        if (mx >= 0 && mx < w / 2 && my >= h / 2 && my <= h)
        {
            return new Vector2(0, 1 + offset);
        }
        else if (mx >= w / 2 && mx <= w && my >= h / 2 && my <= h)
        {
            return new Vector2(1, 1 + offset);
        }
        else if (mx >= 0 && mx < w / 2 && my >= 0 && my < h / 2)
        {
            return new Vector2(0, 0 - offset);
        }
        else
        {
            return new Vector2(1, 0 - offset);
        }

    }


}
