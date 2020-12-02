using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class XpDisplayer : MonoBehaviour
{
    public GameObject gameManager;

    private TextMeshProUGUI xpText;

    // Start is called before the first frame update
    void Start()
    {
        xpText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        xpText.text = gameManager.GetComponent<XpManager>().xp.ToString();
    }
}


