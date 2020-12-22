using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int boundary;
    public int moveSpeed;
    public float leftLimit;
    public float rightLimit;
    public float zoomInLimit;

    public float zoomOutLimit;

    private void Update()
    {

        // move right
        if (transform.position.x < rightLimit && Input.mousePosition.x > Screen.width - boundary)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        // move left
        if (transform.position.x > leftLimit && Input.mousePosition.x < boundary)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        // zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize - Input.GetAxis("Mouse ScrollWheel") * 5, zoomInLimit, zoomOutLimit);
        }

    }
}