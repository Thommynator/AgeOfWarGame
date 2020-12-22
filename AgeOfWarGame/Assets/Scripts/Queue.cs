using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Queue : MonoBehaviour
{
    public int queueSize;
    public AudioClip addToQueueSound;
    public AudioClip spawnSound;
    private List<GameObject> icons;
    private Queue<GameObject> soldiersInQueue;
    public GameObject queueEmptyImage;
    private bool isSpawnAreaFree;
    private GameObject playerSoldiers;

    private AudioSource audioSource;

    void Start()
    {
        // subscribe events
        GameEvents.current.onPlayerSpawnAreaFree += () => { isSpawnAreaFree = true; };
        GameEvents.current.onPlayerSpawnAreaBlocked += () => { isSpawnAreaFree = false; };
        this.icons = new List<GameObject>();
        this.soldiersInQueue = new Queue<GameObject>(this.queueSize);
        this.isSpawnAreaFree = true;
        this.playerSoldiers = GameObject.Find("PlayerSoldiers");
        audioSource = GetComponent<AudioSource>();
        InstantiateInitialEmptyIcons();
    }

    private void InstantiateInitialEmptyIcons()
    {
        int iconWidth = 8;
        RectTransform queueDimensions = (RectTransform)this.transform;
        queueDimensions.sizeDelta = new Vector2(1.5f * iconWidth * this.queueSize - iconWidth / 2, queueDimensions.rect.height);

        for (int i = 0; i < this.queueSize; i++)
        {
            GameObject icon = InstantiateIconAtPosition(new Vector3(-queueDimensions.rect.width / 2 + i * 1.5f * iconWidth, -iconWidth, 0));
            this.icons.Add(icon);
        }
    }

    private GameObject InstantiateIconAtPosition(Vector3 position)
    {
        GameObject icon = GameObject.Instantiate<GameObject>(this.queueEmptyImage);
        icon.transform.SetParent(this.transform);
        icon.transform.localPosition = position;
        icon.transform.rotation = Quaternion.identity;
        return icon;
    }

    public bool AddSoldierToQueue(GameObject soldier)
    {
        if (this.soldiersInQueue.Count < this.queueSize)
        {
            audioSource.PlayOneShot(addToQueueSound);
            this.soldiersInQueue.Enqueue(soldier);
            UpdateIcons();
            if (this.soldiersInQueue.Count == 1)
            {
                StartCoroutine(StartCooldownForFirstSoldier());
            }
            return true;
        }
        return false;
    }

    private IEnumerator StartCooldownForFirstSoldier()
    {
        float spawnDuration = this.soldiersInQueue.Peek().GetComponent<SoldierBehavior>().soldierConfig.spawnDuration;
        yield return new WaitForSeconds(spawnDuration);
        while (!isSpawnAreaFree)
        {
            yield return new WaitForSeconds(0.2f);
        }
        GameObject nextSoldier = GetNextSoldierInQueue();
        SpawnSoldier(nextSoldier);
    }

    private void SpawnSoldier(GameObject nextSoldier)
    {
        audioSource.PlayOneShot(spawnSound);
        GameObject soldier = GameObject.Instantiate(nextSoldier, new Vector3(-17, -1, 0), Quaternion.identity);
        soldier.gameObject.tag = "PlayerSoldier";
        soldier.layer = LayerMask.NameToLayer("PlayerSoldier");
        soldier.transform.SetParent(playerSoldiers.transform);
    }

    public GameObject GetNextSoldierInQueue()
    {
        if (this.soldiersInQueue.Count < 1)
        {
            return null;
        }
        GameObject nextSoldier = this.soldiersInQueue.Dequeue();
        UpdateIcons();
        if (this.soldiersInQueue.Count > 0)
        {
            StartCoroutine(StartCooldownForFirstSoldier());
        }
        return nextSoldier;
    }

    private void UpdateIcons()
    {
        GameObject[] soldiersArray = this.soldiersInQueue.ToArray();
        for (int i = 0; i < this.icons.Count; i++)
        {
            if (i < soldiersArray.Length)
            {
                Sprite iconSprite = GetCorrespondingIconSprite(soldiersArray[i]);
                if (iconSprite)
                {
                    this.icons[i].GetComponent<Image>().sprite = iconSprite;
                }
            }
            else
            {
                this.icons[i].GetComponent<Image>().sprite = queueEmptyImage.GetComponent<Image>().sprite;
            }
        }
    }

    private Sprite GetCorrespondingIconSprite(GameObject soldier)
    {
        return soldier.GetComponent<SoldierBehavior>().soldierConfig.characterQueueIcon;
    }
}
