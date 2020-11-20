using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{

    public int queueSize;
    public List<GameObject> icons;
    private Queue<GameObject> soldiersInQueue;
    public GameObject queueEmptyImage;
    public GameObject queueMeleeImage;
    public GameObject queueRangeImage;
    public GameObject queueTankImage;

    void Start()
    {
        icons = new List<GameObject>();
        soldiersInQueue = new Queue<GameObject>(queueSize);
        int iconWidth = 8;
        RectTransform queueDimensions = (RectTransform)this.transform;
        queueDimensions.sizeDelta = new Vector2(1.5f * iconWidth * queueSize - iconWidth / 2, queueDimensions.rect.height);

        for (int i = 0; i < queueSize; i++)
        {
            GameObject icon = InstantiateIconAtPosition(new Vector3(-queueDimensions.rect.width / 2 + i * 12, 0, 0), queueEmptyImage);
            icons.Add(icon);
        }
    }

    void Update()
    {

    }

    private GameObject InstantiateIconAtPosition(Vector3 position, GameObject iconObject)
    {
        GameObject icon = GameObject.Instantiate<GameObject>(iconObject);
        icon.transform.parent = this.transform;
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
}
