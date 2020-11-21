using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Queue : MonoBehaviour
{

    public int queueSize;
    public List<GameObject> icons;
    private Queue<GameObject> soldiersInQueue;
    public GameObject queueEmptyImage;

    void Start()
    {
        icons = new List<GameObject>();
        soldiersInQueue = new Queue<GameObject>(queueSize);
        InstantiateInitialEmptyIcons();
    }

    void Update()
    {

    }

    private void InstantiateInitialEmptyIcons()
    {
        int iconWidth = 8;
        RectTransform queueDimensions = (RectTransform)this.transform;
        queueDimensions.sizeDelta = new Vector2(1.5f * iconWidth * queueSize - iconWidth / 2, queueDimensions.rect.height);

        for (int i = 0; i < queueSize; i++)
        {
            GameObject icon = InstantiateIconAtPosition(new Vector3(-queueDimensions.rect.width / 2 + i * 1.5f * iconWidth, 0, 0), queueEmptyImage);
            icons.Add(icon);
        }
    }

    private GameObject InstantiateIconAtPosition(Vector3 position, GameObject iconObject)
    {
        GameObject icon = GameObject.Instantiate<GameObject>(iconObject);
        icon.transform.SetParent(this.transform);
        icon.transform.localPosition = position;
        icon.transform.rotation = Quaternion.identity;
        return icon;
    }

    public bool AddSoldierToQueue(GameObject soldier)
    {
        if (soldiersInQueue.Count < queueSize)
        {
            this.soldiersInQueue.Enqueue(soldier);
            Debug.Log("Soldiers in Queue: " + soldiersInQueue.Count);
            UpdateIcons();
            return true;
        }
        return false;
    }

    public GameObject GetNextSoldierInQueue()
    {
        if (this.soldiersInQueue.Count < 1)
        {
            return null;
        }

        return this.soldiersInQueue.Dequeue();
    }

    private void UpdateIcons()
    {
        GameObject[] soldiersArray = soldiersInQueue.ToArray();
        for (int i = 0; i < icons.Count; i++)
        {
            if (i < soldiersArray.Length)
            {
                Debug.Log("Soldiers in Array: " + soldiersArray.Length + " index i: " + i);
                Sprite iconSprite = GetCorrespondingIconSprite(soldiersArray[i]);
                if (iconSprite)
                {
                    icons[i].GetComponent<Image>().sprite = iconSprite;
                }
            }
            else
            {
                icons[i].GetComponent<Image>().sprite = queueEmptyImage.GetComponent<Image>().sprite;
            }
        }
    }

    private Sprite GetCorrespondingIconSprite(GameObject soldier)
    {
        foreach (Transform child in soldier.transform)
        {
            if (child.tag == "QueueIcon")
                return child.GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }
}
