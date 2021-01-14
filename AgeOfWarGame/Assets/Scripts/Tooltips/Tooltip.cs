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
        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;
        GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);
        this.transform.position = mousePosition + Vector2.down * 80;
    }


}
