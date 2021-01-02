using UnityEngine;
using TMPro;

public class XpDisplayer : MonoBehaviour
{
    public GameObject gameManager;

    private TextMeshProUGUI xpText;

    void Start()
    {
        xpText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        xpText.text = gameManager.GetComponent<XpManager>().xp.ToString();
    }
}


