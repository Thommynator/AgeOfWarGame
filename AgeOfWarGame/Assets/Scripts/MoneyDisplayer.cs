using UnityEngine;
using TMPro;

public class MoneyDisplayer : MonoBehaviour {
    public GameObject gameManager;

    private TextMeshProUGUI moneyText;

    void Start() {
        moneyText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        moneyText.text = gameManager.GetComponent<PurchaseManager>().playerMoney.ToString();
    }
}
